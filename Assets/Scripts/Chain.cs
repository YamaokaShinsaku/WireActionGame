using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public GameObject baseBall;     // ��ƂȂ�{�[��
    public GameObject firstBall;    // �ŏ��ɏo�Ă���{�[��

    public Transform firstPositionBase;    // �{�[���̔��ˏ����ʒu
    public float speed = 10;        // �{�[���̑��x
    public float interval = 2.0f;   // �{�[���̐ݒu�Ԋu
    public float angularLimit = 6.0f;   // �W���C���g�̋Ȃ���p�x�̏��
    public float spring = 100.0f;
    public float damper = 10.0f;

    private Rigidbody rootRigidbody;
    private Rigidbody firstChildRigidbody;
    private Rigidbody firstRigidbody;
    //private Rigidbody terminalRigidbody;
  
    private ConfigurableJoint primaryJoint;
    private ConfigurableJoint secondaryJoint;

    private Vector3 connectedAnchor;
    private SoftJointLimit highLimit;
    private SoftJointLimit lowLimit;

    private JointDrive drive;
    public bool isWinding;

	/// <summary>
	/// primaryJoint ��������胂�[�h�ɐ؂�ւ���
	/// </summary>
	private bool IsWinding
	{
		get => this.isWinding;
		set
		{
			this.isWinding = value;
			var joint = this.primaryJoint;
			if (value)
			{
				joint.connectedAnchor = joint.connectedBody.transform.InverseTransformPoint(this.rootRigidbody.position);
				joint.zMotion = ConfigurableJointMotion.Locked;
			}
			else
			{
				joint.zMotion = ConfigurableJointMotion.Free;
			}
		}
	}

	private void Start()
	{
		this.firstRigidbody = this.firstBall.GetComponent<Rigidbody>();
		var firstPosition = this.firstPositionBase.transform.position; //�{�[���̔�������ʒu�̐ݒ�
		this.firstBall.transform.position = firstPosition; //�����ʒu�ɏ����{�[�����ړ�������

		// �����ʒu�ɍ����̃{�[����ݒu
		this.rootRigidbody = Instantiate(this.baseBall, firstPosition, Quaternion.identity).GetComponent<Rigidbody>();

		// �ufirstChildRigidbody�v��rootRigidbody�ƒ������Ă��钼�ڂ̎q��\��
		// �܂���firstBall��firstChildRigidbody�Ƃ���
		this.firstChildRigidbody = this.firstRigidbody;

		// �q�G�����L�[��ł̊m�F��e�Ղɂ��邽�߁A�{�[���ɂ́u�v���n�u�� �A�ԁv�̌`�̖��O��t����
		// rootRigidbody�͓��ʈ����ŘA�Ԃ͕t�����i����������u�v���n�u�� Root�v�Ƃ���j�AfirstBall��0�ԂƂ���
		this.rootRigidbody.name = $"{this.baseBall.name} Root";
		this.firstChildRigidbody.name = $"{this.baseBall.name} 0";

		// �����͏����ʒu�ɌŒ肷�邱�Ƃɂ���
		this.rootRigidbody.gameObject.AddComponent<FixedJoint>();

		// rootRigidbody��firstChildRigidbody��ڑ�����
		this.connectedAnchor = Vector3.back * this.interval;
		this.highLimit = new SoftJointLimit { limit = this.angularLimit };
		this.lowLimit = this.highLimit;
		this.lowLimit.limit *= -1.0f;
		this.drive = new JointDrive
		{
			maximumForce = float.MaxValue,
			positionSpring = this.spring,
			positionDamper = this.damper
		};
		this.primaryJoint = this.rootRigidbody.gameObject.AddComponent<ConfigurableJoint>();
		{
			var joint = this.primaryJoint;
			joint.rotationDriveMode = RotationDriveMode.XYAndZ;
			joint.angularXDrive = joint.angularYZDrive = this.drive;
			joint.xMotion = joint.yMotion = ConfigurableJointMotion.Locked;
			joint.zMotion = ConfigurableJointMotion.Free;
			joint.angularXMotion = joint.angularYMotion = ConfigurableJointMotion.Limited;
			joint.angularZMotion = ConfigurableJointMotion.Locked;
			joint.highAngularXLimit = joint.angularYLimit = this.highLimit;
			joint.lowAngularXLimit = this.lowLimit;
			joint.anchor = Vector3.zero;
			joint.autoConfigureConnectedAnchor = false;
			joint.connectedAnchor = Vector3.zero;
			joint.connectedBody = this.firstChildRigidbody;
		}
	}

	private void Update()
	{
		// �����L�[������������Ɗ�����胂�[�h�A�����ƒʏ탂�[�h�Ƃ���
		if (Input.GetKeyDown(KeyCode.K))
		{
			this.IsWinding = true;
		}

		if (Input.GetKeyUp(KeyCode.K))
		{
			this.IsWinding = false;
		}
	}

	private void FixedUpdate()
	{
		var rootPosition = this.rootRigidbody.position;
		var rootRotation = this.rootRigidbody.rotation;

		// �L�[�{�[�h�����firstRigidbody���ړ�
		var velocity = this.firstRigidbody.velocity;
		velocity.z = 0.0f;
		if (this.IsWinding)
		{
			// ������胂�[�h�̏ꍇ�AconnectedAnchor���k�߂邱�ƂŃ��[���̊�������\������
			this.primaryJoint.connectedAnchor = Vector3.MoveTowards(
				this.primaryJoint.connectedAnchor,
				Vector3.zero,
				this.speed * Time.deltaTime);

			// secondaryJoint�����݂���Ȃ�΁A�܂�firstChildRigidbody�̎���1�ȏ�{�[����
			// ���݂���Ȃ�΁A�����connectedAnchor���قڃ[���܂ŏk�񂾂��𒲂ׂ�
			while ((this.secondaryJoint != null) && (this.primaryJoint.connectedAnchor.sqrMagnitude < 0.01f))
			{
				// connectedAnchor���k�݂����Ă���΃W���C���g�̂Ȃ��ւ����s���AfirstChildRigidbody���폜����
				var newFirstChildRigidbody = this.secondaryJoint.connectedBody;
				var newFirstChildPosition = newFirstChildRigidbody.position;
				var newFirstChildRotation = newFirstChildRigidbody.rotation;
				DestroyImmediate(this.firstChildRigidbody.gameObject);
				this.primaryJoint.connectedAnchor = newFirstChildRigidbody.transform.InverseTransformPoint(rootPosition);
				newFirstChildRigidbody.position = rootPosition;
				newFirstChildRigidbody.rotation = rootRotation;
				this.primaryJoint.connectedBody = newFirstChildRigidbody;
				newFirstChildRigidbody.position = newFirstChildPosition;
				newFirstChildRigidbody.rotation = newFirstChildRotation;

				// primaryJoint�̐ڑ��������firstChildRigidbody�Ƃ��AsecondaryJoint���X�V����
				this.firstChildRigidbody = newFirstChildRigidbody;
				this.secondaryJoint = newFirstChildRigidbody.GetComponent<ConfigurableJoint>();
			}
		}
		else
		{
			if (Input.GetKey(KeyCode.I))
			{
				velocity.z += this.speed;
			}

			// firstChildRigidbody�ɑ΂���rootRigidbody�̑��Έʒu��relativePosition�Ƃ���
			var ballPosition = this.firstChildRigidbody.position;
			var ballRotation = this.firstChildRigidbody.rotation;
			var relativePosition = rootPosition - ballPosition;

			// ���̑��Έʒu�x�N�g���̒�����interval�ȏ�ł���΃{�[����ǉ�����
			var distance = relativePosition.magnitude;
			while (distance >= this.interval)
			{
				// firstChildRigidbody����interval�������ꂽ�ʒu�ɐV�K�{�[����ݒu����
				var newFirstChildPosition = ballPosition + Vector3.ClampMagnitude(relativePosition, this.interval);
				var newFirstChildRigidbody = Instantiate(this.baseBall, rootPosition, rootRotation).GetComponent<Rigidbody>();
				var previousFirstChildName = this.firstChildRigidbody.name;
				var previousFirstChildIndex = int.Parse(previousFirstChildName.Substring(previousFirstChildName.LastIndexOf(' ') + 1));
				newFirstChildRigidbody.name = $"{this.baseBall.name} {previousFirstChildIndex + 1}";

				// �W���C���g���A�^�b�`���A�����̃W���C���g���Ȃ��ւ���
				this.firstChildRigidbody.position = rootPosition;
				this.firstChildRigidbody.rotation = rootRotation;
				this.secondaryJoint = newFirstChildRigidbody.gameObject.AddComponent<ConfigurableJoint>();
				{
					var joint = this.secondaryJoint;
					joint.rotationDriveMode = RotationDriveMode.XYAndZ;
					joint.angularXDrive = joint.angularYZDrive = this.drive;
					joint.xMotion = joint.yMotion = joint.zMotion = ConfigurableJointMotion.Locked;
					joint.angularXMotion = joint.angularYMotion = ConfigurableJointMotion.Limited;
					joint.angularZMotion = ConfigurableJointMotion.Locked;
					joint.highAngularXLimit = joint.angularYLimit = this.highLimit;
					joint.lowAngularXLimit = this.lowLimit;
					joint.anchor = Vector3.zero;
					joint.autoConfigureConnectedAnchor = false;
					joint.connectedAnchor = this.connectedAnchor;
					joint.connectedBody = this.firstChildRigidbody;
				}
				this.primaryJoint.connectedBody = newFirstChildRigidbody;
				newFirstChildRigidbody.position = newFirstChildPosition;
				newFirstChildRigidbody.rotation = Quaternion.Lerp(ballRotation, rootRotation, this.interval / distance);
				this.firstChildRigidbody.position = ballPosition;
				this.firstChildRigidbody.rotation = ballRotation;

				// �V�����{�[��������firstChildRigidbody�Ƃ��AballPosition��relativePosition���X�V����
				this.firstChildRigidbody = newFirstChildRigidbody;
				ballPosition = this.firstChildRigidbody.position;
				relativePosition = rootPosition - ballPosition;
				distance = relativePosition.magnitude;
			}
		}

		this.firstRigidbody.velocity = velocity;
	}
}
