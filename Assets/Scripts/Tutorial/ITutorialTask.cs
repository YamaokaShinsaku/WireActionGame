using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITutorialTask
{
    /// <summary>
    /// チュートリアルのタイトルを取得
    /// </summary>
    /// <returns>チュートリアルのタイトル</returns>
    string GetTitle();

    /// <summary>
    /// 説明文を取得
    /// </summary>
    /// <returns>説明文</returns>
    string GetText();

    /// <summary>
    /// チュートリアルタスクが設定された際に実行
    /// </summary>
    void OnTaskSetting();

    /// <summary>
    /// チュートリアルが達成されたかを判定
    /// </summary>
    /// <returns>達成されたかどうか</returns>
    bool CheckTask();

    /// <summary>
    /// 次のタスクへ遷移するための時間
    /// </summary>
    /// <returns>時間（秒）</returns>
    float GetTransitionTime();
}
