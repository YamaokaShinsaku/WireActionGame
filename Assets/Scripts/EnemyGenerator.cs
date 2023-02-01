using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyObj;

    [SerializeField]
    private float interval;
    [SerializeField]
    private float deltaTime;
    //[SerializeField]
    //private int nowCount;
    [SerializeField]
    private int maxCount;

    [SerializeField]
    private LockOnTarget.LockOnTarget lockOn;

    // Start is called before the first frame update
    void Start()
    {
        lockOn = lockOn.GetComponent<LockOnTarget.LockOnTarget>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckObjectsVisible();
        deltaTime += Time.deltaTime;

        if (deltaTime > interval)
        {
            foreach(GameObject enemy in enemyObj)
            {
                if(enemy.activeInHierarchy == false)
                {
                    enemy.SetActive(true);
                }
            }
            deltaTime = 0.0f;

            //nowCount++;
        }
    }

    public void CheckObjectsVisible()
    {
        for(int i = 0; i < enemyObj.Length;i++)
        {
            if(enemyObj[i].activeInHierarchy == false)
            {
                lockOn.enabled = false;
            }
            else
            {
                lockOn.enabled = true;
            }
        }
    }
}
