using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    // �`���[�g���A���pUI
    public RectTransform tutorialTextArea;
    public Text tutorialTitle;
    public Text tutorialText;

    // �`���[�g���A���^�X�N
    public ITutorialTask currentTask;
    [SerializeField]
    public List<ITutorialTask> tutorialTask;

    // �`���[�g���A���\���t���O
    public bool isEnabled;

    // �^�X�N�̏����𖞂������΂̑J�ڗp�t���O
    public bool task_executed = false;

    // �`���[�g���A���\������UI�ړ�����
    private float fadePosX = 350;

    void Start()
    {
        // �`���[�g���A���̈ꗗ
        tutorialTask = new List<ITutorialTask>()
        {
            new CameraMovementTask(),
            new MovementTask(),
            new WireActionTask(),
        };

        // �ŏ��̃`���[�g���A����ݒ�
        //StartCoroutine("SetCurrentTask(tutorialTask.First())");
        SetCurrentTask(tutorialTask[0]);
        Debug.Log(tutorialTask[0]);

        isEnabled = true;
    }

    void Update()
    {
        // �`���[�g���A�������݂��A���s����Ă��Ȃ��ꍇ
        if(currentTask != null && !task_executed)
        {
            // ���݂̃`���[�g���A�������s���ꂽ������
            if(currentTask.CheckTask())
            {
                task_executed = true;

                // UI�A�j���[�V����

                // �I�������^�X�N�����X�g����폜
                tutorialTask.RemoveAt(0);

                var nextTask = tutorialTask.FirstOrDefault();
                if(nextTask != null)
                {

                    SetCurrentTask(tutorialTask[0]);
                    //StartCoroutine("SetCurrentTask(nextTask, 1.0f)");
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
