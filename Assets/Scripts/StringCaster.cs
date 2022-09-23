using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StringPlayer
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(LineRenderer))]
    public class StringCaster : MonoBehaviour
    {
        [SerializeField]
        private float maxDistance = 100.0f; // 糸を伸ばせる最大距離

        [SerializeField]
        private LayerMask interactiveLayers;    // 糸をくっつけられるレイヤー

        [SerializeField]
        private Vector3 casterCenter = new Vector3(0.0f, 0.5f, 0.0f);   // オブジェクトのローカル座標であらわした糸の射出位置

        [SerializeField]
        private Transform casterCenterObj;     // 糸の射出位置のオブジェクト

        [SerializeField]
        [Min(0.0f)] private float obstacleDetectionMarginStart = 1.0f; // 自分に十分近い物体は糸遮蔽判定から除外させる
        
        [SerializeField]
        [Min(0.0f)] private float obstacleDetectionMarginEnd = 1.0f; // 接着点に十分近い物体は糸遮蔽判定から除外させる

        [SerializeField]
        private float spring = 50.0f;       // 糸の物理挙動を担当するSpringJointのSpring

        [SerializeField]
        private float damper = 20.0f;       // 糸の物理挙動を担当するSpringJointのdamper

        [SerializeField]
        private float equilibrimLength = 1.0f;     // 糸を縮めた時の自然長

        [SerializeField]
        private AvatarIKGoal ikGoal = AvatarIKGoal.RightHand;

        [SerializeField]
        private float ikTransitionTime = 0.5f;      // 腕の位置の遷移時間

        [SerializeField]
        private RawImage reticle;       // 糸を張れるかどうかの状態に合わせて、照準マークを変更する

        [SerializeField]
        private Texture reticleImageValid;      // 照準マーク

        [SerializeField]
        private Texture reticleImageInValid;    // 禁止マーク

        [SerializeField]
        private ParticleSystem particle;    // エフェクト（集中線）

        [SerializeField]
        private float bulletTimeCount;         // バレットタイムの制限時間

        [SerializeField]
        bool BulletTime;        // バレットタイム中かどうか

        [SerializeField]
        private GameObject Crystal;

        private GameObject clone;

        private Animator animator;
        private Camera mainCamera;
        private LineRenderer lineRenderer;
        private SpringJoint springJoint;

        // 右手を伸ばす、戻す動作をスムーズにするため
        private float currentIkWeight;  // 現在のウェイト
        private float targetIkWeight;   // 目標ウェイト
        private float ikWeightVelocity; // ウェイト変化率

        private bool casting;               // 糸が射出中かどうか
        private bool needsUpdateSpring;     // FixedUpdate中でSpringJointの状態が必要かどうか
        private float stringLength;         // 現在の糸の長さ...これをFixedUpdate中でSpringJointのmaxDistanceにセットする
        //private readonly Vector3[] stringAnchor = new Vector3[2];   // SpringJointのプレイヤー側と接着面側の末端
        private readonly Anchor[] stringAnchor = new Anchor[2];     // 末端情報にRigidBodyを持たせる
        private Vector3 worldCasterCenter;   // casterCenterをワールド座標に変換したもの

        // アンカーを定義
        private readonly struct Anchor
        {
            public readonly Vector3 LocalPosition;
            public readonly Rigidbody ConnectedBody;

            public Vector3 WorldPosition => this.ConnectedBody == null
                ? this.LocalPosition
                : this.ConnectedBody.transform.TransformPoint(this.LocalPosition);

            public Anchor(Vector3 worldPosition, Rigidbody connectedBody = null)
            {
                this.LocalPosition = connectedBody == null
                    ? worldPosition
                    : connectedBody.transform.TransformPoint(worldPosition);
                this.ConnectedBody = connectedBody;
            }

        }

        /// <summary>
        /// エフェクト再生
        /// </summary>
        private void Play()
        {
            particle.Play();
        }
        /// <summary>
        /// エフェクト停止
        /// </summary>
        private void Stop()
        {
            particle.Stop();
        }

        private void Awake()
        {
            // コンポーネントへの参照を取得
            this.animator = this.GetComponent<Animator>();
            this.mainCamera = Camera.main;
            this.lineRenderer = this.GetComponent<LineRenderer>();

            // worldCasterCenterの初期化
            //this.worldCasterCenter = this.transform.TransformPoint(this.casterCenter);
            this.worldCasterCenter = casterCenterObj.transform.position;

            bulletTimeCount = 5.0f;
            BulletTime = false;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            /// バレットタイムの設定 ///
            if (BulletTime)
            {
                Play();
                Time.timeScale = 0.01f;
                bulletTimeCount -= Time.unscaledDeltaTime;

                // バレットタイムを終了する（デバッグ）
                if (Input.GetMouseButtonDown(0))
                {
                    BulletTime = false;
                    //Stop();
                }
            }
            else
            {
                Stop();
                Time.timeScale = 1.0f;
                bulletTimeCount = 5.0f;
            }

            if (bulletTimeCount <= 0.0f)
            {
                BulletTime = false;
            }
            ///----------------------///

            ///  糸の射出方向を設定する  ///
            // 画面中心から正面に伸びるRayを求める
            //this.worldCasterCenter = this.transform.TransformPoint(this.casterCenter);
            //var cameraForward = this.cameraTransform.forward;
            //var cameraRay = new Ray(this.cameraTransform.position, cameraForward);

            // 求めたRayの衝突点に向かうRayを求める
            //var aimingRay = new Ray(
            //    this.worldCasterCenter,
            //    Physics.Raycast(cameraRay, out var focus, float.PositiveInfinity, this.interactiveLayers)
            //    ? focus.point - this.worldCasterCenter
            //    : cameraForward);

            // 射出方向のmaxDistance以内の距離に糸が接着可能な物体があれば、糸を射出できる
            //if (Physics.Raycast(aimingRay, out var aimingTarget, this.maxDistance, this.interactiveLayers))
            //{
            //    // reticleの表示を照準マークに変える
            //    this.reticle.texture = this.reticleImageValid;
            //    // 発射ボタンが押されたら
            //    if (/*Input.GetButtonDown("Shot")*/Input.GetMouseButtonDown(1))
            //    {
            //        BulletTime = false;

            //        this.stringAnchor[1] = aimingTarget.point;  // 糸の接着面末端を設定
            //        this.casting = true;
            //        this.targetIkWeight = 1.0f;     // IK目標ウェイトを１にする ... 右手を射出方向に伸ばす
            //        // 糸の長さを設定
            //        this.stringLength = Vector3.Distance(this.worldCasterCenter, aimingTarget.point);
            //        this.needsUpdateSpring = true;
            //    }
            //}
            //else
            //{
            //    // 糸が接着不可なら、reticleの表示を禁止マークに
            //    this.reticle.texture = this.reticleImageInValid;
            //}

            // マウス座標にクリスタルを生成
            //Vector3 pos = new Vector3(Input.mousePosition.x,Input.mousePosition.y,maxDistance);
            Ray Cray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Chit = new RaycastHit();

            if(Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(Cray,out Chit))
                {
                    clone = Instantiate(Crystal, Chit.point, Quaternion.identity);
                }
            }

            this.worldCasterCenter = this.casterCenterObj != null ? this.casterCenterObj.position
                : this.transform.TransformPoint(this.casterCenter);
            var mousePosition = Input.mousePosition;
            var pointerRay = this.mainCamera.ScreenPointToRay(mousePosition);
            var aimingRay = new Ray(
                this.worldCasterCenter,
                Physics.Raycast(pointerRay, out var focus, float.PositiveInfinity, this.interactiveLayers)
                ? focus.point - this.worldCasterCenter
                : pointerRay.direction);

            // レティクルの位置をマウスポインターの位置に
            var reticleTransform = this.reticle.transform as RectTransform;
            var reticleRootCanvas = reticle.canvas.rootCanvas;
            if(RectTransformUtility.ScreenPointToLocalPointInRectangle(
                reticleTransform.parent as RectTransform,
                mousePosition,
                reticleRootCanvas.renderMode == RenderMode.ScreenSpaceOverlay 
                ? null
                : reticleRootCanvas.worldCamera,
                out var reticlePosition))
            {
                reticleTransform.localPosition = reticlePosition;
            }

            if (Physics.Raycast(aimingRay, out var aimingTarget, this.maxDistance, this.interactiveLayers))
            {
                // reticleの表示を照準マークに変える
                this.reticle.texture = this.reticleImageValid;
                // 発射ボタンが押されたら
                if (/*Input.GetButtonDown("Shot")*/Input.GetMouseButtonDown(1))
                {
                    BulletTime = false;

                    this.stringAnchor[1] = new Anchor(aimingTarget.point, aimingTarget.rigidbody);
                    this.casting = true;
                    this.targetIkWeight = 1.0f;     // IK目標ウェイトを１にする ... 右手を射出方向に伸ばす
                    // 糸の長さを設定
                    this.stringLength = Vector3.Distance(this.worldCasterCenter, aimingTarget.point);
                    this.needsUpdateSpring = true;
                }
            }
            else
            {
                // 糸が接着不可なら、reticleの表示を禁止マークに
                this.reticle.texture = this.reticleImageInValid;
            }

            // 糸を射出中にの状態で収縮ボタンが押されたら、糸の長さをequilibrimLengthまで縮小する
            if (this.casting && /*Input.GetButtonDown("Contract")*/Input.GetMouseButtonDown(0))
            {
                this.stringLength = this.equilibrimLength;
                this.needsUpdateSpring = true;
            }

            // 発射ボタンが離されたら
            if (/*Input.GetButtonUp("Shot")*/Input.GetMouseButtonUp(1))
            {
                BulletTime = true;

                this.casting = false;
                this.targetIkWeight = 0.0f;     // IK目標ウェイトを0にする ... 右手を待機状態に戻そうとする
                this.needsUpdateSpring = true;

                Destroy(clone);
            }

            // 右腕のIKウェイトを滑らかに変化させる
            this.currentIkWeight = Mathf.SmoothDamp(
                this.currentIkWeight,
                this.targetIkWeight,
                ref this.ikWeightVelocity,
                this.ikTransitionTime);

            // 糸の状態を更新する
            this.UpdateString();
        }

        /// <summary>
        /// 糸の状態を更新
        /// </summary>
        private void UpdateString()
        {
            // 糸を射出中ならlineRendererをアクティブに、そうでないならfalseに
            //if (this.lineRenderer.enabled = this.casting)
            //{
            //    // 糸を射出中のみ処理をする
            //    // 糸のプレイヤー側の末端を設定
            //    this.stringAnchor[0] = this.worldCasterCenter;

            //    // プレイヤーと接着面との間に障害物があるかチェック
            //    if (Physics.Linecast(this.stringAnchor[0], this.stringAnchor[1],
            //        out var obstacle, this.interactiveLayers))
            //    {
            //        // 障害物があれば、接着点を障害物に変更する
            //        this.stringAnchor[1] = obstacle.point;
            //        this.stringLength = Mathf.Min(
            //            Vector3.Distance(this.stringAnchor[0], this.stringAnchor[1]), this.stringLength);
            //        this.needsUpdateSpring = true;
            //    }

                ///  糸の描画設定
                // 糸の端点同士の距離とstringLengthとの乖離具合によって糸を赤くする
                // 糸が赤くなれば、stringLengthが縮もうとしている
            //    this.lineRenderer.SetPositions(this.stringAnchor);
            //    var gbValue = Mathf.Exp(this.springJoint != null
            //        ? -Mathf.Max(Vector3.Distance(this.stringAnchor[0],
            //        this.stringAnchor[1]) - this.stringLength, 0.0f)
            //        : 0.0f);

            //    var stringColor = new Color(1.0f, gbValue, gbValue);
            //    this.lineRenderer.startColor = stringColor;
            //    this.lineRenderer.endColor = stringColor;
            //}

            if(this.lineRenderer.enabled = this.casting)
            {
                // 糸を射出中のみ処理をする
                // 糸のプレイヤー側の末端を設定
                this.stringAnchor[0] = new Anchor(this.worldCasterCenter);

                var anchorPositionStart = this.stringAnchor[0].WorldPosition;
                var anchorPositionEnd = this.stringAnchor[1].WorldPosition;

                if(this.stringLength > (this.obstacleDetectionMarginStart + this.obstacleDetectionMarginEnd))
                {
                    var start = Vector3.MoveTowards(
                        anchorPositionEnd,
                        anchorPositionStart,
                        this.stringLength - this.obstacleDetectionMarginStart);

                    var end = Vector3.MoveTowards(
                        anchorPositionStart,
                        anchorPositionEnd,
                        this.stringLength - this.obstacleDetectionMarginEnd);

                    // プレイヤーと接着面との間に障害物があるかチェック
                    if (Physics.Linecast(start, end, out var obstacle, this.interactiveLayers))
                    {
                        // 障害物があれば、接着点を障害物に変更する
                        this.stringAnchor[1] = new Anchor(obstacle.point, obstacle.rigidbody);
                        anchorPositionEnd = this.stringAnchor[1].WorldPosition;

                        this.stringLength = Mathf.Min(
                            Vector3.Distance(anchorPositionStart, anchorPositionEnd), this.stringLength);
                        this.needsUpdateSpring = true;
                    }
                }

                ///  糸の描画設定
                // 糸の端点同士の距離とstringLengthとの乖離具合によって糸を赤くする
                // 糸が赤くなれば、stringLengthが縮もうとしている
                this.lineRenderer.SetPosition(0, anchorPositionStart);
                this.lineRenderer.SetPosition(1, anchorPositionEnd);
                var gbValue = Mathf.Exp(
                    this.springJoint != null
                    ? -Mathf.Max(Vector3.Distance(anchorPositionStart, anchorPositionEnd)
                    - this.stringLength, 0.0f)
                    : 0.0f);

                var stringColor = new Color(1.0f, gbValue, gbValue);
                this.lineRenderer.startColor = stringColor;
                this.lineRenderer.endColor = stringColor;
            }
        }

        // 右腕の姿勢を設定し、右腕から糸を出しているように見せる
        private void OnAnimatorIK(int layerIndex)
        {
            //this.animator.SetIKPosition(AvatarIKGoal.RightHand, this.stringAnchor[1]);
            //this.animator.SetIKPositionWeight(AvatarIKGoal.RightHand, this.currentIkWeight);

            this.animator.SetIKPosition(this.ikGoal, this.stringAnchor[1].WorldPosition);
            this.animator.SetIKPositionWeight(this.ikGoal, this.currentIkWeight);
        }

        // SpringJointの更新
        private void FixedUpdate()
        {
            // 更新不要なら
            if (!this.needsUpdateSpring)
            {
                // 何もしない
                return;
            }

            // 糸を射出中なら
            if (this.casting)
            {
                // SpringJointが張られていないとき
                if (this.springJoint == null)
                {
                    // 糸を張る
                    this.springJoint = this.gameObject.AddComponent<SpringJoint>();
                    this.springJoint.autoConfigureConnectedAnchor = false;
                    //this.springJoint.anchor = this.casterCenter;
                    //this.springJoint.anchor = this.casterCenterObj.position;
                    this.springJoint.spring = this.spring;
                    this.springJoint.damper = this.damper;
                }

                this.springJoint.anchor = this.transform.InverseTransformPoint(this.worldCasterCenter);
                this.springJoint.maxDistance = this.stringLength;

                // SpringJointの自然長と接続先を設定
                //this.springJoint.maxDistance = this.stringLength;
                //this.springJoint.connectedAnchor = this.stringAnchor[1];

                this.springJoint.connectedBody = this.stringAnchor[1].ConnectedBody;
                this.springJoint.connectedAnchor = this.stringAnchor[1].LocalPosition;
            }
            else
            {
                // 射出中でなければSpringJointを削除し、
                // 糸による引っぱりを起こらなくする
                Destroy(this.springJoint);
                this.springJoint = null;
            }

            this.needsUpdateSpring = false;
        }
    }

}