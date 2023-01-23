using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTask : ITutorialTask
{
    public string GetTitle()
    {
        return "��{����\n���j���[";
    }

    public string GetText()
    {
        return "X : ���j���[���J��\nY : ���j���[�����";
    }

    public void OnTaskSetting()
    {

    }

    public bool CheckTask()
    {
       if(Input.GetButtonDown("MenuOpenButton"))
       {
            return true;
       }
        if (Input.GetButtonDown("MenuCloseButton"))
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
