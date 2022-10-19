using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickShot : MonoBehaviour
{
    public GameObject[] shotObj;    // 飛ばす武器
    public int count;               // 発射カウント
    private GameObject clone;       // 武器のクローン
    public Vector3[] position;
    public Quaternion[] rotation;

    public Vector3[] offset;
    public Transform target;

    private float angle;
    //　回転するスピード
    [SerializeField]
    private float rotateSpeed = 180f;
    //　ターゲットからの距離
    [SerializeField]
    private Vector3[] distanceFromTarget;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;

        for (int i = 0; i < shotObj.Length; i++)
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
            //position[i] = shotObj[i].transform.position;
            //rotation[i] = shotObj[i].transform.rotation;

            //position[i] = target.position + offset[i];
            //shotObj[i].transform.position = position[i];

            //　ユニットの位置 = ターゲットの位置 ＋ ターゲットから見たユニットの角度 ×　ターゲットからの距離
            shotObj[i].transform.position = target.position + Quaternion.Euler(0f, angle, 0f) * distanceFromTarget[i];
            //　ユニット自身の角度 = ターゲットから見たユニットの方向の角度を計算しそれをユニットの角度に設定する
            shotObj[i].transform.rotation = 
                Quaternion.LookRotation(shotObj[i].transform.position - 
                new Vector3(target.position.x, shotObj[i].transform.position.y, target.position.z), Vector3.up);
            //　ユニットの角度を変更
            angle += rotateSpeed * Time.deltaTime;
            //　角度を0～360度の間で繰り返す
            angle = Mathf.Repeat(angle, 360f);
        }


        // 武器が残っているとき
        if (count < shotObj.Length)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                clone = Instantiate(shotObj[count], shotObj[count].transform.position, shotObj[count].transform.rotation);    // 武器のクローンを生成
                shotObj[count].SetActive(false);        // 武器本体を非表示に
                clone.GetComponent<Homing_2>().enabled = true;
                count++;

                // 5秒後にクローンを削除
                Destroy(clone, 5f);
            }
        }
        else
        {
            count = 0;
        }

        //　武器を装填する
        if (Input.GetKeyDown(KeyCode.K))
        {
            for (int i = 0; i < shotObj.Length; i++)
            {
                shotObj[i].SetActive(true);        // 武器本体を非表示に
            }
        }
    }
}
