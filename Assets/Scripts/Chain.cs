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

    private Rigidbody firstRigidbody;
    private Rigidbody terminalRigidbody;
    private ConfigurableJoint primaryJoint;
    private ConfigurableJoint secondaryJoint;

    private void Start()
    {
        this.firstRigidbody = this.firstBall.GetComponent<Rigidbody>();
        var firstPosition = this.firstPositionBase.transform.position;
        this.firstBall.transform.position = firstPosition;

        // 初期位置に基準のボールを設置
        this.terminalRigidbody = Instantiate(this.baseBall, firstPosition, Quaternion.identity).GetComponent<Rigidbody>();

        // ヒエラルキー上で確認しやすくするため、ボールには「プレハブ名 連番」のスタイルの名前を付ける
        // 基準のボールは0番とする
        this.terminalRigidbody.name = $"{this.baseBall.name} 0";

        // 基準のボールは初期位置に固定する
        this.terminalRigidbody.gameObject.AddComponent<FixedJoint>();

        // firstBallはまず基準のボールと接続する
        this.primaryJoint = this.firstRigidbody.gameObject.AddComponent<ConfigurableJoint>();
        this.primaryJoint.connectedBody = this.terminalRigidbody;
    }

    private void FixedUpdate()
    {
        // terminalRigidBody に対するfirstBall の相対位置をrelativePositionとする
        var ballPosition = this.firstRigidbody.position;
        var relativePosition = ballPosition - this.terminalRigidbody.position;

        // その相対位置ベクトルの長さがinterval以上であればボールを追加する
        while(relativePosition.magnitude >= this.interval)
        {
            // terminalRigidBodyからintervalだけ離れた位置に新規ボールを設置
            var newTerminalPosition = this.terminalRigidbody.position + Vector3.ClampMagnitude(relativePosition, this.interval);
            var newTerminalRigidbody = Instantiate(this.baseBall, newTerminalPosition, Quaternion.identity).GetComponent<Rigidbody>();
            var previousTerminalName = this.terminalRigidbody.name;
            var previousTerminalIndex = int.Parse(previousTerminalName.Substring(previousTerminalName.LastIndexOf(' ') + 1));
            newTerminalRigidbody.name = $"{this.baseBall.name} {previousTerminalIndex + 1}";

            // Jointをアタッチし、既存のJointをつなぎ替える
            this.secondaryJoint = newTerminalRigidbody.gameObject.AddComponent<ConfigurableJoint>();
            this.secondaryJoint.connectedBody = this.terminalRigidbody;
            this.secondaryJoint.xMotion = ConfigurableJointMotion.Locked;
            this.secondaryJoint.yMotion = ConfigurableJointMotion.Locked;
            this.secondaryJoint.zMotion = ConfigurableJointMotion.Locked;
            this.primaryJoint.connectedBody = newTerminalRigidbody;

            // 新しいボールを次のterminalRigidbodyとし、relativePositionも更新する
            this.terminalRigidbody = newTerminalRigidbody;
            relativePosition = ballPosition - this.terminalRigidbody.position;
        }

        // secondaryJointが存在するならば、基準のボールとfirstRigidbodyの間に1つ以上のボールが
        // 存在するならば、更にrelativePositionがどれだけ折れ曲がっているか調べる
        while((this.secondaryJoint != null) && (Vector3.Dot(relativePosition,this.terminalRigidbody.position 
            - this.secondaryJoint.connectedBody.position) < 0.0f))
        {
            // もし90度を超えて曲がっていれば、鎖を縮めようとしているとみなす
            // Jointのつなぎ替えを行い、terminalRigidbodyを削除する
            this.primaryJoint.connectedBody = this.secondaryJoint.connectedBody;
            Destroy(this.terminalRigidbody.gameObject);

            // primaryJointの接続先を次のterminalRigidbodyとし、
            // secondaryJointとrelativePositionも更新する
            this.terminalRigidbody = this.primaryJoint.connectedBody;
            this.secondaryJoint = this.terminalRigidbody.GetComponent<ConfigurableJoint>();
            relativePosition = ballPosition - this.terminalRigidbody.position;
        }

        // キーボード操作で移動
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
