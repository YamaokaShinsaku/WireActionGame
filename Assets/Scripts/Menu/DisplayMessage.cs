using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMessage : MonoBehaviour
{
    [SerializeField]
    public string message;              // メッセージテキスト
    [SerializeField]
    public GameObject messagePrefab;    // メッセージプレファブ
    [SerializeField]
    private GameObject canvas;          // 表示するCanvas
    
    private GameObject messageClone;       // messagePrefabのclone作成用

    private float fadeIn_x = 2000;
    private float fadeOut_x = 3000;

    [SerializeField]
    private GameObject firstSubPanel;

    [SerializeField]
    private GameObject secondSubPanel;

    public bool isMenuOpen;
    private bool isFadeOut;
    private float deleteCount = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        isMenuOpen = false;
        isFadeOut = false;
    }

    void Update()
    {
        if(Input.GetButtonDown("MenuOpenButton"))
        {
            FadeIn();
            isMenuOpen = true;
        }
        if (Input.GetButtonDown("MenuCloseButton"))
        {
            FadeOut();
            isFadeOut = true;
        }

        // フェードアウトしているとき
        if(isFadeOut == true)
        {
            // カウントを減算
            deleteCount -= Time.unscaledDeltaTime;
        }

        // カウントが0になったら
        if (deleteCount < 0)
        {
            // clone を削除
            Destroy(messageClone);
            // カウントをリセット
            deleteCount = 0.5f;
            // フラグをリセット
            isFadeOut = false;

            isMenuOpen = false;
        }
    }

    public void FadeIn()
    {
        if (!messageClone)
        {
            // messagePrefabのcloneを作成
            messageClone = Instantiate(messagePrefab);
            messageClone.SetActive(true);
            messageClone.transform.SetParent(canvas.transform, false);

            // メッセージ内容を取得
            Text messageUIText = messageClone.transform.Find("Message").GetComponent<Text>();
            messageUIText.text = message;
            // 画面内に表示
            iTween.MoveFrom(messageClone, iTween.Hash(
                "position", messageClone.transform.position + new Vector3(fadeIn_x, 0, 0),
                "time", 5 * Time.unscaledDeltaTime));
        }
    }

    public void FadeOut()
    {
        if (messageClone)
        {
            // 画面外に移動
            iTween.MoveTo(messageClone, iTween.Hash(
                "position", messageClone.transform.position + new Vector3(fadeOut_x, 0, 0),
                "time", 5 * Time.unscaledDeltaTime));

            firstSubPanel.SetActive(false);
            secondSubPanel.SetActive(false);
        }
    }
}
