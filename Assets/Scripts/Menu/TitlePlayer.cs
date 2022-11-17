using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayer : MonoBehaviour
{
    private Rigidbody rb;

    private Animator animator;

    float forwardAmount;
    float turnAmount;
    Vector3 groundNormal;

    [SerializeField]
    private GroundCheck groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        groundCheck = GetComponent<GroundCheck>();

        // �������Z�ɂ���]�̉e�����󂯂Ȃ��悤��
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
    /// �A�j���[�V�����̍X�V
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
