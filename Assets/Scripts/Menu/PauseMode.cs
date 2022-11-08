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
    private PostEffect postEffect;      // グレースケール

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
            // グレースケールをon
            postEffect.enabled = true;
            //player.enabled = false;
        }
        else
        {
            Resume();
            //test.enabled = true;
            // グレースケールをoff
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
