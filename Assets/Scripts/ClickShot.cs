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
            position[i] = shotObj[i].transform.position;
            rotation[i] = shotObj[i].transform.rotation;

            position[i] = target.position + offset[i];
            shotObj[i].transform.position = position[i];
        }


        // ���킪�c���Ă���Ƃ�
        if (count < shotObj.Length)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                clone = Instantiate(shotObj[count], position[count], rotation[count]);    // ����̃N���[���𐶐�
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
