using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MasicShotTask2 : ITutorialTask
{
    public string GetTitle()
    {
        return "�`���[�g���A���I��";
    }

    public string GetText()
    {
        return "����Ɋ���Ă�����\n���j���[����^�C�g����";
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
