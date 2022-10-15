using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーコントローラー
/// </summary>
namespace PlayerController
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody rb;

        [SerializeField]
        private Vector3 moveDirection;      // 移動方向

        [SerializeField]
        private Vector3 moveVelocity;       // 加速度

        [SerializeField]
        private float moveSpeed = 3.0f;     // 移動スピード

        [SerializeField]
        private float jumpPower = 6.0f;     // ジャンプ力

        [SerializeField]
        private float maxJumpTime = 0.5f;   // 最大滞空時間

        private Vector3 latestPosition;     // 前フレームの位置

        public bool isGround;       // 接地しているかどうか
        public bool isJumping;      // ジャンプ中かどうか
        public float jumpTime;      // 現在の滞空時間

        private Animator animator;

        float forwardAmount;
        float turnAmount;
        Vector3 groundNormal;
        float groundCheckDistance = 0.1f;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();

            // 物理演算による回転の影響を受けないように
            rb.freezeRotation = true;
        }

        // Update is called once per frame
        void Update()
        {
            // ジャンプ
            Jump();

            UpdateAnimation();
        }

        private void FixedUpdate()
        {
            if (rb != null)
            {
                // 移動
                Move();
            }
            // ジャンプ中の移動
            JumpMove();

            // 移動方向へ回転
            // 前フレームとの一の差から進行方向を割り出し、その方向に回転する
            Vector3 differenceDis = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z)
                - new Vector3(latestPosition.x, 0.0f, latestPosition.z);

            latestPosition = this.transform.position;

            if(Mathf.Abs(differenceDis.x) > 0.001f
                || Mathf.Abs(differenceDis.z) > 0.001f)
            {
                if(moveDirection == new Vector3(0.0f,0.0f,0.0f))
                {
                    return;
                }

                Quaternion rotation = Quaternion.LookRotation(differenceDis);
                rotation = Quaternion.Slerp(rb.transform.rotation, rotation, 0.2f);

                this.transform.rotation = rotation;
            }
        }

        /// <summary>
        /// アニメーションの更新
        /// </summary>
        void UpdateAnimation()
        {
            animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
            animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
            animator.SetBool("OnGround", isGround);
            if(!isGround)
            {
                animator.SetFloat("Jump", rb.velocity.y);
            }

            float runCycle =
                Mathf.Repeat(
                    animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1);
            float jumpLeg = (runCycle < 0.5f ? 1 : -1) * forwardAmount;

            if (isGround)
            {
                animator.SetFloat("JumpLeg", jumpLeg);
            }

            if (isGround)
            {
                animator.speed = 1.0f;
            }
            else
            {
                // don't use that while airborne
                animator.speed = 1;
            }
        }

        /// <summary>
        /// 移動
        /// </summary>
        private void Move()
        {
            // キー入力受付
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            moveDirection = new Vector3(x, 0.0f, z);
            moveDirection.Normalize();
            moveVelocity = moveDirection * moveSpeed;

            rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);

            // プレイヤーが方向転換するのに必要な回転量と前進量を設定
            moveDirection = this.transform.InverseTransformDirection(moveDirection);
            CheckGroundStatus();
            moveDirection = Vector3.ProjectOnPlane(moveDirection, groundNormal);

            turnAmount = Mathf.Atan2(moveDirection.x, moveDirection.z);
            forwardAmount = moveDirection.z;

        }

        /// <summary>
        /// ジャンプ移動
        /// </summary>
        private void JumpMove()
        {
            if (!isJumping)
            {
                return;
            }

            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            // 滞空時間の計算
            float t = jumpTime / maxJumpTime;
            float power = jumpPower * 0.7f;

            // 滞空時間が制限を超えたら
            if (t >= 1.0f)
            {
                isJumping = false;
                jumpTime = 0.0f;
            }

            rb.AddForce(power * Vector3.up, ForceMode.Impulse);
        }

        private void Jump()
        {
            // ジャンプ開始判定
            if (isGround && Input.GetKey(KeyCode.Space))
            {
                isJumping = true;
            }

            // ジャンプ中の処理
            if (isJumping)
            {
                isGround = false;

                // ジャンプボタンを離したら or 滞空時間が制限を超えたら
                if (Input.GetKeyUp(KeyCode.Space) || jumpTime >= maxJumpTime)
                {
                    isJumping = false;
                    jumpTime = 0.0f;
                }
                // ジャンプボタンを押している間
                else if (Input.GetKey(KeyCode.Space))
                {
                    // 滞空時間を加算
                    jumpTime += Time.deltaTime;
                }
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Ground")
            {
                isGround = true;
            }
        }

        /// <summary>
        /// 地面の状態を取得
        /// </summary>
        void CheckGroundStatus()
        {
            RaycastHit hitInfo;
#if UNITY_EDITOR
            // Editor上の動作
            // シーンで地面の判定をするRayを可視化する(デバッグ用）
            Debug.DrawLine(transform.position + (Vector3.up * 0.1f), 
                transform.position + (Vector3.up * 0.1f) + (Vector3.down * groundCheckDistance));
#endif
            // Editor以外の動作
            // 0.1f...キャラクター内部からRayを発射するためのオフセット
            if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance))
            {
                groundNormal = hitInfo.normal;
                animator.applyRootMotion = true;
            }
            else
            {
                groundNormal = Vector3.up;
                animator.applyRootMotion = false;
            }
        }
    }
}