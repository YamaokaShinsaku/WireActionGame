using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respown : MonoBehaviour
{
    [SerializeField]
    private Transform respownPosition;      // リスポーン座標

    [SerializeField]
    public GameObject player;              // プレイヤー

    [SerializeField]
    private SpiderChan.test test;

    [SerializeField]
    private bool isDeth;                  // 死んでいるかどうか

    [SerializeField]
    private GameObject respownEffect;   // リスポーンエフェクト

    [SerializeField]
    private  WeaponChanger.WeaponController weapon;

    [SerializeField]
    private PanelActive panelActive;

    [SerializeField]
    private Animator anim;

    private GameObject clone;
    private float castTime = 2.0f;
    private bool castFlag;

    public PlayerController.PlayerController playerController;
    public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter thirdPerson;
    public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl userControl;

    // Start is called before the first frame update
    void Start()
    {
        test = test.GetComponent<SpiderChan.test>();
        weapon = weapon.GetComponent<WeaponChanger.WeaponController>();
        panelActive = panelActive.GetComponent<PanelActive>();

        anim = anim.GetComponent<Animator>();

        playerController = playerController.GetComponent<PlayerController.PlayerController>();
        thirdPerson = thirdPerson.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();
        userControl = userControl.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>();

        test.enabled = false;
        
        isDeth = false;
        respownEffect.SetActive(false);
        castFlag = false;

        clone = respownEffect;
    }

    // Update is called once per frame
    void Update()
    {
        panelActive = panelActive.GetComponent<PanelActive>();

        if (isDeth)
        {
            clone = Instantiate(respownEffect,respownPosition);
            clone.SetActive(true);
            player.transform.position = respownPosition.position;

            castFlag = true;
            isDeth = false;

            thirdPerson.m_Rigidbody.velocity = new Vector3(0,0,0);

            anim.SetFloat("Forward", 0.0f);
            anim.SetFloat("Turn", 0.0f);
            anim.SetFloat("Jump", 0.0f);
            anim.SetFloat("JumpLeg", 0.0f);
            anim.SetBool("OnGround", true);

            playerController.isJumping = false;

            // ワイヤーアクション中に死んだ際の処理
            if (test.springJoint)
            {
                Destroy(test.springJoint);
            }
            if(test.lineRenderer.enabled)
            {
                test.lineRenderer.enabled = false;
            }
            Destroy(test.clone);
            test.casting = false;
            test.isBulletTime = false;
            test.motionBlur.enabled = false;
            test.Stop();
            test.bulletTimeCount = 0.0f;
            playerController.enabled = true;
            thirdPerson.enabled = false;
            userControl.enabled = false;
            test.enabled = false;
        }
        
        if(panelActive.isPause == true)
        {
            test.enabled = false;
        }

        if(isDeth == false && weapon.isWire && panelActive.isPause == false && test.enabled == false)
        {
            test.enabled = true;
        }

        if (castFlag)
        {
            playerController.enabled = false;
            castTime -= Time.deltaTime;
            if(castTime <= 0.0f && weapon.isWire)
            {
                playerController.enabled = true;
                test.enabled = true;
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
