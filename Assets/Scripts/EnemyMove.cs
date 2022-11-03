using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    public GameObject target;      // 追従するオブジェクト

    [SerializeField]
    private float speed;            // 移動速度

    private float rotationSpeed = 0.5f;

    public Camera targetCamera;

    Rect rect = new Rect(0, 0, 1, 1);

    public bool isVisible;      // 画面内に表示されているかどうか

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

        // target に向かって移動する
        this.transform.position =
            Vector3.MoveTowards(this.transform.position, target.transform.position, Time.deltaTime * speed);


        var viewportPos = targetCamera.WorldToViewportPoint(this.transform.position);

        // 進行方向に回転させる
        Vector3 vec3 = this.transform.position - latePosition;
        latePosition = this.transform.position;

        if(vec3.magnitude > 0.01f)
        {
            this.transform.rotation = Quaternion.LookRotation(vec3);
        }


        if (rect.Contains(viewportPos))
        {
            // 表示されている場合の処理
            //ShowText(this.gameObject.name + "画面に表示");

            isVisible = true;
            //lockOn.enabled = true;
        }
        else
        {
            // 表示されていない場合の処理
            //ShowText(this.gameObject.name + "画面から消えた");

            isVisible = false;
            //lockOn.enabled = false;
            //lockOn.target = null;
        }
    }

    /// <summary>
    /// メッセージを表示する
    /// </summary>
    /// <param name="message">表示するメッセージ</param>
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
