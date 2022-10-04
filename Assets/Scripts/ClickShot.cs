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

    public GameObject lookTarget;
    private Vector3 prevPosition;
    FixedJoint component;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 diff = shotObj[0].transform.position - prevPosition;
        ////prevPosition = shotObj[0].transform.position;
        ////if (diff.magnitude > 0.01f)
        ////{
        ////    //shotObj[0].transform.rotation = Quaternion.LookRotation(diff);
        ////    shotObj[0].transform.eulerAngles = new Vector3(90, Quaternion.LookRotation(diff).y, 0);
        ////}

        //Quaternion rot = Quaternion.AngleAxis(-lookTarget.transform.rotation.y, Vector3.forward);
        //Quaternion q = shotObj[0].transform.rotation;
        //shotObj[0].transform.rotation = Quaternion.RotateTowards(shotObj[0].transform.rotation, q * rot, 5);

        // 座標更新
        for (int i = 0; i < shotObj.Length; i++)
        {
            position[i] = shotObj[i].transform.position;
            rotation[i] = shotObj[i].transform.rotation;
        }

        // 武器が残っているとき
        if (count < shotObj.Length)
        {
            if (component == null)
            {
                component = shotObj[0].AddComponent<FixedJoint>();
                component.connectedBody = lookTarget.GetComponent<Rigidbody>();
            }
            // 武器がプレイヤーを追従するようにする
            for (int i = 0; i < shotObj.Length; i++)
            {
                // 位置の計算
                //shotObj[i].transform.position = target.transform.position + offset[i];
                
                // 向きの計算
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                Destroy(component);
                if (component == null)
                {
                    clone = Instantiate(shotObj[count], position[count], rotation[count]);    // 武器のクローンを生成
                    shotObj[count].SetActive(false);        // 武器本体を非表示に
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Vector3 dir = ray.direction;            // 飛ばす向き
                    clone.GetComponent<Rigidbody>().AddForce(dir * 3000);   // マウスの位置に飛ばす
                    count++;

                    // 5秒後にクローンを削除
                    Destroy(clone, 5f);
                }
                //clone = Instantiate(shotObj[count], position[count], rotation[count]);    // 武器のクローンを生成
                //shotObj[count].SetActive(false);        // 武器本体を非表示に
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //Vector3 dir = ray.direction;            // 飛ばす向き
                //clone.GetComponent<Rigidbody>().AddForce(dir * 3000);   // マウスの位置に飛ばす
                //count++;

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
