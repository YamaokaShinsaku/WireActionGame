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

    private Rigidbody firstRigidbody;
    private Rigidbody terminalRigidbody;
    private ConfigurableJoint primaryJoint;
    private ConfigurableJoint secondaryJoint;

    private void Start()
    {
        this.firstRigidbody = this.firstBall.GetComponent<Rigidbody>();
        var firstPosition = this.firstPositionBase.transform.position;
        this.firstBall.transform.position = firstPosition;

        // �����ʒu�Ɋ�̃{�[����ݒu
        this.terminalRigidbody = Instantiate(this.baseBall, firstPosition, Quaternion.identity).GetComponent<Rigidbody>();

        // �q�G�����L�[��Ŋm�F���₷�����邽�߁A�{�[���ɂ́u�v���n�u�� �A�ԁv�̃X�^�C���̖��O��t����
        // ��̃{�[����0�ԂƂ���
        this.terminalRigidbody.name = $"{this.baseBall.name} 0";

        // ��̃{�[���͏����ʒu�ɌŒ肷��
        this.terminalRigidbody.gameObject.AddComponent<FixedJoint>();

        // firstBall�͂܂���̃{�[���Ɛڑ�����
        this.primaryJoint = this.firstRigidbody.gameObject.AddComponent<ConfigurableJoint>();
        this.primaryJoint.connectedBody = this.terminalRigidbody;
    }

    private void FixedUpdate()
    {
        // terminalRigidBody �ɑ΂���firstBall �̑��Έʒu��relativePosition�Ƃ���
        var ballPosition = this.firstRigidbody.position;
        var relativePosition = ballPosition - this.terminalRigidbody.position;

        // ���̑��Έʒu�x�N�g���̒�����interval�ȏ�ł���΃{�[����ǉ�����
        while(relativePosition.magnitude >= this.interval)
        {
            // terminalRigidBody����interval�������ꂽ�ʒu�ɐV�K�{�[����ݒu
            var newTerminalPosition = this.terminalRigidbody.position + Vector3.ClampMagnitude(relativePosition, this.interval);
            var newTerminalRigidbody = Instantiate(this.baseBall, newTerminalPosition, Quaternion.identity).GetComponent<Rigidbody>();
            var previousTerminalName = this.terminalRigidbody.name;
            var previousTerminalIndex = int.Parse(previousTerminalName.Substring(previousTerminalName.LastIndexOf(' ') + 1));
            newTerminalRigidbody.name = $"{this.baseBall.name} {previousTerminalIndex + 1}";

            // Joint���A�^�b�`���A������Joint���Ȃ��ւ���
            this.secondaryJoint = newTerminalRigidbody.gameObject.AddComponent<ConfigurableJoint>();
            this.secondaryJoint.connectedBody = this.terminalRigidbody;
            this.secondaryJoint.xMotion = ConfigurableJointMotion.Locked;
            this.secondaryJoint.yMotion = ConfigurableJointMotion.Locked;
            this.secondaryJoint.zMotion = ConfigurableJointMotion.Locked;
            this.primaryJoint.connectedBody = newTerminalRigidbody;

            // �V�����{�[��������terminalRigidbody�Ƃ��ArelativePosition���X�V����
            this.terminalRigidbody = newTerminalRigidbody;
            relativePosition = ballPosition - this.terminalRigidbody.position;
        }

        // secondaryJoint�����݂���Ȃ�΁A��̃{�[����firstRigidbody�̊Ԃ�1�ȏ�̃{�[����
        // ���݂���Ȃ�΁A�X��relativePosition���ǂꂾ���܂�Ȃ����Ă��邩���ׂ�
        while((this.secondaryJoint != null) && (Vector3.Dot(relativePosition,this.terminalRigidbody.position 
            - this.secondaryJoint.connectedBody.position) < 0.0f))
        {
            // ����90�x�𒴂��ċȂ����Ă���΁A�����k�߂悤�Ƃ��Ă���Ƃ݂Ȃ�
            // Joint�̂Ȃ��ւ����s���AterminalRigidbody���폜����
            this.primaryJoint.connectedBody = this.secondaryJoint.connectedBody;
            Destroy(this.terminalRigidbody.gameObject);

            // primaryJoint�̐ڑ��������terminalRigidbody�Ƃ��A
            // secondaryJoint��relativePosition���X�V����
            this.terminalRigidbody = this.primaryJoint.connectedBody;
            this.secondaryJoint = this.terminalRigidbody.GetComponent<ConfigurableJoint>();
            relativePosition = ballPosition - this.terminalRigidbody.position;
        }

        // �L�[�{�[�h����ňړ�
        var velocity = this.firstRigidbody.velocity;
        velocity.z = 0.0f;
        if (Input.GetKey(KeyCode.I))
        {
            velocity.z += this.speed;
        }
        if (Input.GetKey(KeyCode.K))
        {
            velocity.z -= this.speed;
        }
        this.firstRigidbody.velocity = velocity;
    }
}
