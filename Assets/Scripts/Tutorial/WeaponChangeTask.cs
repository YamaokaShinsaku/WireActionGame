using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����؂�ւ��̃`���[�g���A��
/// </summary>
public class WeaponChangeTask : ITutorialTask
{
    public string GetTitle()
    {
        return "��{����\n�����ύX";
    }

    public string GetText()
    {
        return "LB�ő����؂�ւ�";
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
