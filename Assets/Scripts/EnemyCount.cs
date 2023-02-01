using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCount : MonoBehaviour
{
    [SerializeField]
    private Text enemyCount;

    [SerializeField]
    private int enemyMax;

    [SerializeField]
    private int nowEnemy;

    [SerializeField]
    private GameObject[] specialEnemy;

    public bool endFlag;

    // Start is called before the first frame update
    void Start()
    {
        enemyMax = specialEnemy.Length;
        nowEnemy = enemyMax;
        enemyCount.text = nowEnemy.ToString();

        endFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    /// <summary>
    /// �J�E���g�e�L�X�g�̒l��ύX
    /// </summary>
    /// <param name="nowEnemyHitCount">�G�l�~�[�ɓ���������</param>
    public void changeHitCount(int nowEnemyHitCount)
    {
        nowEnemy -= nowEnemyHitCount;

        if (nowEnemy < 0)
        {
            nowEnemy = 0;

            endFlag = true;
        }

        enemyCount.text = nowEnemy.ToString();
    }
}
