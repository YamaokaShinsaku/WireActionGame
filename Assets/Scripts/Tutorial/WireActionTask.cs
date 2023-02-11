using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ワイヤー操作のチュートリアル
/// </summary>
public class WireActionTask : ITutorialTask
{
    public string GetTitle()
    {
        return "基本操作\nワイヤーアクション(1/2)";
    }

    public string GetText()
    {
        return "RBでワイヤーを発射\nワイヤーが消えると5秒間停止";
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
        return 20.0f;
    }
}
