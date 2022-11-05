using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMessage : MonoBehaviour
{
    [SerializeField]
    public string message;              // 表示するメッセージ
    [SerializeField]
    public GameObject messagePrefab;    // メッセージUIのPrefab
    [SerializeField]
    private GameObject canvas;          // 表示するCanvas
    [SerializeField]
    private GameObject messageUI;       // messagePrefabのクローン生成用

    private float fadeIn_x = 1920;
    private float fadeOut_x = 1920;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    [System.Obsolete]
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(!messageUI)
            {
                // messagePrefabのクローンを生成
                messageUI = Instantiate(messagePrefab);
                messageUI.transform.SetParent(canvas.transform, false);

                // メッセ―ジを設定
                Text messageUIText = messageUI.transform.FindChild("Message").GetComponent<Text>();
                messageUIText.text = message;
                // メッセージを画面内に移動
                iTween.MoveFrom(messageUI, iTween.Hash(
                    "position", messageUI.transform.position + new Vector3(fadeIn_x, 0, 0),
                    "time", 1));
            }
        }
    }

    public IEnumerator OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (messageUI)
            {
                // メッセージを画面外に移動
                iTween.MoveTo(messageUI, iTween.Hash(
                    "position", messageUI.transform.position + new Vector3(fadeOut_x, 0, 0),
                    "time", 1));

                yield return new WaitForSeconds(0.5f);

                Destroy(messageUI);
            }
        }
    }
}
