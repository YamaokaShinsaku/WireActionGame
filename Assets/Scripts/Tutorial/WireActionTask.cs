using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���C���[����̃`���[�g���A��
/// </summary>
public class WireActionTask : ITutorialTask
{
    public string GetTitle()
    {
        return "��{����\n���C���[�A�N�V����(1/2)";
    }

    public string GetText()
    {
        return "RB�Ń��C���[�𔭎�\n���C���[���������5�b�Ԓ�~";
    }

    public void OnTaskSetting()
    {

    }

    public bool CheckTask()
    {
        if(Input.GetButtonDown("WireShot"))
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
