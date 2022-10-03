using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickShot : MonoBehaviour
{
    public GameObject[] shotObj;    // ��΂�����
    public int count;               // ���˃J�E���g
    private GameObject clone;       // ����̃N���[��
    private float nowPos;

    public Vector3[] offset;
    public Transform target;

    public GameObject lookTarget;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // ���킪�c���Ă���Ƃ�
        if (count < shotObj.Length)
        {

            // ���킪�v���C���[��Ǐ]����悤�ɂ���
            for (int i = 0; i < shotObj.Length; i++)
            {
                // �ʒu�̌v�Z
                Vector3 position = shotObj[i].transform.position;

                position.y = offset[i].y + Mathf.PingPong(Time.time / 3, 0.3f);
                position.z = shotObj[i].transform.position.z;

                shotObj[i].transform.position = target.transform.position + offset[i];

                // �����̌v�Z
                Vector3 direction = lookTarget.transform.position - shotObj[i].transform.position;
                direction.y = 0;
                
                Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
                lookRotation.x = 1.0f;
                
                shotObj[i].transform.rotation = Quaternion.Lerp(shotObj[i].transform.rotation, lookRotation, 0.1f);

                //Debug.Log(shotObj[i].transform.rotation);
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                clone = Instantiate(shotObj[count]);    // ����̃N���[���𐶐�
                shotObj[count].SetActive(false);        // ����{�̂��\����
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 dir = ray.direction;            // ��΂�����
                clone.GetComponent<Rigidbody>().AddForce(dir * 3000);   // �}�E�X�̈ʒu�ɔ�΂�

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
                shotObj[i].SetActive(true);
            }
        }
    }
}
