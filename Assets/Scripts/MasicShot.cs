using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasicShot : MonoBehaviour
{
    public GameObject shotObj;       // 飛ばす武器
    public int count;               // 発射カウント
    private GameObject clone;       // 武器のクローン

    [SerializeField]
    private GameObject cloneCreatePosition;

    public Vector3 position;
    public Quaternion rotation;

    public Vector3 offset;
    public Transform target;

    private float angle;
    //　回転するスピード
    [SerializeField]
    private float rotateSpeed = 180f;
    //　ターゲットからの距離
    [SerializeField]
    private Vector3 distanceFromTarget;

    private float beforeTrigger;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        shotObj.GetComponent<Homing_2>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 座標更新
        //　ユニットの位置 = ターゲットの位置 ＋ ターゲットから見たユニットの角度 ×　ターゲットからの距離
        shotObj.transform.position = target.position + Quaternion.Euler(0f, angle, 0f) * distanceFromTarget;
        //　ユニット自身の角度 = ターゲットから見たユニットの方向の角度を計算しそれをユニットの角度に設定する
        shotObj.transform.rotation =
            Quaternion.LookRotation(shotObj.transform.position -
            new Vector3(target.position.x, shotObj.transform.position.y, target.position.z), Vector3.up);
        //　ユニットの角度を変更
        angle += rotateSpeed * Time.deltaTime;
        //　角度を0～360度の間で繰り返す
        angle = Mathf.Repeat(angle, 360f);


        float rightTrigger = Input.GetAxis("magicShot");

        if (Input.GetKeyDown(KeyCode.J) || rightTrigger > 0 && beforeTrigger == 0.0f)
        {
            clone = Instantiate(shotObj, cloneCreatePosition.transform.position, cloneCreatePosition.transform.rotation);    // 武器のクローンを生成
            //shotObj.SetActive(false);        // 武器本体を非表示に
            clone.GetComponent<Homing_2>().enabled = true;
            count++;

            // 5秒後にクローンを削除
            Destroy(clone, 5f);
        }

        beforeTrigger = rightTrigger;

        //　武器を装填する
        //if (Input.GetKeyDown(KeyCode.K) || Input.GetButtonDown("magicSet"))
        //{
        //    shotObj.SetActive(true);        // 武器本体を非表示に
        //}
    }
}
