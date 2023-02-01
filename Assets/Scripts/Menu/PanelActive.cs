using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelActive : MonoBehaviour
{
    [SerializeField]
    public GameObject messagePanel;    // メッセージプレファブ

    [SerializeField]
    private PostEffect postEffect;      // グレースケール

    [SerializeField]
    private GameObject[] Image;

    [SerializeField]
    private GameObject reticleCanvas;

    [SerializeField]
    private SpecialEnemyCount specialEnemy;

    public bool canWeaponChange;
    public bool isPause;

    // Start is called before the first frame update
    void Start()
    {
        messagePanel.SetActive(false);
        postEffect = postEffect.GetComponent<PostEffect>();

        canWeaponChange = true;
        isPause = false;
    }

    // Update is called once per frame
    public void Update()
    {
        specialEnemy = specialEnemy.GetComponent<SpecialEnemyCount>();
        if (Input.GetButtonDown("MenuOpenButton"))
        {
            canWeaponChange = false;
            Pause();
            postEffect.enabled = true;
            messagePanel.SetActive(true);
        }
        if (Input.GetButtonDown("MenuCloseButton"))
        {
            canWeaponChange = true;
            Resume();
            postEffect.enabled = false;
            messagePanel.SetActive(false);
        }
    }

    public void Pause()
    {
        isPause = true;
        Time.timeScale = 0.0f;
        reticleCanvas.SetActive(false);

        //for (int i = 0; i < Image.Length; i++)
        //{
        //    Image[i].SetActive(false);
        //}
    }

    public void Resume()
    {
        isPause = false;
        Time.timeScale = 1.0f;
        reticleCanvas.SetActive(true);
        //for (int i = 0; i < Image.Length; i++)
        //{
        //    Image[i].SetActive(true);
        //}
    }
}
