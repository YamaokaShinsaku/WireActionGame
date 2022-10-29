using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemyHit : MonoBehaviour
{

    [SerializeField]
    private EnemyCount enemyCount;

    private int hitCount;

    // Start is called before the first frame update
    void Start()
    {
        enemyCount = enemyCount.GetComponent<EnemyCount>();

        hitCount = 0;
    }

    private void OnCollisionEnter(Collision other)
    {

        if (this.gameObject.transform.parent.name == "S_Enemy")
        {
            Debug.Log("SpecialEnemy");
            hitCount++;
            enemyCount.changeHitCount(hitCount);
        }
    }
}
