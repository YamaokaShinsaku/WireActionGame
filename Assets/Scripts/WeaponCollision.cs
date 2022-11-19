using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject hitEffect;

    void OnCollisionEnter(Collision collision)
    {
        // エネミーに当たったら
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("enemyHit");
            //collision.gameObject.GetComponent<Renderer>().material.color = Color.black;
            Instantiate(hitEffect, this.transform.position, this.transform.rotation);

            // 武器オブジェクトを削除
            Destroy(this.gameObject);
            Destroy(hitEffect);

        }
        else
        {
            Debug.Log("elseHit");
        }
    }
}
