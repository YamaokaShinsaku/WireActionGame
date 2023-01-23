using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 魔法発射のチュートリアル
/// </summary>
public class MagicShotTask : ITutorialTask
{
    public string GetTitle()
    {
        return "基本操作\n魔法発射";
    }

    public string GetText()
    {
        return "LTで魔法発射";
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
