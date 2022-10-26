using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ロックオン
/// </summary>
namespace LockOnTarget
{
    public class LockOnTarget : MonoBehaviour
    {
        private GameObject nearObj;         //最も近いオブジェクト
        private float searchTime = 0;    //経過時間

        [SerializeField]
        private Camera targetCamera;
        [SerializeField]
        public Transform target;
        [SerializeField]
        private Transform targetUI;
        [SerializeField]
        private Vector3 worldOffset;

        private RectTransform parentUI;

        // Use this for initialization
        void Start()
        {
            //最も近かったオブジェクトを取得
            nearObj = serchTag(gameObject, "Enemy");

            target = nearObj.transform;
        }

        public void Initialize(Transform target_, Camera targetCamera = null)
        {
            target = target_;
            targetCamera = targetCamera != null ? targetCamera : Camera.main;

            OnUpdatePosition();
        }

        private void Awake()
        {
            // カメラが指定されていなければメインカメラにする
            if (targetCamera == null)
            {
                targetCamera = Camera.main; 
            }

            // 親UIのRectTransformを保持
            parentUI = targetUI.parent.GetComponent<RectTransform>();
        }

        // Update is called once per frame
        void Update()
        {
            target = nearObj.transform;
            OnUpdatePosition();

            //経過時間を取得
            searchTime += Time.deltaTime;

            if (searchTime >= 1.0f)
            {
                //最も近かったオブジェクトを取得
                nearObj = serchTag(gameObject, "Enemy");

                // 名前を表示
                //Debug.Log(nearObj.name);

                //経過時間を初期化
                searchTime = 0;
            }

            //対象の位置の方向を向く
            transform.LookAt(nearObj.transform);

            //自分自身の位置から相対的に移動する
            //transform.Translate(Vector3.forward * 0.01f);
        }

        //指定されたタグの中で最も近いものを取得
        public GameObject serchTag(GameObject nowObj, string tagName)
        {
            float tmpDis = 0;           //距離用一時変数
            float nearDis = 0;          //最も近いオブジェクトの距離
            //string nearObjName = "";    //オブジェクト名称
            GameObject targetObj = null; //オブジェクト

            //タグ指定されたオブジェクトを配列で取得する
            foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
            {
                //自身と取得したオブジェクトの距離を取得
                tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

                //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
                //一時変数に距離を格納
                if (nearDis == 0 || nearDis > tmpDis)
                {
                    nearDis = tmpDis;
                    //nearObjName = obs.name;
                    targetObj = obs;
                }

            }
            //最も近かったオブジェクトを返す
            //return GameObject.Find(nearObjName);
            return targetObj;
        }

        // UIの位置を更新する
        private void OnUpdatePosition()
        {
            var cameraTransform = targetCamera.transform;

            // カメラの向きベクトル
            var cameraDir = cameraTransform.forward;
            // オブジェクトの位置
            var targetWorldPos = target.position + worldOffset;
            // カメラからターゲットへのベクトル
            var targetDir = targetWorldPos - cameraTransform.position;

            // 内積を使ってカメラ前方かどうかを判定
            var isFront = Vector3.Dot(cameraDir, targetDir) > 0;

            // カメラ前方ならUI表示、後方なら非表示
            targetUI.gameObject.SetActive(isFront);
            if (!isFront) return;

            // オブジェクトのワールド座標→スクリーン座標変換
            var targetScreenPos = targetCamera.WorldToScreenPoint(targetWorldPos);

            // スクリーン座標変換→UIローカル座標変換
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentUI,
                targetScreenPos,
                null,
                out var uiLocalPos
            );

            // RectTransformのローカル座標を更新
            targetUI.localPosition = uiLocalPos;
        }
    }
}