using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasicShot : MonoBehaviour
{
    public GameObject shotObj;       // ��΂�����
    public int count;               // ���˃J�E���g
    private GameObject clone;       // ����̃N���[��

    [SerializeField]
    private GameObject cloneCreatePosition;

    public Vector3 position;
    public Quaternion rotation;

    public Vector3 offset;
    public Transform target;

    private float angle;
    //�@��]����X�s�[�h
    [SerializeField]
    private float rotateSpeed = 180f;
    //�@�^�[�Q�b�g����̋���
    [SerializeField]
    private Vector3 distanceFromTarget;

    private float beforeTrigger;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        shotObj.GetComponent<Homing_2>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ���W�X�V
        //�@���j�b�g�̈ʒu = �^�[�Q�b�g�̈ʒu �{ �^�[�Q�b�g���猩�����j�b�g�̊p�x �~�@�^�[�Q�b�g����̋���
        shotObj.transform.position = target.position + Quaternion.Euler(0f, angle, 0f) * distanceFromTarget;
        //�@���j�b�g���g�̊p�x = �^�[�Q�b�g���猩�����j�b�g�̕����̊p�x���v�Z����������j�b�g�̊p�x�ɐݒ肷��
        shotObj.transform.rotation =
            Quaternion.LookRotation(shotObj.transform.position -
            new Vector3(target.position.x, shotObj.transform.position.y, target.position.z), Vector3.up);
        //�@���j�b�g�̊p�x��ύX
        angle += rotateSpeed * Time.deltaTime;
        //�@�p�x��0�`360�x�̊ԂŌJ��Ԃ�
        angle = Mathf.Repeat(angle, 360f);


        float rightTrigger = Input.GetAxis("magicShot");

        if (Input.GetKeyDown(KeyCode.J) || rightTrigger > 0 && beforeTrigger == 0.0f)
        {
            clone = Instantiate(shotObj, cloneCreatePosition.transform.position, cloneCreatePosition.transform.rotation);    // ����̃N���[���𐶐�
            //shotObj.SetActive(false);        // ����{�̂��\����
            clone.GetComponent<Homing_2>().enabled = true;
            count++;

            // 5�b��ɃN���[�����폜
            Destroy(clone, 5f);
        }

        beforeTrigger = rightTrigger;

        //�@����𑕓U����
        //if (Input.GetKeyDown(KeyCode.K) || Input.GetButtonDown("magicSet"))
        //{
        //    shotObj.SetActive(true);        // ����{�̂��\����
        //}
    }
}