using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器切り替えのチュートリアル
/// </summary>
public class WeaponChangeTask : ITutorialTask
{
    public string GetTitle()
    {
        return "基本操作\n装備変更";
    }

    public string GetText()
    {
        return "LBで装備切り替え";
    }

    public void OnTaskSetting()
    {

    }

    public bool CheckTask()
    {
        if (Input.GetButtonDown("WeaponChange"))
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
