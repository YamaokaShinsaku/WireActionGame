using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageFade : MonoBehaviour
{
    //[SerializeField]
    //public string message;              // メッセージテキスト
    [SerializeField]
    public GameObject gameObj;    // メッセージプレファブ
    //[SerializeField]
    //private GameObject canvas;          // 表示するCanvas
    //[SerializeField]
    //private GameObject messageUI;       // messagePrefabのclone作成用

    private float fadeIn_x = 2000;
    private float fadeOut_x = 3000;

    public void FadeIn()
    {
        if (!gameObj)
        {
            // 画面内に表示
            iTween.MoveFrom(gameObj, iTween.Hash(
                "position", gameObj.transform.position + new Vector3(fadeIn_x, 0, 0),
                "time", 1));
        }
    }

    public void FadeOut()
    {
        if (gameObj)
        {
            // 画面外に移動
            iTween.MoveTo(gameObj, iTween.Hash(
                "position", gameObj.transform.position + new Vector3(fadeOut_x, 0, 0),
                "time", 3));

            //yield return new WaitForSeconds(0.5f);

            //Destroy(messageUI);
        }
    }
}
