using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMode : Pauser
{
    [SerializeField]
    private DisplayMessage displayMessage;

    [SerializeField]
    private PostEffect postEffect;      // グレースケール

    private void Start()
    {
        displayMessage = displayMessage.GetComponent<DisplayMessage>();
        postEffect = postEffect.GetComponent<PostEffect>();
    }

    private void Update()
    {
        displayMessage = displayMessage.GetComponent<DisplayMessage>();
        if (displayMessage.isMenuOpen)
        {
            Pauser.Pause();
            // グレースケールをon
            postEffect.enabled = true;
        }
        else
        {
            Pauser.Resume();
            // グレースケールをoff
            postEffect.enabled = false;

            //Pauser.Resume();

        }
    }

}
