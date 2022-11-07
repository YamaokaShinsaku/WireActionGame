using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMessage : MonoBehaviour
{
    [SerializeField]
    public string message;              // �\�����郁�b�Z�[�W
    [SerializeField]
    public GameObject messagePrefab;    // ���b�Z�[�WUI��Prefab
    [SerializeField]
    private GameObject canvas;          // �\������Canvas
    [SerializeField]
    private GameObject messageUI;       // messagePrefab�̃N���[�������p

    private float fadeIn_x = 2000;
    private float fadeOut_x = 3000;

    [SerializeField]
    private GameObject firstSubPanel;

    [SerializeField]
    private GameObject secondSubPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {
        if (!messageUI)
        {
            // messagePrefabのcloneを作成
            messageUI = Instantiate(messagePrefab);
            messageUI.transform.SetParent(canvas.transform, false);

            // メッセージ内容を取得
            Text messageUIText = messageUI.transform.Find("Message").GetComponent<Text>();
            messageUIText.text = message;
            // 画面内に表示
            iTween.MoveFrom(messageUI, iTween.Hash(
                "position", messageUI.transform.position + new Vector3(fadeIn_x, 0, 0),
                "time", 1));
        }
    }

    public IEnumerator FadeOut()
    {
        if (messageUI)
        {
            // 画面外に移動
            iTween.MoveTo(messageUI, iTween.Hash(
                "position", messageUI.transform.position + new Vector3(fadeOut_x, 0, 0),
                "time", 3));

            firstSubPanel.SetActive(false);
            secondSubPanel.SetActive(false);

            yield return new WaitForSeconds(0.5f);

            Destroy(messageUI);
        }
    }


    [System.Obsolete]
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(!messageUI)
            {
                // messagePrefabのcloneを作成
                messageUI = Instantiate(messagePrefab);
                messageUI.transform.SetParent(canvas.transform, false);

                // メッセージ内容を取得
                Text messageUIText = messageUI.transform.Find("Message").GetComponent<Text>();
                messageUIText.text = message;
                // 画面内に表示
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
                // 画面外に移動
                iTween.MoveTo(messageUI, iTween.Hash(
                    "position", messageUI.transform.position + new Vector3(fadeOut_x, 0, 0),
                    "time", 3));

                firstSubPanel.SetActive(false);
                secondSubPanel.SetActive(false);

                yield return new WaitForSeconds(0.5f);

                Destroy(messageUI);
            }
        }
    }
}
