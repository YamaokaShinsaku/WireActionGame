using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMode : Pauser
{
    [SerializeField]
    private DisplayMessage displayMessage;

    [SerializeField]
    private PostEffect postEffect;

    [SerializeField]
    ButtonClick buttonClick;

    public bool canWeaponChange;

    private void Start()
    {
        displayMessage = displayMessage.GetComponent<DisplayMessage>();
        postEffect = postEffect.GetComponent<PostEffect>();
        buttonClick = buttonClick.GetComponent<ButtonClick>();

        canWeaponChange = true;
    }

    private void Update()
    {
        displayMessage = displayMessage.GetComponent<DisplayMessage>();
        if (displayMessage.isMenuOpen)
        {
            Pauser.Pause();

            postEffect.enabled = true;

            canWeaponChange = false;
        }
        else
        {
            Pauser.Resume();

            postEffect.enabled = false;
            //player.enabled = true;

            buttonClick.noPush();
            canWeaponChange = true;
        }
    }

    private void Pause()
    {
        if(buttonClick.sw)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0.01f;
        }
    }

    private void Resume()
    {
        Time.timeScale = 1.0f;
    }
}
