using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �W�����v�̃`���[�g���A��
/// </summary>
public class JumpTask : ITutorialTask
{
    public string GetTitle()
    {
        return "��{����\n�W�����v";
    }

    public string GetText()
    {
        return "A�{�^���ŃW�����v\n���͎��Ԃō������ς��";
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
