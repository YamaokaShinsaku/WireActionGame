using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireActionTask2 : ITutorialTask
{
    public string GetTitle()
    {
        return "基本操作\nワイヤーアクション(2/2)";
    }

    public string GetText()
    {
        return "停止中にもう一度RBを押すと\nワイヤーを発射\nLTを押すと停止時間終了";
    }

    public void OnTaskSetting()
    {

    }

    public bool CheckTask()
    {
        if (Input.GetButtonDown("WireShot"))
        {
            return true;
        }

        return false;
    }

    public float GetTransitionTime()
    {
        return 20.0f;
    }
}
