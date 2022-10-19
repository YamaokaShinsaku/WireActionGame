using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // �G�l�~�[�ɓ���������
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("enemyHit");
            collision.gameObject.GetComponent<Renderer>().material.color = Color.black;

            // ����I�u�W�F�N�g���폜
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("elseHit");
            //Destroy(this.gameObject);
        }

        // �I�u�W�F�N�g���폜
        //Destroy(this.gameObject);
    }
}
