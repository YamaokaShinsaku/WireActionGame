using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Attack : MonoBehaviour
{
    //private Animator animator;

    //[SerializeField]
    //ParticleSystem particle;

    [SerializeField]
    private GameObject bullet;
    //�@�e���΂���
    [SerializeField]
    private float shotPower = 1000f;

    public bool responFlag;

    private Transform firstPos;

    private GameObject bulletIns;
    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        responFlag = false;

        firstPos = bullet.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            //animator.SetBool("Attack", true);
            bulletIns = Instantiate(bullet);

            //�@���j�b�g�̈ʒu�Ɗp�x�ɒe�̃C���X�^���X�𐶐�
            //var bulletIns = Instantiate(bullet, transform.position, firstPos.rotation);
            //�@�e��Rigidbody�ɗ͂������ă��j�b�g�̌����Ă�������ɔ�΂�
            bulletIns.GetComponent<Rigidbody>().AddForce(transform.forward * shotPower, ForceMode.Force);
            //�@5�b��ɍ폜
            Destroy(bulletIns, 5f);

        }
    }

    //public void Blade_Play()
    //{
    //    particle.Play();
    //}

    //public void Blade_Stop()
    //{
    //    particle.Stop();
    //}
}
