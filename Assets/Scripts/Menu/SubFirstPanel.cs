using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SubFirstPanel : MonoBehaviour
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
        mainPanel.SetActive(true);

        mES = GetComponent<EventSystem>();
        //ボタンが選択された状態になる
        EventSystem.current.SetSelectedGameObject(button);
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームパッドのスティックの傾けた時の値を取得
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        // 傾きが1の時（↑に倒しているとき）
        if (/*Input.GetKeyDown(KeyCode.UpArrow)*/ /*y >= 1 ||*/ x <= -1)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(button);
        }
        // 傾きが-1の時（↓に倒しているとき）
        if (/*Input.GetKeyDown(KeyCode.DownArrow)*/ /*y <= -1 ||*/ x >= 1)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(button2);
        }
    }
    public void MainView()
    {
        //mainPanel.GetComponent<RectTransform>().SetAsLastSibling();
        //Time.timeScale = 0.0f;
        mainPanel.SetActive(true);
        this.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(button);
    }
}
