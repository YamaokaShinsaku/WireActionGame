using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickShot : MonoBehaviour
{
    public GameObject[] shotObj;    // ��΂�����
    public int count;               // ���˃J�E���g
    private GameObject clone;       // ����̃N���[��
    public Vector3[] position;
    public Quaternion[] rotation;

    public Vector3[] offset;
    public Transform target;

    private float angle;
    //�@��]����X�s�[�h
    [SerializeField]
    private float rotateSpeed = 180f;
    //�@�^�[�Q�b�g����̋���
    [SerializeField]
    private Vector3[] distanceFromTarget;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;

        for (int i = 0; i < shotObj.Length; i++)
        {
            shotObj[i].GetComponent<Homing_2>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ���W�X�V
        for (int i = 0; i < shotObj.Length; i++)
        {
            //position[i] = shotObj[i].transform.position;
            //rotation[i] = shotObj[i].transform.rotation;

            //position[i] = target.position + offset[i];
            //shotObj[i].transform.position = position[i];

            //�@���j�b�g�̈ʒu = �^�[�Q�b�g�̈ʒu �{ �^�[�Q�b�g���猩�����j�b�g�̊p�x �~�@�^�[�Q�b�g����̋���
            shotObj[i].transform.position = target.position + Quaternion.Euler(0f, angle, 0f) * distanceFromTarget[i];
            //�@���j�b�g���g�̊p�x = �^�[�Q�b�g���猩�����j�b�g�̕����̊p�x���v�Z����������j�b�g�̊p�x�ɐݒ肷��
            shotObj[i].transform.rotation = 
                Quaternion.LookRotation(shotObj[i].transform.position - 
                new Vector3(target.position.x, shotObj[i].transform.position.y, target.position.z), Vector3.up);
            //�@���j�b�g�̊p�x��ύX
            angle += rotateSpeed * Time.deltaTime;
            //�@�p�x��0�`360�x�̊ԂŌJ��Ԃ�
            angle = Mathf.Repeat(angle, 360f);
        }


        // ���킪�c���Ă���Ƃ�
        if (count < shotObj.Length)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                clone = Instantiate(shotObj[count], shotObj[count].transform.position, shotObj[count].transform.rotation);    // ����̃N���[���𐶐�
                shotObj[count].SetActive(false);        // ����{�̂��\����
                clone.GetComponent<Homing_2>().enabled = true;
                count++;

                // 5�b��ɃN���[�����폜
                Destroy(clone, 5f);
            }
        }
        else
        {
            count = 0;
        }

        //�@����𑕓U����
        if (Input.GetKeyDown(KeyCode.K))
        {
            for (int i = 0; i < shotObj.Length; i++)
            {
                shotObj[i].SetActive(true);        // ����{�̂��\����
            }
        }
    }
}
