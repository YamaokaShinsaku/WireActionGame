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
        return "��{����\n���@����(1/2)";
    }

    public string GetText()
    {
        return "RT�Ŗ��@���ˁi4���j\n4���ł�����RB�ōđ��U";
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
        return 20.0f;
    }
}
