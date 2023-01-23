using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 武器切り替え
/// </summary>
namespace WeaponChanger
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] weapons;   // 装備アイテム

        [SerializeField]
        private int currentNum = 0;     // アイテム番号

        public SpiderChan.test test;       // ワイヤースクリプト
        public MasicShot magicShot;

        [SerializeField]
        private GameObject idleMagicFire;

        public bool isWire;
        public bool isShot;
        public ParticleSystem magicCircle;      // 魔法陣エフェクト

        [SerializeField]
        private GameObject reticle;

        [SerializeField]
        private PanelActive pauseMode;

        // Start is called before the first frame update
        void Start()
        {
            test = test.GetComponent<SpiderChan.test>();
            magicShot = magicShot.GetComponent<MasicShot>();
            pauseMode = pauseMode.GetComponent<PanelActive>();

            for (int i = 0; i < weapons.Length; i++)
            {
                if (i == currentNum)
                {
                    weapons[i].SetActive(true);
                    test.enabled = false;
                    magicShot.enabled = true;

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
                    magicShot.enabled = false;

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
            pauseMode = pauseMode.GetComponent<PanelActive>();

            // 武器を切り替える
            if (Input.GetKeyDown(KeyCode.L)
                || Input.GetButtonDown("WeaponChange") 
                && test.isBulletTime == false
                && pauseMode.canWeaponChange == true)
            {
                currentNum = (currentNum + 1) % weapons.Length;

                for (int i = 0; i < weapons.Length; i++)
                {
                    if (i == currentNum)
                    {
                        test.enabled = false;
                        magicShot.enabled = true;

                        reticle.SetActive(false);

                        isWire = false;
                        isShot = true;

                        idleMagicFire.SetActive(true);
                        magicCircle.Play();
                        magicShot.count = 0;
                        idleMagicFire.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                        test.isBulletTime = false;
                        test.Stop();
                        test.bulletTimeCount = 0.0f;
                        Time.timeScale = 1.0f;
                    }
                    else
                    {
                        test.enabled = true;
                        magicShot.enabled = false;

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