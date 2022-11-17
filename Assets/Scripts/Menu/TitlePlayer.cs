using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayer : MonoBehaviour
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

    //public bool isGround;       // 接地しているかどうか
    public bool isJumping;      // ジャンプ中かどうか
    public float jumpTime;      // 現在の滞空時間

    private Animator animator;

    float forwardAmount;
    float turnAmount;
    Vector3 groundNormal;
    public float groundCheckDistance;

    [SerializeField]
    private GroundCheck groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        groundCheck = GetComponent<GroundCheck>();

        // 物理演算による回転の影響を受けないように
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();
        //CheckGround();

        groundCheck.CheckGround();
    }


    /// <summary>
    /// アニメーションの更新
    /// </summary>
    void UpdateAnimation()
    {
        animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
        animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
        animator.SetBool("OnGround", groundCheck.isGround);
        if (!groundCheck.isGround)
        {
            animator.SetFloat("Jump", rb.velocity.y);
        }

        float runCycle =
            Mathf.Repeat(
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1);
        float jumpLeg = (runCycle < 0.5f ? 1 : -1) * forwardAmount;

        if (groundCheck.isGround)
        {
            animator.SetFloat("JumpLeg", jumpLeg);
        }

        if (groundCheck.isGround)
        {
            animator.speed = 1.0f;
        }
        else
        {
            // don't use that while airborne
            animator.speed = 1;
        }
    }

}
