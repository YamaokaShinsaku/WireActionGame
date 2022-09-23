using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    ParticleSystem particle;

    void Start()
    {
        particle = this.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (particle.isStopped) //パーティクルが終了したか判別
        {
            Destroy(this.gameObject);   //パーティクル用ゲームオブジェクトを削除
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("HitOther");
        if (collision.other.tag == "Player")
        {
            Debug.Log("HitStage");
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("OtherHit");
    }

}
