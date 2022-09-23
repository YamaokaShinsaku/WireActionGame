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
        if (particle.isStopped) //�p�[�e�B�N�����I������������
        {
            Destroy(this.gameObject);   //�p�[�e�B�N���p�Q�[���I�u�W�F�N�g���폜
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
