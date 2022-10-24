using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public ClickShot clickShot;        // �V���b�g�I�u�W�F�N�g

        public bool isWire;
        public bool isShot;
        public ParticleSystem magicCircle;

        // Start is called before the first frame update
        void Start()
        {
            test = test.GetComponent<SpiderChan.test>();
            clickShot = clickShot.GetComponent<ClickShot>();

            for (int i = 0; i < weapons.Length; i++)
            {
                if (i == currentNum)
                {
                    weapons[i].SetActive(true);
                    test.enabled = false;
                    clickShot.enabled = true;

                    isWire = false;
                    isShot = true;

                    magicCircle.Play();

                    test.bulletTimeCount = 0.0f;
                }
                else
                {
                    weapons[i].SetActive(false);
                    test.enabled = true;
                    clickShot.enabled = false;

                    isWire = true;
                    isShot = false;

                    magicCircle.Stop();
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            // �����؂�ւ���
            if (Input.GetKeyDown(KeyCode.L))
            {
                currentNum = (currentNum + 1) % weapons.Length;

                for (int i = 0; i < weapons.Length; i++)
                {
                    if (i == currentNum)
                    {
                        weapons[i].SetActive(true);
                        test.enabled = false;
                        clickShot.enabled = true;

                        isWire = false;
                        isShot = true;

                        magicCircle.Play();

                        test.isBulletTime = false;
                        test.bulletTimeCount = 0.0f;
                        Time.timeScale = 1.0f;
                    }
                    else
                    {
                        weapons[i].SetActive(false);
                        test.enabled = true;
                        clickShot.enabled = false;

                        isWire = true;
                        isShot = false;

                        magicCircle.Stop();
                    }
                }
            }
        }
    }

}