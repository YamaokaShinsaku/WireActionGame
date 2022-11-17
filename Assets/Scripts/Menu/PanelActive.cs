using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelActive : MonoBehaviour
{
    [SerializeField]
    public GameObject messagePanel;    // ���b�Z�[�W�v���t�@�u

    [SerializeField]
    private PostEffect postEffect;      // �O���[�X�P�[��

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

    private void Pause()
    {
        isPause = true;
        Time.timeScale = 0.0f;
    }

    private void Resume()
    {
        isPause = false;
        Time.timeScale = 1.0f;
    }
}
