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
                // messagePrefab�̃N���[���𐶐�
                messageUI = Instantiate(messagePrefab);
                messageUI.transform.SetParent(canvas.transform, false);

                // ���b�Z�\�W��ݒ�
                Text messageUIText = messageUI.transform.FindChild("Message").GetComponent<Text>();
                messageUIText.text = message;
                // ���b�Z�[�W����ʓ��Ɉړ�
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
                // ���b�Z�[�W����ʊO�Ɉړ�
                iTween.MoveTo(messageUI, iTween.Hash(
                    "position", messageUI.transform.position + new Vector3(fadeOut_x, 0, 0),
                    "time", 1));

                yield return new WaitForSeconds(0.5f);

                Destroy(messageUI);
            }
        }
    }
}
