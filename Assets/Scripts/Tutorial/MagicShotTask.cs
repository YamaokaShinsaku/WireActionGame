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
        return "基本操作\n魔法発射(1/2)";
    }

    public string GetText()
    {
        return "RTで魔法発射（4発）\n4発打った後RBで再装填";
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
        return 20.0f;
    }
}
