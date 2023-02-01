using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{
    [SerializeField]
    private float deltaTime;    // 経過時間
    [SerializeField]
    private float vectorX;      // ランダムX座標
    [SerializeField]
    private float vectorY;      // ランダムY座標
    [SerializeField]
    private float vectorZ;      // ランダムＺ座標

    [SerializeField]
    public float restTime;

    // 生成する範囲の座標を持つオブジェクトを取得
    [SerializeField]
    private Transform rangeXMin;
    [SerializeField]
    private Transform rangeXMax;
    [SerializeField]
    private Transform rangeYMin;
    [SerializeField]
    private Transform rangeYMax;
    [SerializeField]
    private Transform rangeZMin;
    [SerializeField]
    private Transform rangeZMax;

    // Start is called before the first frame update
    void Start()
    {
        deltaTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime -= Time.deltaTime;

        if(deltaTime <= 0.0f)
        {
            // ランダムな値を取得
            vectorX = Random.Range(rangeXMin.position.x, rangeXMax.position.x);
            vectorY = Random.Range(rangeYMin.position.y, rangeYMax.position.y);
            vectorZ = Random.Range(rangeZMin.position.z, rangeZMax.position.z);

            // 座標更新
            this.transform.position = new Vector3(vectorX, vectorY, vectorZ);

            // 経過時間をリセット
            deltaTime = restTime;
        }
    }
}
