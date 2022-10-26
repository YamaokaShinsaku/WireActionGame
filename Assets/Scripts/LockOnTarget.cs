using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���b�N�I��
/// </summary>
namespace LockOnTarget
{
    public class LockOnTarget : MonoBehaviour
    {
        private GameObject nearObj;         //�ł��߂��I�u�W�F�N�g
        private float searchTime = 0;    //�o�ߎ���

        [SerializeField]
        private Camera targetCamera;
        [SerializeField]
        public Transform target;
        [SerializeField]
        private Transform targetUI;
        [SerializeField]
        private Vector3 worldOffset;

        private RectTransform parentUI;

        // Use this for initialization
        void Start()
        {
            //�ł��߂������I�u�W�F�N�g���擾
            nearObj = serchTag(gameObject, "Enemy");

            target = nearObj.transform;
        }

        public void Initialize(Transform target_, Camera targetCamera = null)
        {
            target = target_;
            targetCamera = targetCamera != null ? targetCamera : Camera.main;

            OnUpdatePosition();
        }

        private void Awake()
        {
            // �J�������w�肳��Ă��Ȃ���΃��C���J�����ɂ���
            if (targetCamera == null)
            {
                targetCamera = Camera.main; 
            }

            // �eUI��RectTransform��ێ�
            parentUI = targetUI.parent.GetComponent<RectTransform>();
        }

        // Update is called once per frame
        void Update()
        {
            target = nearObj.transform;
            OnUpdatePosition();

            //�o�ߎ��Ԃ��擾
            searchTime += Time.deltaTime;

            if (searchTime >= 1.0f)
            {
                //�ł��߂������I�u�W�F�N�g���擾
                nearObj = serchTag(gameObject, "Enemy");

                // ���O��\��
                //Debug.Log(nearObj.name);

                //�o�ߎ��Ԃ�������
                searchTime = 0;
            }

            //�Ώۂ̈ʒu�̕���������
            transform.LookAt(nearObj.transform);

            //�������g�̈ʒu���瑊�ΓI�Ɉړ�����
            //transform.Translate(Vector3.forward * 0.01f);
        }

        //�w�肳�ꂽ�^�O�̒��ōł��߂����̂��擾
        public GameObject serchTag(GameObject nowObj, string tagName)
        {
            float tmpDis = 0;           //�����p�ꎞ�ϐ�
            float nearDis = 0;          //�ł��߂��I�u�W�F�N�g�̋���
            //string nearObjName = "";    //�I�u�W�F�N�g����
            GameObject targetObj = null; //�I�u�W�F�N�g

            //�^�O�w�肳�ꂽ�I�u�W�F�N�g��z��Ŏ擾����
            foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
            {
                //���g�Ǝ擾�����I�u�W�F�N�g�̋������擾
                tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

                //�I�u�W�F�N�g�̋������߂����A����0�ł���΃I�u�W�F�N�g�����擾
                //�ꎞ�ϐ��ɋ������i�[
                if (nearDis == 0 || nearDis > tmpDis)
                {
                    nearDis = tmpDis;
                    //nearObjName = obs.name;
                    targetObj = obs;
                }

            }
            //�ł��߂������I�u�W�F�N�g��Ԃ�
            //return GameObject.Find(nearObjName);
            return targetObj;
        }

        // UI�̈ʒu���X�V����
        private void OnUpdatePosition()
        {
            var cameraTransform = targetCamera.transform;

            // �J�����̌����x�N�g��
            var cameraDir = cameraTransform.forward;
            // �I�u�W�F�N�g�̈ʒu
            var targetWorldPos = target.position + worldOffset;
            // �J��������^�[�Q�b�g�ւ̃x�N�g��
            var targetDir = targetWorldPos - cameraTransform.position;

            // ���ς��g���ăJ�����O�����ǂ����𔻒�
            var isFront = Vector3.Dot(cameraDir, targetDir) > 0;

            // �J�����O���Ȃ�UI�\���A����Ȃ��\��
            targetUI.gameObject.SetActive(isFront);
            if (!isFront) return;

            // �I�u�W�F�N�g�̃��[���h���W���X�N���[�����W�ϊ�
            var targetScreenPos = targetCamera.WorldToScreenPoint(targetWorldPos);

            // �X�N���[�����W�ϊ���UI���[�J�����W�ϊ�
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentUI,
                targetScreenPos,
                null,
                out var uiLocalPos
            );

            // RectTransform�̃��[�J�����W���X�V
            targetUI.localPosition = uiLocalPos;
        }
    }
}