using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ジャンプのチュートリアル
/// </summary>
public class JumpTask : ITutorialTask
{
    public string GetTitle()
    {
        return "基本操作\nジャンプ";
    }

    public string GetText()
    {
        return "Aボタンでジャンプ\n入力時間で高さが変わる";
    }

    public void OnTaskSetting()
    {

    }

    public bool CheckTask()
    {
        if (Input.GetButtonDown("Jump"))
        {
            return true;
        }

        return false;
    }

    public float GetTransitionTime()
    {
        return 5.0f;
    }
}
