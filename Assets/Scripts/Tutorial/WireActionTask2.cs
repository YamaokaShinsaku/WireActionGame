using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireActionTask2 : ITutorialTask
{
    public string GetTitle()
    {
        return "î{ì\nC[ANV(2/2)";
    }

    public string GetText()
    {
        return "â~Éà¤êxRBð·Æ\nC[ð­Ë\nLTð·Æâ~ÔI¹";
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
        return 15.0f;
    }
}
