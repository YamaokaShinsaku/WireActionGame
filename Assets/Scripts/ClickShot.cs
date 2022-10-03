using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickShot : MonoBehaviour
{
    public GameObject[] shotObj;    // 飛ばす武器
    public int count;               // 発射カウント
    private GameObject clone;       // 武器のクローン
    private float nowPos;

    public Vector3[] offset;
    public Transform target;

    public GameObject lookTarget;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // 武器が残っているとき
        if (count < shotObj.Length)
        {

            // 武器がプレイヤーを追従するようにする
            for (int i = 0; i < shotObj.Length; i++)
            {
                // 位置の計算
                Vector3 position = shotObj[i].transform.position;

                position.y = offset[i].y + Mathf.PingPong(Time.time / 3, 0.3f);
                position.z = shotObj[i].transform.position.z;

                shotObj[i].transform.position = target.transform.position + offset[i];

                // 向きの計算
                Vector3 direction = lookTarget.transform.position - shotObj[i].transform.position;
                direction.y = 0;
                
                Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
                lookRotation.x = 1.0f;
                
                shotObj[i].transform.rotation = Quaternion.Lerp(shotObj[i].transform.rotation, lookRotation, 0.1f);

                //Debug.Log(shotObj[i].transform.rotation);
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                clone = Instantiate(shotObj[count]);    // 武器のクローンを生成
                shotObj[count].SetActive(false);        // 武器本体を非表示に
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 dir = ray.direction;            // 飛ばす向き
                clone.GetComponent<Rigidbody>().AddForce(dir * 3000);   // マウスの位置に飛ばす

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
                shotObj[i].SetActive(true);
            }
        }
    }
}
