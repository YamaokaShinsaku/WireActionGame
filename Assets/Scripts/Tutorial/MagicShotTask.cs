using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���@���˂̃`���[�g���A��
/// </summary>
public class MagicShotTask : ITutorialTask
{
    public string GetTitle()
    {
        return "��{����\n���@����";
    }

    public string GetText()
    {
        return "LT�Ŗ��@����";
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
        return 2.0f;
    }
}
