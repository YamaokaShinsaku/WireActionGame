using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing_2 : MonoBehaviour
{
    private float DivNum = 40;      // 分割数
    private float Counter = 0f;     // レーザー進行フレーム用
    public float CounterSpeed;      // 進行フレーム数

    //public GameObject target;       // 目標オブジェクト

    public LockOnTarget.LockOnTarget lockonTarget;
    public GameObject player;
    [SerializeField]
    private GameObject targetEnemy;

    // ベジェ曲線の目標座標を持つオブジェクト
    [SerializeField]
    public Transform firstPoint;
    [SerializeField]
    public Transform secondPoint;

    private float u = 0f;           //ベジェ曲線位置用

    private Vector3 P0 = Vector3.zero;      // ベジェ曲線の発生位置

    private Vector3 P1 = Vector3.zero;      // ベジェ曲線の下側の目標座標

    private Vector3 P2 = Vector3.zero;      // ベジェ曲線の真ん中付近の目標座標

    private Vector3 P3 = Vector3.zero;      // 目標座標

    // 補完用
    private Vector3 P01 = Vector3.zero;

    private Vector3 P12 = Vector3.zero;

    private Vector3 P23 = Vector3.zero;

    private Vector3 P02 = Vector3.zero;

    private Vector3 P13 = Vector3.zero;

    private Vector3 P03 = Vector3.zero;

    /// <summary>
    /// 目標座標を生成
    /// </summary>
    /// <param name="x">P3.x</param>
    /// <param name="y">P3.y</param>
    /// <param name="z">P3.z</param>
    public void Create_P3xyz(float x, float y, float z)
    {
        P3 = new Vector3(x, y, z);
    }

    void Start()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);//画像回転

        // ベジェ曲線発生位置
        P0 = new Vector3(
            this.transform.position.x,
            this.transform.position.y,
            this.transform.position.z);

        // ベジェ曲線最初の目標座標
        //P1 = new Vector3(
        //    /*Random.Range(-5.0f + this.transform.position.x, 5.0f + this.transform.position.x)*/
        //    this.transform.position.x - 10.0f,
        //    Random.Range(0.0f + this.transform.position.y, 0.0f + this.transform.position.y)
        //    //this.transform.position.y - 10.0f
        //    ,
        //    /*Random.Range(-5.0f + this.transform.position.z, 5.0f + this.transform.position.z)*/
        //    this.transform.position.z + 20.0f);

        // ベジェ曲線二つ目の目標座標
        //P2 = new Vector3(
        //    /*Random.Range(-5.0f + this.transform.position.x, 5.0f + this.transform.position.x)*/
        //    this.transform.position.x,
        //    Random.Range(0.0f + this.transform.position.y, 0.0f + this.transform.position.y),
        //    /*Random.Range(-5.0f + this.transform.position.z, 5.0f + this.transform.position.z)*/
        //    this.transform.position.z - 20.0f);

        // ベジェ曲線最初の目標座標
        P1 = new Vector3(firstPoint.position.x, firstPoint.position.y, firstPoint.position.z);
        // ベジェ曲線二つ目の目標座標
        P2 = new Vector3(secondPoint.position.x,secondPoint.position.y,secondPoint.position.z);

        // 最終目標座標
        //Create_P3xyz(target.transform.position.x, target.transform.position.y, target.transform.position.z);
        lockonTarget = player.GetComponentInChildren<LockOnTarget.LockOnTarget>();

        //// 目標座標
       // Create_P3xyz(targetEnemy.transform.position.x, targetEnemy.transform.position.y, targetEnemy.transform.position.z);
    }

    void Update()
    {
        //targetEnemy = lockonTarget.serchTag(this.gameObject,"Enemy");
        //// 目標座標
        //Create_P3xyz(targetEnemy.transform.position.x, targetEnemy.transform.position.y, targetEnemy.transform.position.z);

        // エネミーがロックされていないとき
        if(targetEnemy = null)
        {
            Vector3 centerPosition = Camera.main.transform.position;
            // 最終目標座標
            Create_P3xyz(centerPosition.x, centerPosition.y, centerPosition.z);
        }
        // エネミーがロックされているとき
        else if (targetEnemy = lockonTarget.serchTag(this.gameObject, "Enemy"))
        {
            // エネミーの座標を最終目標座標に
            Create_P3xyz(targetEnemy.transform.position.x, targetEnemy.transform.position.y, targetEnemy.transform.position.z);
        }


        // ベジェ曲線の位置を移動
        u = (1.0f / DivNum) * Counter;

        P01 = new Vector3(
            (1.0f - u) * P0.x + u * P1.x,
            (1.0f - u) * P0.y + u * P1.y,
            (1.0f - u) * P0.z + u * P1.z);

        P12 = new Vector3(
            (1.0f - u) * P1.x + u * P2.x,
            (1.0f - u) * P1.y + u * P2.y,
            (1.0f - u) * P1.z + u * P2.z);

        P23 = new Vector3(
            (1.0f - u) * P2.x + u * P3.x,
            (1.0f - u) * P2.y + u * P3.y,
            (1.0f - u) * P2.z + u * P3.z);

        P02 = new Vector3(
            (1.0f - u) * P01.x + u * P12.x,
            (1.0f - u) * P01.y + u * P12.y,
            (1.0f - u) * P01.z + u * P12.z);

        P13 = new Vector3(
            (1.0f - u) * P12.x + u * P23.x,
            (1.0f - u) * P12.y + u * P23.y,
            (1.0f - u) * P12.z + u * P23.z);

        P03 = new Vector3(
            (1.0f - u) * P02.x + u * P13.x,
            (1.0f - u) * P02.y + u * P13.y,
            (1.0f - u) * P02.z + u * P13.z);

        // 座標更新
        Vector3 pos = transform.position;
        pos = P03;

        // ターゲットの方向にY軸回転する
        //Vector3 direction = target.transform.position - this.transform.position;
        //Vector3 direction = targetEnemy.transform.position - this.transform.position;
        //Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        //Quaternion rotation2 = Quaternion.LookRotation(direction, Vector3.right);

        //rotation = new Quaternion(1.0f/*rotation2.x * target.transform.position.y*/, rotation.y * 2.0f, rotation.z, rotation.w);

        // 表示座標
        this.transform.position = pos;
        //this.transform.rotation = rotation;

        // 速さ
        Counter += CounterSpeed;
    }
}
