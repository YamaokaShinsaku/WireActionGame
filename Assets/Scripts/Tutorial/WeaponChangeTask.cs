using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// íØèÖ¦Ì`[gA
/// </summary>
public class WeaponChangeTask : ITutorialTask
{
    public string GetTitle()
    {
        return "î{ì\nõÏX";
    }

    public string GetText()
    {
        return "LBÅõØèÖ¦";
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
        return 5.0f;
    }
}
