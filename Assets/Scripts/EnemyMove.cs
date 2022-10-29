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


    // Start is called before the first frame update
    void Start()
    {
        //speed = 0.1f;

        lockOn = lockOn.GetComponent<LockOnTarget.LockOnTarget>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vec3 = target.transform.position - this.transform.position;
        Quaternion quaternion = Quaternion.LookRotation(vec3);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, quaternion, rotationSpeed);

        // target に向かって移動する
        this.transform.position =
            Vector3.MoveTowards(this.transform.position, target.transform.position, speed);


        var viewportPos = targetCamera.WorldToViewportPoint(this.transform.position);

        if(rect.Contains(viewportPos))
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
