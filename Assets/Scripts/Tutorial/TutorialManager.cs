using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialManager : MonoBehaviour
{
    // �`���[�g���A���pUI
    public RectTransform tutorialTextArea;
    public Text tutorialTitle;
    public Text tutorialText;

    // �`���[�g���A���^�X�N
    private ITutorialTask currentTask;
    private List<ITutorialTask> tutorialTask;

    // �`���[�g���A���\���t���O
    private bool isEnabled;

    // �^�X�N�̏����𖞂������΂̑J�ڗp�t���O
    private bool task_executed = false;

    // �`���[�g���A���\������UI�ړ�����
    private float fadePosX = 700;

    void Start()
    {
        // �`���[�g���A���̈ꗗ
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

        // �ŏ��̃`���[�g���A����ݒ�
        SetCurrentTask(tutorialTask[0]);

        isEnabled = true;
    }

    void Update()
    {
        // �`���[�g���A�������݂��A���s����Ă��Ȃ��ꍇ
        if(currentTask != null && !task_executed)
        {
            // ���݂̃`���[�g���A�������s���ꂽ������
            if (currentTask.CheckTask())
            {
                task_executed = true;

                // UI�A�j���[�V����
                iTween.MoveTo(tutorialTextArea.gameObject, iTween.Hash(
                    "position", tutorialTextArea.transform.localPosition + new Vector3(fadePosX, 0, 0),
                    "time", 1f));

                // �I�������^�X�N�����X�g����폜
                tutorialTask.RemoveAt(0);

                var nextTask = tutorialTask.FirstOrDefault();
                if(nextTask != null)
                {
                    // �x�����s
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
    /// �V�����`���[�g���A���^�X�N��ݒ�
    /// </summary>
    /// <param name="task">�^�X�N</param>
    /// <param name="time">�ҋ@����</param>
    /// <returns></returns>
    protected void SetCurrentTask(ITutorialTask task, float time = 0)
    {
        currentTask = task;
        task_executed = false;

        // UI�Ƀ^�C�g���Ɛ�������ݒ�
        tutorialTitle.text = task.GetTitle();
        tutorialText.text = task.GetText();

        Debug.Log(tutorialTitle.text);
        Debug.Log(tutorialText.text);

        // �^�X�N�ݒ莞�̊֐������s
        task.OnTaskSetting();

        // UI�A�j���[�V����
        iTween.MoveTo(tutorialTextArea.gameObject,iTween.Hash(
           "position", tutorialTextArea.transform.localPosition  - new Vector3(fadePosX, 0, 0),
                "time", 1f));
    }

    /// <summary>
    /// �`���[�g���A���̗L���E�����̐؂�ւ�
    /// </summary>
    protected void SwitchEnabled()
    {
        isEnabled = !isEnabled;

        // UI�̕\���؂�ւ�
        float alpha = isEnabled ? 1.0f : 0;
        tutorialTextArea.GetComponent<CanvasGroup>().alpha = alpha;
    }
}
