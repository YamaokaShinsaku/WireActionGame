using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    public GameObject target;      // �Ǐ]����I�u�W�F�N�g

    [SerializeField]
    private float speed;            // �ړ����x

    public Camera targetCamera;

    Rect rect = new Rect(0, 0, 1, 1);

    public bool isVisible;

    [SerializeField]
    Text uiText;

    [SerializeField]
    private LockOnTarget.LockOnTarget lockOn;

    // Start is called before the first frame update
    void Start()
    {
        //speed = 0.1f;

        lockOn = lockOn.GetComponent<LockOnTarget.LockOnTarget>();
    }

    // Update is called once per frame
    void Update()
    {
        // target �Ɍ������Ĉړ�����
        this.transform.position =
            Vector3.MoveTowards(this.transform.position, target.transform.position, speed);


        var viewportPos = targetCamera.WorldToViewportPoint(this.transform.position);

        if(rect.Contains(viewportPos))
        {
            // �\������Ă���ꍇ�̏���
            ShowText(this.gameObject.name + "��ʂɕ\��");

            isVisible = true;
            //lockOn.enabled = true;
        }
        else
        {
            // �\������Ă��Ȃ��ꍇ�̏���
            ShowText(this.gameObject.name + "��ʂ��������");

            isVisible = false;
            //lockOn.enabled = false;
            //lockOn.target = null;
        }
    }

    void ShowText(string message)
    {
        uiText.text = message;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Magic")
        {
            this.gameObject.SetActive(false);
        }
    }

}
