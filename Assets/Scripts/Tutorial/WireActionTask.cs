using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireActionTask : ITutorialTask
{
    public string GetTitle()
    {
        return "��{����@�@���C���[�A�N�V����";
    }

    public string GetText()
    {
        return "RB�Ń��C���[�𔭎�";
    }

    public void OnTaskSetting()
    {

    }

    public bool CheckTask()
    {
        if(Input.GetButtonDown("WireShot"))
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
