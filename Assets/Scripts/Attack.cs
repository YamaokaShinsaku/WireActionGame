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
    //　弾を飛ばす力
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

            //　ユニットの位置と角度に弾のインスタンスを生成
            //var bulletIns = Instantiate(bullet, transform.position, firstPos.rotation);
            //　弾のRigidbodyに力を加えてユニットの向いている方向に飛ばす
            bulletIns.GetComponent<Rigidbody>().AddForce(transform.forward * shotPower, ForceMode.Force);
            //　5秒後に削除
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
