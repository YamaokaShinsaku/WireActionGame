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

    private float rotationSpeed = 0.5f;

    public Camera targetCamera;

    Rect rect = new Rect(0, 0, 1, 1);

    public bool isVisible;      // ��ʓ��ɕ\������Ă��邩�ǂ���

    [SerializeField]
    Text uiText;

    [SerializeField]
    private LockOnTarget.LockOnTarget lockOn;

    private Vector3 latePosition;


    // Start is called before the first frame update
    void Start()
    {
        //speed = 0.1f;

        lockOn = lockOn.GetComponent<LockOnTarget.LockOnTarget>();

        this.transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {

        // target �Ɍ������Ĉړ�����
        this.transform.position =
            Vector3.MoveTowards(this.transform.position, target.transform.position, Time.deltaTime * speed);


        var viewportPos = targetCamera.WorldToViewportPoint(this.transform.position);

        // �i�s�����ɉ�]������
        Vector3 vec3 = this.transform.position - latePosition;
        latePosition = this.transform.position;

        if(vec3.magnitude > 0.01f)
        {
            this.transform.rotation = Quaternion.LookRotation(vec3);
        }


        if (rect.Contains(viewportPos))
        {
            // �\������Ă���ꍇ�̏���
            //ShowText(this.gameObject.name + "��ʂɕ\��");

            isVisible = true;
            //lockOn.enabled = true;
        }
        else
        {
            // �\������Ă��Ȃ��ꍇ�̏���
            //ShowText(this.gameObject.name + "��ʂ��������");

            isVisible = false;
            //lockOn.enabled = false;
            //lockOn.target = null;
        }
    }

    /// <summary>
    /// ���b�Z�[�W��\������
    /// </summary>
    /// <param name="message">�\�����郁�b�Z�[�W</param>
    void ShowText(string message)
    {
        uiText.text = message;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Magic")
        {
            //this.gameObject.SetActive(false);

            this.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

}
