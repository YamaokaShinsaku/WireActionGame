using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�R���g���[���[
/// </summary>
namespace PlayerController
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody rb;

        [SerializeField]
        private Vector3 moveDirection;      // �ړ�����

        [SerializeField]
        private Vector3 moveVelocity;       // �����x

        [SerializeField]
        private float moveSpeed = 3.0f;     // �ړ��X�s�[�h

        [SerializeField]
        private float jumpPower = 6.0f;     // �W�����v��

        [SerializeField]
        private float maxJumpTime = 0.5f;   // �ő�؋󎞊�

        private Vector3 latestPosition;     // �O�t���[���̈ʒu

        public bool isGround;       // �ڒn���Ă��邩�ǂ���
        public bool isJumping;      // �W�����v�����ǂ���
        public float jumpTime;      // ���݂̑؋󎞊�

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

            // �������Z�ɂ���]�̉e�����󂯂Ȃ��悤��
            rb.freezeRotation = true;
        }

        // Update is called once per frame
        void Update()
        {
            // �W�����v
            Jump();

            UpdateAnimation();
        }

        private void FixedUpdate()
        {
            if (rb != null)
            {
                // �ړ�
                Move();
            }
            // �W�����v���̈ړ�
            JumpMove();

            // �ړ������։�]
            // �O�t���[���Ƃ̈�̍�����i�s����������o���A���̕����ɉ�]����
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
        /// �A�j���[�V�����̍X�V
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
        /// �ړ�
        /// </summary>
        private void Move()
        {
            // �L�[���͎�t
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            moveDirection = new Vector3(x, 0.0f, z);
            moveDirection.Normalize();
            moveVelocity = moveDirection * moveSpeed;

            rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);

            // �v���C���[�������]������̂ɕK�v�ȉ�]�ʂƑO�i�ʂ�ݒ�
            moveDirection = this.transform.InverseTransformDirection(moveDirection);
            CheckGroundStatus();
            moveDirection = Vector3.ProjectOnPlane(moveDirection, groundNormal);

            turnAmount = Mathf.Atan2(moveDirection.x, moveDirection.z);
            forwardAmount = moveDirection.z;

        }

        /// <summary>
        /// �W�����v�ړ�
        /// </summary>
        private void JumpMove()
        {
            if (!isJumping)
            {
                return;
            }

            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            // �؋󎞊Ԃ̌v�Z
            float t = jumpTime / maxJumpTime;
            float power = jumpPower * 0.7f;

            // �؋󎞊Ԃ������𒴂�����
            if (t >= 1.0f)
            {
                isJumping = false;
                jumpTime = 0.0f;
            }

            rb.AddForce(power * Vector3.up, ForceMode.Impulse);
        }

        private void Jump()
        {
            // �W�����v�J�n����
            if (isGround && Input.GetKey(KeyCode.Space))
            {
                isJumping = true;
            }

            // �W�����v���̏���
            if (isJumping)
            {
                isGround = false;

                // �W�����v�{�^���𗣂����� or �؋󎞊Ԃ������𒴂�����
                if (Input.GetKeyUp(KeyCode.Space) || jumpTime >= maxJumpTime)
                {
                    isJumping = false;
                    jumpTime = 0.0f;
                }
                // �W�����v�{�^���������Ă����
                else if (Input.GetKey(KeyCode.Space))
                {
                    // �؋󎞊Ԃ����Z
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
        /// �n�ʂ̏�Ԃ��擾
        /// </summary>
        void CheckGroundStatus()
        {
            RaycastHit hitInfo;
#if UNITY_EDITOR
            // Editor��̓���
            // �V�[���Œn�ʂ̔��������Ray����������(�f�o�b�O�p�j
            Debug.DrawLine(transform.position + (Vector3.up * 0.1f), 
                transform.position + (Vector3.up * 0.1f) + (Vector3.down * groundCheckDistance));
#endif
            // Editor�ȊO�̓���
            // 0.1f...�L�����N�^�[��������Ray�𔭎˂��邽�߂̃I�t�Z�b�g
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