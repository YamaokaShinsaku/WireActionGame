using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShotTask : ITutorialTask
{
    public string GetTitle()
    {
        return "Šî–{‘€ì\n–‚–@”­Ë";
    }

    public string GetText()
    {
        return "LT‚Å–‚–@”­Ë";
    }

    public void OnTaskSetting()
    {

    }

    public bool CheckTask()
    {
        float rightTrigger = Input.GetAxis("magicShot");

        if (rightTrigger > 0)
        {
            return true;
        }

            return false;
    }

    public float GetTransitionTime()
    {
        return 2.0f;
    }
}
