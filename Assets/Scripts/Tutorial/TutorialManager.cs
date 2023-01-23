using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialManager : MonoBehaviour
{
    // チュートリアル用UI
    public RectTransform tutorialTextArea;
    public Text tutorialTitle;
    public Text tutorialText;

    // チュートリアルタスク
    private ITutorialTask currentTask;
    private List<ITutorialTask> tutorialTask;

    // チュートリアル表示フラグ
    private bool isEnabled;

    // タスクの条件を満たした歳の遷移用フラグ
    private bool task_executed = false;

    // チュートリアル表示時のUI移動距離
    private float fadePosX = 700;

    void Start()
    {
        // チュートリアルの一覧
        tutorialTask = new List<ITutorialTask>()
        {
            new CameraMovementTask(),
            new MovementTask(),
            new JumpTask(),
            new WireActionTask(),
            new WeaponChangeTask(),
            new MagicShotTask(),
            new MenuTask(),
        };

        // 最初のチュートリアルを設定
        SetCurrentTask(tutorialTask[0]);

        isEnabled = true;
    }

    void Update()
    {
        // チュートリアルが存在し、実行されていない場合
        if(currentTask != null && !task_executed)
        {
            // 現在のチュートリアルが実行されたか判定
            if (currentTask.CheckTask())
            {
                task_executed = true;

                // UIアニメーション
                iTween.MoveTo(tutorialTextArea.gameObject, iTween.Hash(
                    "position", tutorialTextArea.transform.localPosition + new Vector3(fadePosX, 0, 0),
                    "time", 1f));

                // 終了したタスクをリストから削除
                tutorialTask.RemoveAt(0);

                var nextTask = tutorialTask.FirstOrDefault();
                if(nextTask != null)
                {
                    // 遅延実行
                    DOVirtual.DelayedCall(1.0f,
                        ()=>{
                            SetCurrentTask(tutorialTask[0]);
                        });
                    //SetCurrentTask(tutorialTask[0]);
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            SwitchEnabled();
        }
    }

    /// <summary>
    /// 新しいチュートリアルタスクを設定
    /// </summary>
    /// <param name="task">タスク</param>
    /// <param name="time">待機時間</param>
    /// <returns></returns>
    protected void SetCurrentTask(ITutorialTask task, float time = 0)
    {
        currentTask = task;
        task_executed = false;

        // UIにタイトルと説明文を設定
        tutorialTitle.text = task.GetTitle();
        tutorialText.text = task.GetText();

        Debug.Log(tutorialTitle.text);
        Debug.Log(tutorialText.text);

        // タスク設定時の関数を実行
        task.OnTaskSetting();

        // UIアニメーション
        iTween.MoveTo(tutorialTextArea.gameObject,iTween.Hash(
           "position", tutorialTextArea.transform.localPosition  - new Vector3(fadePosX, 0, 0),
                "time", 1f));
    }

    /// <summary>
    /// チュートリアルの有効・無効の切り替え
    /// </summary>
    protected void SwitchEnabled()
    {
        isEnabled = !isEnabled;

        // UIの表示切り替え
        float alpha = isEnabled ? 1.0f : 0;
        tutorialTextArea.GetComponent<CanvasGroup>().alpha = alpha;
    }
}
