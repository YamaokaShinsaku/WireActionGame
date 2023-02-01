using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��{�ړ��`���[�g���A��
/// </summary>
public class MovementTask : ITutorialTask
{
    public string GetTitle()
    {
        return "��{����\n�ړ�";
    }

    public string GetText()
    {
        return "���X�e�B�b�N�ňړ�";
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
