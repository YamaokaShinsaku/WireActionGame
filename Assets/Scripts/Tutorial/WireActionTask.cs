using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireActionTask : ITutorialTask
{
    public string GetTitle()
    {
        return "基本操作　　ワイヤーアクション";
    }

    public string GetText()
    {
        return "RBでワイヤーを発射";
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
