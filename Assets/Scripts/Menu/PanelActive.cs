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

    [SerializeField]
    private TimeScaleManager timeScale;

    public bool canWeaponChange;
    public bool isPause;

    // Start is called before the first frame update
    void Start()
    {
        messagePanel.SetActive(false);
        postEffect = postEffect.GetComponent<PostEffect>();
        timeScale = timeScale.GetComponent<TimeScaleManager>();

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
            //Pause();
            timeScale.isPause = true;
            postEffect.enabled = true;
            messagePanel.SetActive(true);
        }
        if (Input.GetButtonDown("MenuCloseButton"))
        {
            canWeaponChange = true;
            //Resume();
            timeScale.isPause = false;
            postEffect.enabled = false;
            messagePanel.SetActive(false);
        }
    }
}
