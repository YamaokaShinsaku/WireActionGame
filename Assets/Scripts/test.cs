using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpiderChan
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(LineRenderer))]
    public class test : MonoBehaviour
    {
        [SerializeField]
        private float maxDistance = 100.0f; //  ����L�΂���ő勗��

        [SerializeField]
        private LayerMask interactiveLayers;    // �������������郌�C���[

        [SerializeField]
        private Vector3 casterCenter = new Vector3(0.0f, 0.5f, 0.0f);   // �I�u�W�F�N�g�̃��[�J�����W�ł���킵�����̎ˏo�ʒu

        [SerializeField]
        private Transform casterCenterObj;     // ���̎ˏo�ʒu�̃I�u�W�F�N�g

        [SerializeField]
        private float spring = 50.0f;       // ���̕���������S������SpringJoint��Spring

        [SerializeField]
        private float damper = 20.0f;       // ���̕���������S������SpringJoint��damper

        [SerializeField]
        private float equilibrimLength = 1.0f;     // �����k�߂����̎��R��

        //[SerializeField]
        //private float ikTransitionTime = 0.5f;      // �r�̈ʒu�̑J�ڎ���

        [SerializeField]
        public RawImage reticle;       // ���𒣂�邩�ǂ����̏�Ԃɍ��킹�āA�Ə��}�[�N��ύX����

        [SerializeField]
        public Texture reticleImageValid;      // �Ə��}�[�N

        [SerializeField]
        public Texture reticleImageInValid;    // �֎~�}�[�N

        [SerializeField]
        private ParticleSystem particle;    // �G�t�F�N�g�i�W�����j

        [SerializeField]
        public float bulletTimeCount;         // �o���b�g�^�C���̐�������

        [SerializeField]
        public bool isBulletTime;        // �o���b�g�^�C�������ǂ���

        [SerializeField]
        private GameObject Crystal;     // �N���X�^��


        private GameObject clone;       // �I�u�W�F�N�g��clone�����p

        private Animator animator;
        private Transform cameraTransform;
        private LineRenderer lineRenderer;
        private SpringJoint springJoint;
        private ConfigurableJoint joint;

        // �E���L�΂��A�߂�������X���[�Y�ɂ��邽��
        private float currentIkWeight;  // ���݂̃E�F�C�g
        private float targetIkWeight;   // �ڕW�E�F�C�g
        private float ikWeightVelocity; // �E�F�C�g�ω���

        private bool casting;               // �����ˏo�����ǂ���
        private bool needsUpdateSpring;     // FixedUpdate����SpringJoint�̏�Ԃ��K�v���ǂ���
        private float stringLength;         // ���݂̎��̒���....�����FixedUpdate����SpringJoint��maxDistance�ɃZ�b�g����
        private readonly Vector3[] stringAnchor = new Vector3[2];   // SpringJoint�̃v���C���[���Ɛڒ��ʑ��̖��[
        private Vector3 worldCasterCenter;   // casterCenter�����[���h���W�ɕϊ���������

        public PlayerController.PlayerController playerController;
        public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter thirdPerson;
        public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl userControl;


        /// <summary>
        /// �G�t�F�N�g�Đ�
        /// </summary>
        private void Play()
        {
            particle.Play();
        }
        /// <summary>
        /// �G�t�F�N�g��~
        /// </summary>
        private void Stop()
        {
            particle.Stop();
        }

        private void Awake()
        {
            // �R���|�[�l���g�ւ̎Q�Ƃ��擾
            this.animator = this.GetComponent<Animator>();
            this.cameraTransform = Camera.main.transform;
            this.lineRenderer = this.GetComponent<LineRenderer>();

            // worldCasterCenter�̏�����
            //this.worldCasterCenter = this.transform.TransformPoint(this.casterCenter);    // �肩�甭�˂���
            this.worldCasterCenter = casterCenterObj.transform.position;    // �I�u�W�F�N�g���甭�˂���

            bulletTimeCount = 5.0f;
            isBulletTime = false;

            Stop();
        }

        // Start is called before the first frame update
        void Start()
        {
            playerController = playerController.GetComponent<PlayerController.PlayerController>();
            thirdPerson = thirdPerson.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();
            userControl = userControl.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>();
        }

        // Update is called once per frame
        void Update()
        {
            // �o���b�g�^�C��
            if (isBulletTime)
            {
                Play();
                Time.timeScale = 0.01f;
                bulletTimeCount -= Time.unscaledDeltaTime;

                // �o���b�g�^�C�����I������i�f�o�b�O�j
                if (Input.GetMouseButtonDown(0))
                {
                    isBulletTime = false;
                    //Stop();
                }
            }
            else
            {
                Stop();
                Time.timeScale = 1.0f;
                bulletTimeCount = 5.0f;
            }

            if (bulletTimeCount <= 0.0f)
            {
                isBulletTime = false;
            }

            // �}�E�X���W��Ray���΂�
            //Vector3 pos = new Vector3(Input.mousePosition.x,Input.mousePosition.y,maxDistance);
            //Ray Cray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit Chit = new RaycastHit();

            //if (Input.GetMouseButtonDown(1))
            //{
            //    if (Physics.Raycast(Cray, out Chit, maxDistance))
            //    {
            //        clone = Instantiate(Crystal, Chit.point, Quaternion.identity);
            //    }
            //}

            ///  ���̎ˏo������ݒ肷��  ///
            // ��ʒ��S���琳�ʂɐL�т�Ray�����߂�
            //this.worldCasterCenter = this.transform.TransformPoint(this.casterCenter);
            this.worldCasterCenter = this.casterCenterObj != null ? this.casterCenterObj.position
               : this.transform.TransformPoint(this.casterCenter);
            var cameraForward = this.cameraTransform.forward;
            var cameraRay = new Ray(this.cameraTransform.position, cameraForward);

            // ���߂�Ray�̏Փ˓_�Ɍ�����Ray�����߂�
            var aimingRay = new Ray(
                this.worldCasterCenter,
                Physics.Raycast(cameraRay, out var focus, float.PositiveInfinity, this.interactiveLayers)
                ? focus.point - this.worldCasterCenter
                : cameraForward);


            // �ˏo������maxDistance�ȓ��̋����Ɏ����ڒ��\�ȕ��̂�����΁A�����ˏo�ł���
            if (Physics.Raycast(aimingRay, out var aimingTarget, this.maxDistance, this.interactiveLayers))
            {
                // reticle�̕\�����Ə��}�[�N�ɕς���
                this.reticle.texture = this.reticleImageValid;
                // ���˃{�^���������ꂽ��
                if (/*Input.GetButtonDown("Shot")*/Input.GetMouseButtonDown(1))
                {
                    isBulletTime = false;

                    clone = Instantiate(Crystal, aimingTarget.point, Quaternion.identity);

                    playerController.enabled = false;
                    thirdPerson.enabled = true;
                    userControl.enabled = true;

                    this.stringAnchor[1] = aimingTarget.point;  // ���̐ڒ��ʖ��[��ݒ�
                    this.casting = true;
                    //this.targetIkWeight = 1.0f;     // IK�ڕW�E�F�C�g���P�ɂ��� ... �E����ˏo�����ɐL�΂�
                    this.stringLength = Vector3.Distance(this.worldCasterCenter, aimingTarget.point);   // ���̒�����ݒ�
                    this.needsUpdateSpring = true;
                }
            }
            else
            {
                // �����ڒ��s�Ȃ�Areticle�̕\�����֎~�}�[�N��
                this.reticle.texture = this.reticleImageInValid;
            }

            // �����ˏo���̏�ԂŎ��k�{�^���������ꂽ��A���̒�����equilibrimLength�܂ŏk������
            if (this.casting && /*Input.GetButtonDown("Contract")*/Input.GetMouseButtonDown(0))
            {
                this.stringLength = this.equilibrimLength;
                this.needsUpdateSpring = true;
            }

            // ���˃{�^���������ꂽ��
            if (/*Input.GetButtonUp("Shot")*/Input.GetMouseButtonUp(1))
            {
                isBulletTime = true;

                playerController.enabled = true;
                thirdPerson.enabled = false;
                userControl.enabled = false;

                this.casting = false;
                //this.targetIkWeight = 0.0f;     // IK�ڕW�E�F�C�g��0�ɂ��� ... �E���ҋ@��Ԃɖ߂����Ƃ���
                this.needsUpdateSpring = true;

                Destroy(clone);
            }

            // �E�r��IK�E�F�C�g�����炩�ɕω�������
            //this.currentIkWeight = Mathf.SmoothDamp(
            //    this.currentIkWeight,
            //    this.targetIkWeight,
            //    ref this.ikWeightVelocity,
            //    this.ikTransitionTime);

            // ���̏�Ԃ��X�V����
            this.UpdateString();
        }

        /// <summary>
        /// ���̏�Ԃ��X�V
        /// </summary>
        private void UpdateString()
        {
            // �����ˏo���Ȃ�lineRenderer���A�N�e�B�u�ɁA�����łȂ��Ȃ�false��
            if (this.lineRenderer.enabled = this.casting)
            {
                // �����ˏo���̂ݏ���������
                // ���̃v���C���[���̖��[��ݒ�
                this.stringAnchor[0] = this.worldCasterCenter;

                // �v���C���[�Ɛڒ��ʂƂ̊Ԃɏ�Q�������邩�`�F�b�N
                //if (Physics.Linecast(this.stringAnchor[0], this.stringAnchor[1],
                //    out var obstacle, this.interactiveLayers))
                //{
                //    // ��Q��������΁A�ڒ��_����Q���ɕύX����
                //    this.stringAnchor[1] = obstacle.point;
                //    this.stringLength = Mathf.Min(
                //        Vector3.Distance(this.stringAnchor[0], this.stringAnchor[1]), this.stringLength);
                //    this.needsUpdateSpring = true;
                //}

                ///  ���̕`��ݒ�
                // ���̒[�_���m�̋�����stringLength�Ƃ̘�����ɂ���Ď���Ԃ�����
                // �����Ԃ��Ȃ�΁AstringLength���k�����Ƃ��Ă���
                this.lineRenderer.SetPositions(this.stringAnchor);
                var gbValue = Mathf.Exp(this.springJoint != null
                    ? -Mathf.Max(Vector3.Distance(this.stringAnchor[0], this.stringAnchor[1]) - this.stringLength, 0.0f)
                    : 0.0f);

                var stringColor = new Color(1.0f, gbValue, gbValue);
                this.lineRenderer.startColor = stringColor;
                this.lineRenderer.endColor = stringColor;
            }
        }

        // �E�r�̎p����ݒ肵�A�E�r���玅���o���Ă���悤�Ɍ�����
        //private void OnAnimatorIK(int layerIndex)
        //{
        //    this.animator.SetIKPosition(AvatarIKGoal.RightHand, this.stringAnchor[1]);
        //    this.animator.SetIKPositionWeight(AvatarIKGoal.RightHand, this.currentIkWeight);
        //}

        // SpringJoint�̍X�V
        private void FixedUpdate()
        {
            // �X�V�s�v�Ȃ�
            if (!this.needsUpdateSpring)
            {
                // �������Ȃ�
                return;
            }

            // �����ˏo���Ȃ�
            if (this.casting)
            {
                // SpringJoint�������Ă��Ȃ��Ƃ�
                if (this.springJoint == null)
                {
                    // ���𒣂�
                    this.springJoint = this.gameObject.AddComponent<SpringJoint>();
                    this.springJoint.autoConfigureConnectedAnchor = false;
                    this.springJoint.anchor = this.casterCenter;
                    this.springJoint.spring = this.spring;
                    this.springJoint.damper = this.damper;
                }

                // SpringJoint�̎��R���Ɛڑ����ݒ�
                this.springJoint.maxDistance = this.stringLength;
                this.springJoint.connectedAnchor = this.stringAnchor[1];
            }
            else
            {
                // �ˏo���łȂ����SpringJoint���폜���A
                // ���ɂ������ς���N����Ȃ�����
                Destroy(this.springJoint);
                this.springJoint = null;
            }

            this.needsUpdateSpring = false;
        }
    }
}