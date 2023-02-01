using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasicShot : MonoBehaviour
{
    public GameObject[] shotObj;       // 飛ばす武器
    public int count;               // 発射カウント
    private GameObject clone;       // 武器のクローン
    private GameObject cloneSecond;       // 武器のクローン
    private GameObject cloneThird;       // 武器のクローン
    private GameObject cloneForce;       // 武器のクローン

    [SerializeField]
    public GameObject cloneIdleFire;     // クローンを生成する場所

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
        for(int i = 0; i < shotObj.Length; i++)
        {
            shotObj[i].GetComponent<Homing_2>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 座標更新
        for (int i = 0; i < shotObj.Length; i++)
        {
            //　ユニットの位置 = ターゲットの位置 ＋ ターゲットから見たユニットの角度 ×　ターゲットからの距離
            shotObj[i].transform.position = target.position + Quaternion.Euler(0f, angle, 0f) * distanceFromTarget;
            //　ユニット自身の角度 = ターゲットから見たユニットの方向の角度を計算しそれをユニットの角度に設定する
            shotObj[i].transform.rotation =
                Quaternion.LookRotation(shotObj[i].transform.position -
                new Vector3(target.position.x, shotObj[i].transform.position.y, target.position.z), Vector3.up);
            //　ユニットの角度を変更
            angle += rotateSpeed * Time.deltaTime;
            //　角度を0～360度の間で繰り返す
            angle = Mathf.Repeat(angle, 360f);
        }

        float rightTrigger = Input.GetAxis("magicShot");

        if (Input.GetKeyDown(KeyCode.J) || rightTrigger > 0 && beforeTrigger == 0.0f
            && count < 4)
        {
            // 武器のクローンを生成
            clone = Instantiate(shotObj[0], cloneIdleFire.transform.position, cloneIdleFire.transform.rotation);
            cloneSecond = Instantiate(shotObj[1], cloneIdleFire.transform.position, cloneIdleFire.transform.rotation);
            cloneThird = Instantiate(shotObj[2], cloneIdleFire.transform.position, cloneIdleFire.transform.rotation);
            cloneForce = Instantiate(shotObj[3], cloneIdleFire.transform.position, cloneIdleFire.transform.rotation);

            //shotObj.SetActive(false);        // 武器本体を非表示に
            clone.GetComponent<Homing_2>().enabled = true;
            cloneSecond.GetComponent<Homing_2>().enabled = true;
            cloneThird.GetComponent<Homing_2>().enabled = true;
            cloneForce.GetComponent<Homing_2>().enabled = true;

            // 3秒後にクローンを削除
            Destroy(clone, 3f);
            Destroy(cloneSecond, 3f);
            Destroy(cloneThird, 3f);
            Destroy(cloneForce, 3f);

            count++;
        }
        // 残弾数によってサイズを変更
        if(count == 1)
        {
            cloneIdleFire.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }
        if (count == 2)
        {
            cloneIdleFire.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        if (count == 3)
        {
            cloneIdleFire.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }
        if (count == 4)
        {
            cloneIdleFire.SetActive(false);
        }


        beforeTrigger = rightTrigger;

        //　武器を装填する
        if (Input.GetKeyDown(KeyCode.K) || Input.GetButtonDown("magicSet"))
        {
            count = 0;
            cloneIdleFire.SetActive(true);        // 武器本体を非表示に
            cloneIdleFire.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
}
