using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respown : MonoBehaviour
{
    [SerializeField]
    private Transform respownPosition;      // ���X�|�[�����W

    [SerializeField]
    public GameObject player;              // �v���C���[

    [SerializeField]
    private bool isDeth;                  // ����ł��邩�ǂ���

    [SerializeField]
    private GameObject respownEffect;   // ���X�|�[���G�t�F�N�g

    private GameObject clone;
    private float castTime = 2.0f;
    private bool castFlag;

    // Start is called before the first frame update
    void Start()
    {
        isDeth = false;
        respownEffect.SetActive(false);
        castFlag = false;

        clone = respownEffect;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDeth)
        {
            clone = Instantiate(respownEffect,respownPosition);
            clone.SetActive(true);
            player.transform.position = respownPosition.position;

            castFlag = true;
            isDeth = false;
        }

        if(castFlag)
        {
            castTime -= Time.deltaTime;
            if(castTime <= 0.0f)
            {
                castTime = 2.0f;
                castFlag = false;
                Destroy(clone);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "DethZone")
        {
            isDeth = true;
        }

        if(collision.gameObject.tag == "Enemy")
        {
            isDeth = true;
        }
    }
}
