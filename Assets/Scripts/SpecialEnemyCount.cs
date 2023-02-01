using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialEnemyCount : MonoBehaviour
{
    public int count;
    public bool endFlag;

    [SerializeField]
    public GameObject[] specialEnemy;

    [SerializeField]
    private Text enemyCount;

    [SerializeField]
    private Text clearText;

    [SerializeField]
    private Text menuText;

    public bool a_flag;
    public float a_color;

    // Start is called before the first frame update
    void Start()
    {
        count = specialEnemy.Length;

        clearText.enabled = false;
        menuText.enabled = false;

        a_flag = false;
        a_color = 5;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] specialEnemy = GameObject.FindGameObjectsWithTag("S_Enemy");

        count = specialEnemy.Length;

        enemyCount.text = count.ToString();

        if(count == 0)
        {         
            endFlag = true;
        }

        if(endFlag == true)
        {
            clearText.enabled = true;
            menuText.enabled = true;

            a_flag = true;

            if (a_flag)
            {
                //テキストの透明度を変更する
                clearText.color = new Color(clearText.color.r, clearText.color.g, clearText.color.b, a_color);
                menuText.color = new Color(menuText.color.r, menuText.color.g, menuText.color.b, a_color);
                a_color -= Time.deltaTime;
                //透明度が0になったら終了する。
                if (a_color < 0)
                {
                    a_color = 0;
                    a_flag = false;
                }
            }
        }
    }

    public void TextActive()
    {
        clearText.enabled = true;
        menuText.enabled = true;

        a_flag = true;
        a_color = 5;

        if (a_flag)
        {
            //テキストの透明度を変更する
            clearText.color = new Color(0, 0, 0, a_color);
            menuText.color = new Color(0, 0, 0, a_color);
            a_color -= Time.deltaTime;
            //透明度が0になったら終了する。
            if (a_color < 0)
            {
                a_color = 0;
                a_flag = false;
            }
        }
    }

    public void ClosePanel()
    {
        
        Debug.Log("closePanel");
    }
}
