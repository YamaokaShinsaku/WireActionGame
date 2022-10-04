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
        }
        else
        {
            Debug.Log("elseHit");
        }

        // �I�u�W�F�N�g���폜
        Destroy(this.gameObject);
    }
}
