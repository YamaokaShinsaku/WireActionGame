using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireActionTask2 : ITutorialTask
{
    public string GetTitle()
    {
        return "��{����\n���C���[�A�N�V����(2/2)";
    }

    public string GetText()
    {
        return "��~���ɂ�����xRB��������\n���C���[�𔭎�\nLT�������ƒ�~���ԏI��";
    }

    public void OnTaskSetting()
    {

    }

    public bool CheckTask()
    {
        if (Input.GetButtonDown("WireShot"))
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
