using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TimeScaleを制御するためのクラス
/// </summary>
public class TimeScaleManager : MonoBehaviour
{
    public bool isPause;    // 停止
    //public bool isResume;   // 再開

    [SerializeField]
    private PostEffect postEffect;      // グレースケール

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
            // グレースケールをon
            postEffect.enabled = true;
            Pause();
        }
        else
        {
            // グレースケールをoff
            postEffect.enabled = false;
            Resume();
        }

    }

    /// <summary>
    /// 停止させる
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0.0f;
    }

    /// <summary>
    /// 再開する
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1.0f;
    }
}
