using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 基本移動チュートリアル
/// </summary>
public class MovementTask : ITutorialTask
{
    public string GetTitle()
    {
        return "基本操作\n移動";
    }

    public string GetText()
    {
        return "左スティックで移動";
    }

    public void OnTaskSetting()
    {
        
    }

    public bool CheckTask()
    {
        float axis_H = Input.GetAxis("Horizontal");
        float axis_V = Input.GetAxis("Vertical");

        if(0 < axis_V || 0 < axis_H)
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
