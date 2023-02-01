using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClearPanel : MonoBehaviour
{

    public GameObject mainPanel;

    //�@�ŏ��Ƀt�H�[�J�X����Q�[���I�u�W�F�N�g
    [SerializeField]
    private GameObject firstSelect;

    public GameObject button;
    public GameObject button2;

    private EventSystem mES;

    // Start is called before the first frame update
    void Start()
    {
        mainPanel.GetComponent<RectTransform>().SetAsLastSibling();

        mES = GetComponent<EventSystem>();
        //�{�^�����I�����ꂽ��ԂɂȂ�
        EventSystem.current.SetSelectedGameObject(button);
    }

    // Update is called once per frame
    void Update()
    {

        // �Q�[���p�b�h�̃X�e�B�b�N�̌X�������̒l���擾
        float x = Input.GetAxis("Horizontal");

        // �X����1�̎��i���ɓ|���Ă���Ƃ��j
        if (/*Input.GetKeyDown(KeyCode.UpArrow)*/ x <= -1)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(button);
        }
        // �X����-1�̎��i���ɓ|���Ă���Ƃ��j
        if (/*Input.GetKeyDown(KeyCode.DownArrow)*/ x >= 1)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(button2);
        }
    }
}
