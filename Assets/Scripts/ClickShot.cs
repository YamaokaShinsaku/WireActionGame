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

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // 座標更新
        for (int i = 0; i < shotObj.Length; i++)
        {
            position[i] = shotObj[i].transform.position;
            rotation[i] = shotObj[i].transform.rotation;

            position[i] = target.position + offset[i];
            shotObj[i].transform.position = position[i];
        }

        // 武器が残っているとき
        if (count < shotObj.Length)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                clone = Instantiate(shotObj[count], position[count], rotation[count]);    // 武器のクローンを生成
                shotObj[count].SetActive(false);        // 武器本体を非表示に
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 dir = ray.direction;            // 飛ばす向き
                clone.GetComponent<Rigidbody>().AddForce(dir * 3000);   // マウスの位置に飛ばす
                count++;

                //// 5秒後にクローンを削除
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
                shotObj[i].SetActive(true);
            }
        }
    }
}
