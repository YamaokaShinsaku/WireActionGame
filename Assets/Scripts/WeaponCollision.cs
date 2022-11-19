using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject hitEffect;

    void OnCollisionEnter(Collision collision)
    {
        // �G�l�~�[�ɓ���������
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("enemyHit");
            //collision.gameObject.GetComponent<Renderer>().material.color = Color.black;
            Instantiate(hitEffect, this.transform.position, this.transform.rotation);

            // ����I�u�W�F�N�g���폜
            Destroy(this.gameObject);
            Destroy(hitEffect);

        }
        else
        {
            Debug.Log("elseHit");
        }
    }
}
