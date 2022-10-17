using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public ClickShot clickShot;        // ショットオブジェクト

        public bool isWire;
        public bool isShot;

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

                    test.bulletTimeCount = 0.0f;
                }
                else
                {
                    weapons[i].SetActive(false);
                    test.enabled = true;
                    clickShot.enabled = false;

                    isWire = true;
                    isShot = false;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            // 武器を切り替える
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
                    }
                }
            }
        }
    }

}