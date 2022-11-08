using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����؂�ւ�
/// </summary>
namespace WeaponChanger
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] weapons;   // �����A�C�e��

        [SerializeField]
        private int currentNum = 0;     // �A�C�e���ԍ�

        public SpiderChan.test test;       // ���C���[�X�N���v�g
        //public ClickShot clickShot;        // �V���b�g�I�u�W�F�N�g
        public MasicShot masicShot;

        [SerializeField]
        private GameObject idleMagicFire;

        public bool isWire;
        public bool isShot;
        public ParticleSystem magicCircle;      // ���@�w�G�t�F�N�g

        [SerializeField]
        private GameObject reticle;


        // Start is called before the first frame update
        void Start()
        {
            test = test.GetComponent<SpiderChan.test>();
            //clickShot = clickShot.GetComponent<ClickShot>();
            masicShot = masicShot.GetComponent<MasicShot>();

            for (int i = 0; i < weapons.Length; i++)
            {
                if (i == currentNum)
                {
                    weapons[i].SetActive(true);
                    test.enabled = false;
                    //clickShot.enabled = true;
                    masicShot.enabled = true;

                    isWire = false;
                    isShot = true;

                    reticle.SetActive(false);

                    idleMagicFire.SetActive(true);
                    magicCircle.Play();

                    test.bulletTimeCount = 0.0f;
                }
                else
                {
                    weapons[i].SetActive(false);
                    test.enabled = true;
                    //clickShot.enabled = false;
                    masicShot.enabled = false;

                    isWire = true;
                    isShot = false;

                    reticle.SetActive(true);

                    idleMagicFire.SetActive(false);
                    magicCircle.Stop();
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            // �����؂�ւ���
            if (Input.GetKeyDown(KeyCode.L)
                || Input.GetButtonDown("WeaponChange") && test.isBulletTime == false)
            {
                currentNum = (currentNum + 1) % weapons.Length;

                for (int i = 0; i < weapons.Length; i++)
                {
                    if (i == currentNum)
                    {
                        //weapons[i].SetActive(true);
                        //clickShot.enabled = true;
                        test.enabled = false;
                        masicShot.enabled = true;

                        reticle.SetActive(false);

                        isWire = false;
                        isShot = true;

                        idleMagicFire.SetActive(true);
                        magicCircle.Play();

                        test.isBulletTime = false;
                        test.Stop();
                        test.bulletTimeCount = 0.0f;
                        Time.timeScale = 1.0f;
                    }
                    else
                    {
                        //weapons[i].SetActive(false);
                        //clickShot.enabled = false;
                        test.enabled = true;
                        masicShot.enabled = false;

                        reticle.SetActive(true);

                        isWire = true;
                        isShot = false;

                        idleMagicFire.SetActive(false);
                        magicCircle.Stop();
                    }
                }
            }
        }
    }

}