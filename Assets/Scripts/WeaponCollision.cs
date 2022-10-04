using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // エネミーに当たったら
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("enemyHit");
            collision.gameObject.GetComponent<Renderer>().material.color = Color.black;
        }
        else
        {
            Debug.Log("elseHit");
        }

        // オブジェクトを削除
        Destroy(this.gameObject);
    }
}
