using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ロックオン
/// </summary>
namespace LockOnTarget
{
    public class LockOnTarget : MonoBehaviour
    {
        [SerializeField]
        private GameObject target;      // ターゲット

        protected void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Enemy")
            {
                target = other.gameObject;
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            if(other.gameObject.tag == "Enemy")
            {
                target = null;
            }
        }

        /// <summary>
        /// ターゲットを取得する
        /// </summary>
        /// <returns></returns>
        public GameObject GetTarget()
        {
            return this.target;
        }

    }

}