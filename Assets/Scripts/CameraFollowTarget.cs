using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField]
    private GameObject target;      // 追従させたいターゲット

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        //ゲーム開始時点のカメラとターゲットの距離を取得
        offset = gameObject.transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        // カメラの位置をターゲットの位置にオフセットを足した場所にする
        gameObject.transform.position = target.transform.position + offset;

    }
}
