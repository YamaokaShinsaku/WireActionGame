using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public GameObject baseBall;     // 基準となるボール
    public GameObject firstBall;    // 最初に出てくるボール

    public Transform firstPositionBase;    // ボールの発射初期位置
    public float speed = 10;        // ボールの速度
    public float interval = 2.0f;   // ボールの設置間隔
    public float angularLimit = 6.0f;   // ジョイントの曲がる角度の上限
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
	/// primaryJoint を巻き取りモードに切り替える
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
		var firstPosition = this.firstPositionBase.transform.position; //ボールの発生する位置の設定
		this.firstBall.transform.position = firstPosition; //初期位置に初期ボールを移動させる

		// 初期位置に根元のボールを設置
		this.rootRigidbody = Instantiate(this.baseBall, firstPosition, Quaternion.identity).GetComponent<Rigidbody>();

		// 「firstChildRigidbody」はrootRigidbodyと直結している直接の子を表す
		// まずはfirstBallをfirstChildRigidbodyとする
		this.firstChildRigidbody = this.firstRigidbody;

		// ヒエラルキー上での確認を容易にするため、ボールには「プレハブ名 連番」の形の名前を付ける
		// rootRigidbodyは特別扱いで連番は付けず（さしあたり「プレハブ名 Root」とする）、firstBallを0番とする
		this.rootRigidbody.name = $"{this.baseBall.name} Root";
		this.firstChildRigidbody.name = $"{this.baseBall.name} 0";

		// 根元は初期位置に固定することにする
		this.rootRigidbody.gameObject.AddComponent<FixedJoint>();

		// rootRigidbodyとfirstChildRigidbodyを接続する
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
		// 下矢印キーを押し下げると巻き取りモード、離すと通常モードとする
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

		// キーボード操作でfirstRigidbodyを移動
		var velocity = this.firstRigidbody.velocity;
		velocity.z = 0.0f;
		if (this.IsWinding)
		{
			// 巻き取りモードの場合、connectedAnchorを縮めることでリールの巻き取りを表現する
			this.primaryJoint.connectedAnchor = Vector3.MoveTowards(
				this.primaryJoint.connectedAnchor,
				Vector3.zero,
				this.speed * Time.deltaTime);

			// secondaryJointが存在するならば、つまりfirstChildRigidbodyの次に1つ以上ボールが
			// 存在するならば、さらにconnectedAnchorがほぼゼロまで縮んだかを調べる
			while ((this.secondaryJoint != null) && (this.primaryJoint.connectedAnchor.sqrMagnitude < 0.01f))
			{
				// connectedAnchorが縮みきっていればジョイントのつなぎ替えを行い、firstChildRigidbodyを削除する
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

				// primaryJointの接続先を次のfirstChildRigidbodyとし、secondaryJointも更新する
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

			// firstChildRigidbodyに対するrootRigidbodyの相対位置をrelativePositionとする
			var ballPosition = this.firstChildRigidbody.position;
			var ballRotation = this.firstChildRigidbody.rotation;
			var relativePosition = rootPosition - ballPosition;

			// その相対位置ベクトルの長さがinterval以上であればボールを追加する
			var distance = relativePosition.magnitude;
			while (distance >= this.interval)
			{
				// firstChildRigidbodyからintervalだけ離れた位置に新規ボールを設置する
				var newFirstChildPosition = ballPosition + Vector3.ClampMagnitude(relativePosition, this.interval);
				var newFirstChildRigidbody = Instantiate(this.baseBall, rootPosition, rootRotation).GetComponent<Rigidbody>();
				var previousFirstChildName = this.firstChildRigidbody.name;
				var previousFirstChildIndex = int.Parse(previousFirstChildName.Substring(previousFirstChildName.LastIndexOf(' ') + 1));
				newFirstChildRigidbody.name = $"{this.baseBall.name} {previousFirstChildIndex + 1}";

				// ジョイントもアタッチし、既存のジョイントをつなぎ替える
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

				// 新しいボールを次のfirstChildRigidbodyとし、ballPositionとrelativePositionも更新する
				this.firstChildRigidbody = newFirstChildRigidbody;
				ballPosition = this.firstChildRigidbody.position;
				relativePosition = rootPosition - ballPosition;
				distance = relativePosition.magnitude;
			}
		}

		this.firstRigidbody.velocity = velocity;
	}
}
