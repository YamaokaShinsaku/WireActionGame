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

            // 武器オブジェクトを削除
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("elseHit");
            //Destroy(this.gameObject);
        }

        // オブジェクトを削除
        //Destroy(this.gameObject);
    }
}
