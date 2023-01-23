using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTask : ITutorialTask
{
    public string GetTitle()
    {
        return "基本操作\nメニュー";
    }

    public string GetText()
    {
        return "X : メニューを開く\nY : メニューを閉じる";
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
