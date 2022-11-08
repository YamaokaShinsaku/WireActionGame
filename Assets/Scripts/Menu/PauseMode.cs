using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMode : MonoBehaviour
{
    [SerializeField]
    private DisplayMessage displayMessage;

    [SerializeField]
    private SpiderChan.test test;

    [SerializeField]
    private PlayerController.PlayerController player;

    [SerializeField]
    private PostEffect postEffect;      // �O���[�X�P�[��

    private void Start()
    {
        displayMessage = displayMessage.GetComponent<DisplayMessage>();
        test = test.GetComponent<SpiderChan.test>();
        player = player.GetComponent<PlayerController.PlayerController>();
        postEffect = postEffect.GetComponent<PostEffect>();
    }

    private void Update()
    {
        displayMessage = displayMessage.GetComponent<DisplayMessage>();
        if (displayMessage.isMenuOpen)
        {
            Pause();
            test.enabled = false;
            // �O���[�X�P�[����on
            postEffect.enabled = true;
            //player.enabled = false;
        }
        else
        {
            Resume();
            //test.enabled = true;
            // �O���[�X�P�[����off
            postEffect.enabled = false;
            //player.enabled = true;
        }
    }

    private void Pause()
    {
        Time.timeScale = 0.01f;
    }

    private void Resume()
    {
        Time.timeScale = 1.0f;
    }
}
