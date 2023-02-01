using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClearPanel : MonoBehaviour
{

    public GameObject mainPanel;

    //　最初にフォーカスするゲームオブジェクト
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
        //ボタンが選択された状態になる
        EventSystem.current.SetSelectedGameObject(button);
    }

    // Update is called once per frame
    void Update()
    {

        // ゲームパッドのスティックの傾けた時の値を取得
        float x = Input.GetAxis("Horizontal");

        // 傾きが1の時（↑に倒しているとき）
        if (/*Input.GetKeyDown(KeyCode.UpArrow)*/ x <= -1)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(button);
        }
        // 傾きが-1の時（↓に倒しているとき）
        if (/*Input.GetKeyDown(KeyCode.DownArrow)*/ x >= 1)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(button2);
        }
    }
}
