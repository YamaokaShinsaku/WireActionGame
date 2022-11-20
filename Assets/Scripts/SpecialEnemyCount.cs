using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialEnemyCount : MonoBehaviour
{
    private int count;
    public bool endFlag;

    [SerializeField]
    public GameObject[] specialEnemy;

    [SerializeField]
    private Text enemyCount;

    [SerializeField]
    private GameObject clearPanel;

    // Start is called before the first frame update
    void Start()
    {
        count = specialEnemy.Length;

        clearPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] specialEnemy = GameObject.FindGameObjectsWithTag("S_Enemy");

        count = specialEnemy.Length;

        enemyCount.text = count.ToString();

        if(count == 0)
        {
            clearPanel.SetActive(true);
            endFlag = true;
        }
    }

    public void ClosePanel()
    {
        clearPanel.SetActive(false);
    }
}
