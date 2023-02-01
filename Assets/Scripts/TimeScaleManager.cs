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

    [SerializeField]
    private PostEffect postEffect;      // �O���[�X�P�[��

    private void Start()
    {
        postEffect = postEffect.GetComponent<PostEffect>();
    }

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
            // �O���[�X�P�[����on
            postEffect.enabled = true;
            Pause();
        }
        else
        {
            // �O���[�X�P�[����off
            postEffect.enabled = false;
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
