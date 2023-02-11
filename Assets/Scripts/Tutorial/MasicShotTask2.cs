using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MasicShotTask2 : ITutorialTask
{
    public string GetTitle()
    {
        return "チュートリアル終了";
    }

    public string GetText()
    {
        return "操作に慣れてきたら\nメニューからタイトルへ";
    }

    public void OnTaskSetting()
    {

    }

    public bool CheckTask()
    {
        return false;
    }

    public float GetTransitionTime()
    {
        return 10.0f;
    }
}
