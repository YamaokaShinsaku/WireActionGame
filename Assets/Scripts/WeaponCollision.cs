using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject hitEffect;

    private GameObject cloneEffect;

    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip audioClip;

    private bool isAudioEnd;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isAudioEnd = false;
        audioSource.clip = audioClip;
    }

    private void Update()
    {
        // 再生が終わったら
        if (!audioSource.isPlaying && isAudioEnd)
        {
            Destroy(cloneEffect);
            Destroy(this.gameObject);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        // �G�l�~�[�ɓ���������
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("enemyHit");
            //collision.gameObject.GetComponent<Renderer>().material.color = Color.black;
            cloneEffect = Instantiate(hitEffect, this.transform.position, this.transform.rotation);

            audioSource.Play();
            isAudioEnd = true;

            // ����I�u�W�F�N�g��폜
            //Destroy(this.gameObject);

        }
        else
        {
            Debug.Log("elseHit");
        }
    }
}
