using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageFade : MonoBehaviour
{
    //[SerializeField]
    //public string message;              // ���b�Z�[�W�e�L�X�g
    [SerializeField]
    public GameObject gameObj;    // ���b�Z�[�W�v���t�@�u
    //[SerializeField]
    //private GameObject canvas;          // �\������Canvas
    //[SerializeField]
    //private GameObject messageUI;       // messagePrefab��clone�쐬�p

    private float fadeIn_x = 2000;
    private float fadeOut_x = 3000;

    public void FadeIn()
    {
        if (!gameObj)
        {
            // ��ʓ��ɕ\��
            iTween.MoveFrom(gameObj, iTween.Hash(
                "position", gameObj.transform.position + new Vector3(fadeIn_x, 0, 0),
                "time", 1));
        }
    }

    public void FadeOut()
    {
        if (gameObj)
        {
            // ��ʊO�Ɉړ�
            iTween.MoveTo(gameObj, iTween.Hash(
                "position", gameObj.transform.position + new Vector3(fadeOut_x, 0, 0),
                "time", 3));

            //yield return new WaitForSeconds(0.5f);

            //Destroy(messageUI);
        }
    }
}
