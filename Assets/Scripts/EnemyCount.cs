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

    // Start is called before the first frame update
    void Start()
    {
        nowEnemy = enemyMax;
        enemyCount.text = nowEnemy.ToString();
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    /// <summary>
    /// カウントテキストの値を変更
    /// </summary>
    /// <param name="nowEnemyHitCount">エネミーに当たった回数</param>
    public void changeHitCount(int nowEnemyHitCount)
    {
        nowEnemy -= nowEnemyHitCount;

        enemyCount.text = nowEnemy.ToString();
    }
}
