using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TimeScale�𐧌䂷�邽�߂̃N���X
/// </summary>
public class TimeScaleManager : MonoBehaviour
{
    public bool isPause;    // ��~
    //public bool isResume;   // �ĊJ

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            isPause = true;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            isPause = false;
        }

        if(isPause)
        {
            Pause();
        }
        else
        {
            Resume();
        }

    }

    /// <summary>
    /// ��~������
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0.0f;
    }

    /// <summary>
    /// �ĊJ����
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1.0f;
    }
}
