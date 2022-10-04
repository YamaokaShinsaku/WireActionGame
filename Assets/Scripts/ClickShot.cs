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

    public GameObject lookTarget;
    private Vector3 prevPosition;
    FixedJoint component;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 diff = shotObj[0].transform.position - prevPosition;
        ////prevPosition = shotObj[0].transform.position;
        ////if (diff.magnitude > 0.01f)
        ////{
        ////    //shotObj[0].transform.rotation = Quaternion.LookRotation(diff);
        ////    shotObj[0].transform.eulerAngles = new Vector3(90, Quaternion.LookRotation(diff).y, 0);
        ////}

        //Quaternion rot = Quaternion.AngleAxis(-lookTarget.transform.rotation.y, Vector3.forward);
        //Quaternion q = shotObj[0].transform.rotation;
        //shotObj[0].transform.rotation = Quaternion.RotateTowards(shotObj[0].transform.rotation, q * rot, 5);

        // ���W�X�V
        for (int i = 0; i < shotObj.Length; i++)
        {
            position[i] = shotObj[i].transform.position;
            rotation[i] = shotObj[i].transform.rotation;
        }

        // ���킪�c���Ă���Ƃ�
        if (count < shotObj.Length)
        {
            if (component == null)
            {
                component = shotObj[0].AddComponent<FixedJoint>();
                component.connectedBody = lookTarget.GetComponent<Rigidbody>();
            }
            // ���킪�v���C���[��Ǐ]����悤�ɂ���
            for (int i = 0; i < shotObj.Length; i++)
            {
                // �ʒu�̌v�Z
                //shotObj[i].transform.position = target.transform.position + offset[i];
                
                // �����̌v�Z
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                Destroy(component);
                if (component == null)
                {
                    clone = Instantiate(shotObj[count], position[count], rotation[count]);    // ����̃N���[���𐶐�
                    shotObj[count].SetActive(false);        // ����{�̂��\����
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Vector3 dir = ray.direction;            // ��΂�����
                    clone.GetComponent<Rigidbody>().AddForce(dir * 3000);   // �}�E�X�̈ʒu�ɔ�΂�
                    count++;

                    // 5�b��ɃN���[�����폜
                    Destroy(clone, 5f);
                }
                //clone = Instantiate(shotObj[count], position[count], rotation[count]);    // ����̃N���[���𐶐�
                //shotObj[count].SetActive(false);        // ����{�̂��\����
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //Vector3 dir = ray.direction;            // ��΂�����
                //clone.GetComponent<Rigidbody>().AddForce(dir * 3000);   // �}�E�X�̈ʒu�ɔ�΂�
                //count++;

                //// 5�b��ɃN���[�����폜
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
                shotObj[i].SetActive(true);
            }
        }
    }
}
