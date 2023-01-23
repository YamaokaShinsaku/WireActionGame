using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementTask : ITutorialTask
{
    public string GetTitle()
    {
        return "��{����\n�J�����ړ�";
    }

    public string GetText()
    {
        return "�E�X�e�B�b�N�Ŏ��_�ړ�";
    }

    public void OnTaskSetting()
    {
        
    }

    public bool CheckTask()
    {
        float axis_H = Input.GetAxis("Horizontal2");
        float axis_V = Input.GetAxis("Vertical2");

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
