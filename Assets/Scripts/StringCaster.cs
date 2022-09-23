using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StringPlayer
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(LineRenderer))]
    public class StringCaster : MonoBehaviour
    {
        [SerializeField]
        private float maxDistance = 100.0f; // ����L�΂���ő勗��

        [SerializeField]
        private LayerMask interactiveLayers;    // �������������郌�C���[

        [SerializeField]
        private Vector3 casterCenter = new Vector3(0.0f, 0.5f, 0.0f);   // �I�u�W�F�N�g�̃��[�J�����W�ł���킵�����̎ˏo�ʒu

        [SerializeField]
        private Transform casterCenterObj;     // ���̎ˏo�ʒu�̃I�u�W�F�N�g

        [SerializeField]
        [Min(0.0f)] private float obstacleDetectionMarginStart = 1.0f; // �����ɏ\���߂����͎̂��Օ����肩�珜�O������
        
        [SerializeField]
        [Min(0.0f)] private float obstacleDetectionMarginEnd = 1.0f; // �ڒ��_�ɏ\���߂����͎̂��Օ����肩�珜�O������

        [SerializeField]
        private float spring = 50.0f;       // ���̕���������S������SpringJoint��Spring

        [SerializeField]
        private float damper = 20.0f;       // ���̕���������S������SpringJoint��damper

        [SerializeField]
        private float equilibrimLength = 1.0f;     // �����k�߂����̎��R��

        [SerializeField]
        private AvatarIKGoal ikGoal = AvatarIKGoal.RightHand;

        [SerializeField]
        private float ikTransitionTime = 0.5f;      // �r�̈ʒu�̑J�ڎ���

        [SerializeField]
        private RawImage reticle;       // ���𒣂�邩�ǂ����̏�Ԃɍ��킹�āA�Ə��}�[�N��ύX����

        [SerializeField]
        private Texture reticleImageValid;      // �Ə��}�[�N

        [SerializeField]
        private Texture reticleImageInValid;    // �֎~�}�[�N

        [SerializeField]
        private ParticleSystem particle;    // �G�t�F�N�g�i�W�����j

        [SerializeField]
        private float bulletTimeCount;         // �o���b�g�^�C���̐�������

        [SerializeField]
        bool BulletTime;        // �o���b�g�^�C�������ǂ���

        [SerializeField]
        private GameObject Crystal;

        private GameObject clone;

        private Animator animator;
        private Camera mainCamera;
        private LineRenderer lineRenderer;
        private SpringJoint springJoint;

        // �E���L�΂��A�߂�������X���[�Y�ɂ��邽��
        private float currentIkWeight;  // ���݂̃E�F�C�g
        private float targetIkWeight;   // �ڕW�E�F�C�g
        private float ikWeightVelocity; // �E�F�C�g�ω���

        private bool casting;               // �����ˏo�����ǂ���
        private bool needsUpdateSpring;     // FixedUpdate����SpringJoint�̏�Ԃ��K�v���ǂ���
        private float stringLength;         // ���݂̎��̒���...�����FixedUpdate����SpringJoint��maxDistance�ɃZ�b�g����
        //private readonly Vector3[] stringAnchor = new Vector3[2];   // SpringJoint�̃v���C���[���Ɛڒ��ʑ��̖��[
        private readonly Anchor[] stringAnchor = new Anchor[2];     // ���[����RigidBody����������
        private Vector3 worldCasterCenter;   // casterCenter�����[���h���W�ɕϊ���������

        // �A���J�[���`
        private readonly struct Anchor
        {
            public readonly Vector3 LocalPosition;
            public readonly Rigidbody ConnectedBody;

            public Vector3 WorldPosition => this.ConnectedBody == null
                ? this.LocalPosition
                : this.ConnectedBody.transform.TransformPoint(this.LocalPosition);

            public Anchor(Vector3 worldPosition, Rigidbody connectedBody = null)
            {
                this.LocalPosition = connectedBody == null
                    ? worldPosition
                    : connectedBody.transform.TransformPoint(worldPosition);
                this.ConnectedBody = connectedBody;
            }

        }

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
            this.mainCamera = Camera.main;
            this.lineRenderer = this.GetComponent<LineRenderer>();

            // worldCasterCenter�̏�����
            //this.worldCasterCenter = this.transform.TransformPoint(this.casterCenter);
            this.worldCasterCenter = casterCenterObj.transform.position;

            bulletTimeCount = 5.0f;
            BulletTime = false;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            /// �o���b�g�^�C���̐ݒ� ///
            if (BulletTime)
            {
                Play();
                Time.timeScale = 0.01f;
                bulletTimeCount -= Time.unscaledDeltaTime;

                // �o���b�g�^�C�����I������i�f�o�b�O�j
                if (Input.GetMouseButtonDown(0))
                {
                    BulletTime = false;
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
                BulletTime = false;
            }
            ///----------------------///

            ///  ���̎ˏo������ݒ肷��  ///
            // ��ʒ��S���琳�ʂɐL�т�Ray�����߂�
            //this.worldCasterCenter = this.transform.TransformPoint(this.casterCenter);
            //var cameraForward = this.cameraTransform.forward;
            //var cameraRay = new Ray(this.cameraTransform.position, cameraForward);

            // ���߂�Ray�̏Փ˓_�Ɍ�����Ray�����߂�
            //var aimingRay = new Ray(
            //    this.worldCasterCenter,
            //    Physics.Raycast(cameraRay, out var focus, float.PositiveInfinity, this.interactiveLayers)
            //    ? focus.point - this.worldCasterCenter
            //    : cameraForward);

            // �ˏo������maxDistance�ȓ��̋����Ɏ����ڒ��\�ȕ��̂�����΁A�����ˏo�ł���
            //if (Physics.Raycast(aimingRay, out var aimingTarget, this.maxDistance, this.interactiveLayers))
            //{
            //    // reticle�̕\�����Ə��}�[�N�ɕς���
            //    this.reticle.texture = this.reticleImageValid;
            //    // ���˃{�^���������ꂽ��
            //    if (/*Input.GetButtonDown("Shot")*/Input.GetMouseButtonDown(1))
            //    {
            //        BulletTime = false;

            //        this.stringAnchor[1] = aimingTarget.point;  // ���̐ڒ��ʖ��[��ݒ�
            //        this.casting = true;
            //        this.targetIkWeight = 1.0f;     // IK�ڕW�E�F�C�g���P�ɂ��� ... �E����ˏo�����ɐL�΂�
            //        // ���̒�����ݒ�
            //        this.stringLength = Vector3.Distance(this.worldCasterCenter, aimingTarget.point);
            //        this.needsUpdateSpring = true;
            //    }
            //}
            //else
            //{
            //    // �����ڒ��s�Ȃ�Areticle�̕\�����֎~�}�[�N��
            //    this.reticle.texture = this.reticleImageInValid;
            //}

            // �}�E�X���W�ɃN���X�^���𐶐�
            //Vector3 pos = new Vector3(Input.mousePosition.x,Input.mousePosition.y,maxDistance);
            Ray Cray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Chit = new RaycastHit();

            if(Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(Cray,out Chit))
                {
                    clone = Instantiate(Crystal, Chit.point, Quaternion.identity);
                }
            }

            this.worldCasterCenter = this.casterCenterObj != null ? this.casterCenterObj.position
                : this.transform.TransformPoint(this.casterCenter);
            var mousePosition = Input.mousePosition;
            var pointerRay = this.mainCamera.ScreenPointToRay(mousePosition);
            var aimingRay = new Ray(
                this.worldCasterCenter,
                Physics.Raycast(pointerRay, out var focus, float.PositiveInfinity, this.interactiveLayers)
                ? focus.point - this.worldCasterCenter
                : pointerRay.direction);

            // ���e�B�N���̈ʒu���}�E�X�|�C���^�[�̈ʒu��
            var reticleTransform = this.reticle.transform as RectTransform;
            var reticleRootCanvas = reticle.canvas.rootCanvas;
            if(RectTransformUtility.ScreenPointToLocalPointInRectangle(
                reticleTransform.parent as RectTransform,
                mousePosition,
                reticleRootCanvas.renderMode == RenderMode.ScreenSpaceOverlay 
                ? null
                : reticleRootCanvas.worldCamera,
                out var reticlePosition))
            {
                reticleTransform.localPosition = reticlePosition;
            }

            if (Physics.Raycast(aimingRay, out var aimingTarget, this.maxDistance, this.interactiveLayers))
            {
                // reticle�̕\�����Ə��}�[�N�ɕς���
                this.reticle.texture = this.reticleImageValid;
                // ���˃{�^���������ꂽ��
                if (/*Input.GetButtonDown("Shot")*/Input.GetMouseButtonDown(1))
                {
                    BulletTime = false;

                    this.stringAnchor[1] = new Anchor(aimingTarget.point, aimingTarget.rigidbody);
                    this.casting = true;
                    this.targetIkWeight = 1.0f;     // IK�ڕW�E�F�C�g���P�ɂ��� ... �E����ˏo�����ɐL�΂�
                    // ���̒�����ݒ�
                    this.stringLength = Vector3.Distance(this.worldCasterCenter, aimingTarget.point);
                    this.needsUpdateSpring = true;
                }
            }
            else
            {
                // �����ڒ��s�Ȃ�Areticle�̕\�����֎~�}�[�N��
                this.reticle.texture = this.reticleImageInValid;
            }

            // �����ˏo���ɂ̏�ԂŎ��k�{�^���������ꂽ��A���̒�����equilibrimLength�܂ŏk������
            if (this.casting && /*Input.GetButtonDown("Contract")*/Input.GetMouseButtonDown(0))
            {
                this.stringLength = this.equilibrimLength;
                this.needsUpdateSpring = true;
            }

            // ���˃{�^���������ꂽ��
            if (/*Input.GetButtonUp("Shot")*/Input.GetMouseButtonUp(1))
            {
                BulletTime = true;

                this.casting = false;
                this.targetIkWeight = 0.0f;     // IK�ڕW�E�F�C�g��0�ɂ��� ... �E���ҋ@��Ԃɖ߂����Ƃ���
                this.needsUpdateSpring = true;

                Destroy(clone);
            }

            // �E�r��IK�E�F�C�g�����炩�ɕω�������
            this.currentIkWeight = Mathf.SmoothDamp(
                this.currentIkWeight,
                this.targetIkWeight,
                ref this.ikWeightVelocity,
                this.ikTransitionTime);

            // ���̏�Ԃ��X�V����
            this.UpdateString();
        }

        /// <summary>
        /// ���̏�Ԃ��X�V
        /// </summary>
        private void UpdateString()
        {
            // �����ˏo���Ȃ�lineRenderer���A�N�e�B�u�ɁA�����łȂ��Ȃ�false��
            //if (this.lineRenderer.enabled = this.casting)
            //{
            //    // �����ˏo���̂ݏ���������
            //    // ���̃v���C���[���̖��[��ݒ�
            //    this.stringAnchor[0] = this.worldCasterCenter;

            //    // �v���C���[�Ɛڒ��ʂƂ̊Ԃɏ�Q�������邩�`�F�b�N
            //    if (Physics.Linecast(this.stringAnchor[0], this.stringAnchor[1],
            //        out var obstacle, this.interactiveLayers))
            //    {
            //        // ��Q��������΁A�ڒ��_����Q���ɕύX����
            //        this.stringAnchor[1] = obstacle.point;
            //        this.stringLength = Mathf.Min(
            //            Vector3.Distance(this.stringAnchor[0], this.stringAnchor[1]), this.stringLength);
            //        this.needsUpdateSpring = true;
            //    }

                ///  ���̕`��ݒ�
                // ���̒[�_���m�̋�����stringLength�Ƃ̘�����ɂ���Ď���Ԃ�����
                // �����Ԃ��Ȃ�΁AstringLength���k�����Ƃ��Ă���
            //    this.lineRenderer.SetPositions(this.stringAnchor);
            //    var gbValue = Mathf.Exp(this.springJoint != null
            //        ? -Mathf.Max(Vector3.Distance(this.stringAnchor[0],
            //        this.stringAnchor[1]) - this.stringLength, 0.0f)
            //        : 0.0f);

            //    var stringColor = new Color(1.0f, gbValue, gbValue);
            //    this.lineRenderer.startColor = stringColor;
            //    this.lineRenderer.endColor = stringColor;
            //}

            if(this.lineRenderer.enabled = this.casting)
            {
                // �����ˏo���̂ݏ���������
                // ���̃v���C���[���̖��[��ݒ�
                this.stringAnchor[0] = new Anchor(this.worldCasterCenter);

                var anchorPositionStart = this.stringAnchor[0].WorldPosition;
                var anchorPositionEnd = this.stringAnchor[1].WorldPosition;

                if(this.stringLength > (this.obstacleDetectionMarginStart + this.obstacleDetectionMarginEnd))
                {
                    var start = Vector3.MoveTowards(
                        anchorPositionEnd,
                        anchorPositionStart,
                        this.stringLength - this.obstacleDetectionMarginStart);

                    var end = Vector3.MoveTowards(
                        anchorPositionStart,
                        anchorPositionEnd,
                        this.stringLength - this.obstacleDetectionMarginEnd);

                    // �v���C���[�Ɛڒ��ʂƂ̊Ԃɏ�Q�������邩�`�F�b�N
                    if (Physics.Linecast(start, end, out var obstacle, this.interactiveLayers))
                    {
                        // ��Q��������΁A�ڒ��_����Q���ɕύX����
                        this.stringAnchor[1] = new Anchor(obstacle.point, obstacle.rigidbody);
                        anchorPositionEnd = this.stringAnchor[1].WorldPosition;

                        this.stringLength = Mathf.Min(
                            Vector3.Distance(anchorPositionStart, anchorPositionEnd), this.stringLength);
                        this.needsUpdateSpring = true;
                    }
                }

                ///  ���̕`��ݒ�
                // ���̒[�_���m�̋�����stringLength�Ƃ̘�����ɂ���Ď���Ԃ�����
                // �����Ԃ��Ȃ�΁AstringLength���k�����Ƃ��Ă���
                this.lineRenderer.SetPosition(0, anchorPositionStart);
                this.lineRenderer.SetPosition(1, anchorPositionEnd);
                var gbValue = Mathf.Exp(
                    this.springJoint != null
                    ? -Mathf.Max(Vector3.Distance(anchorPositionStart, anchorPositionEnd)
                    - this.stringLength, 0.0f)
                    : 0.0f);

                var stringColor = new Color(1.0f, gbValue, gbValue);
                this.lineRenderer.startColor = stringColor;
                this.lineRenderer.endColor = stringColor;
            }
        }

        // �E�r�̎p����ݒ肵�A�E�r���玅���o���Ă���悤�Ɍ�����
        private void OnAnimatorIK(int layerIndex)
        {
            //this.animator.SetIKPosition(AvatarIKGoal.RightHand, this.stringAnchor[1]);
            //this.animator.SetIKPositionWeight(AvatarIKGoal.RightHand, this.currentIkWeight);

            this.animator.SetIKPosition(this.ikGoal, this.stringAnchor[1].WorldPosition);
            this.animator.SetIKPositionWeight(this.ikGoal, this.currentIkWeight);
        }

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
                    //this.springJoint.anchor = this.casterCenter;
                    //this.springJoint.anchor = this.casterCenterObj.position;
                    this.springJoint.spring = this.spring;
                    this.springJoint.damper = this.damper;
                }

                this.springJoint.anchor = this.transform.InverseTransformPoint(this.worldCasterCenter);
                this.springJoint.maxDistance = this.stringLength;

                // SpringJoint�̎��R���Ɛڑ����ݒ�
                //this.springJoint.maxDistance = this.stringLength;
                //this.springJoint.connectedAnchor = this.stringAnchor[1];

                this.springJoint.connectedBody = this.stringAnchor[1].ConnectedBody;
                this.springJoint.connectedAnchor = this.stringAnchor[1].LocalPosition;
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