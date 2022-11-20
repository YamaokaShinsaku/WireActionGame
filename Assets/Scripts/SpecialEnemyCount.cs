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
    private GameObject clearPanel;
    [SerializeField]
    private GameObject reticleCanvas;
    [SerializeField]
    private PostEffect postEffect;      // グレースケール


    private GameObject clone;
    public PanelActive panelActive;

    // Start is called before the first frame update
    void Start()
    {
        postEffect = postEffect.GetComponent<PostEffect>();
        count = specialEnemy.Length;

        panelActive = panelActive.GetComponent<PanelActive>();

        clearPanel.SetActive(false);

        var parent = this.transform;
        clone = Instantiate(clearPanel,clearPanel.transform.position,clearPanel.transform.rotation,parent);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] specialEnemy = GameObject.FindGameObjectsWithTag("S_Enemy");
        panelActive = panelActive.GetComponent<PanelActive>();

        count = specialEnemy.Length;

        enemyCount.text = count.ToString();

        if(count == 0)
        {         
            endFlag = true;
        }

        if(endFlag == true)
        {
            OpenPanel();
        }
    }

    public void OpenPanel()
    {
        panelActive.isPause = true;
        Time.timeScale = 0.0f;
        postEffect.enabled = true;
        reticleCanvas.SetActive(false);
        count = specialEnemy.Length - 1;
        if(clone)
        {
            clone.SetActive(true);
        }
        endFlag = false;
    }

    public void ClosePanel()
    {
        panelActive.isPause = false;
        Time.timeScale = 1.0f;
        postEffect.enabled = false;
        reticleCanvas.SetActive(true);
        Destroy(clone);
        Debug.Log("closePanel");
    }
}
