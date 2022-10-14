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
        [SerializeField]
        public float moveTurnSpeed = 360;
        [SerializeField]
        public float stationaryTurnSpeed = 180;
        [SerializeField]
        public float jumpPower = 12.0f;
        [SerializeField]
        public float maxJumpTime = 0.5f;
        [SerializeField]
        public float jumpTime;
        [Range(0.0f, 4.0f)][SerializeField]
        public float gravityMultiplier = 2.0f;
        [SerializeField]
        public float runCycleLegOffset = 0.2f;
        [SerializeField]
        public float moveSpeedMultiplier = 1.0f;
        [SerializeField]
        public float animSpeedMultiplier = 1.0f;
        [SerializeField]
        public float groundCheckDistance = 1.0f;

        Rigidbody rb;
        Animator animator;
        CapsuleCollider capsule;

        Vector3 groundNormal;
        Vector3 capsuleCenter;

        const float K_Half = 0.5f;
        float origGroundCheckDistance;
        float turnAmount;
        float forwardAmount;
        float capsuleHeight;

        bool isGrounded;
        bool isJumping;
        bool crouching;


        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            capsule = GetComponent<CapsuleCollider>();

            capsuleHeight = capsule.height;
            capsuleCenter = capsule.center;

            rb.constraints = 
                RigidbodyConstraints.FreezeRotationX
                | RigidbodyConstraints.FreezeRotationY
                | RigidbodyConstraints.FreezeRotationZ;

            origGroundCheckDistance = groundCheckDistance;
        }

        public void Move(Vector3 move, bool crouch, bool jump)
        {
            if(move.magnitude > 1.0f)
            {
                move.Normalize();
            }

            move = transform.InverseTransformDirection(move);
            CheckGroundStatus();
            move = Vector3.ProjectOnPlane(move, groundNormal);
            turnAmount = Mathf.Atan2(move.x, move.z);
            forwardAmount = move.z;

            ApplyExtraTurnRotation();

            if(isGrounded)
            {
                HandleGroundedMovement(crouch, jump);
            }
            else
            {
                HandleAirbrneMovement();
            }

            UpdateAnimator(move);
        }

        void ScaleCapsuleForCrouching(bool crouch)
        {
            if(isGrounded && crouch)
            {
                if(crouching)
                {
                    return;
                }

                capsule.height = capsule.height / 2.0f;
                capsule.center = capsule.center / 2.0f;
                crouching = true;
            }
            else
            {
                Ray crouchRay = 
                    new Ray(rb.position + Vector3.up * capsule.radius * K_Half, Vector3.up);
                float crouchRayLength = capsuleHeight - capsule.radius * K_Half;

                if(Physics.SphereCast(crouchRay, capsule.radius * K_Half,
                    crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
                {
                    crouching = true;
                    return;
                }

                capsule.height = capsuleHeight;
                capsule.center = capsuleCenter;
                crouching = false;
            }
        }

        void PreventStandingInLowHeadroom()
        {
            if(!crouching)
            {
                Ray crouchRay =
                    new Ray(rb.position + Vector3.up * capsule.radius * K_Half, Vector3.up);
                float crouchRayLength = capsuleHeight - capsule.radius * K_Half;
                
                if(Physics.SphereCast(crouchRay,capsule.radius * K_Half,
                   crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
                {
                    crouching = true;
                }
            }
        }

        void UpdateAnimator(Vector3 move)
        {
            animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
            animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
            animator.SetBool("Crouch", crouching);
            animator.SetBool("OnGround", isGrounded);

            if(!isGrounded)
            {
                animator.SetFloat("Jump", rb.velocity.y);
            }
        }
    }
}