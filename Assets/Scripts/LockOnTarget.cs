using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���b�N�I��
/// </summary>
namespace LockOnTarget
{
    public class LockOnTarget : MonoBehaviour
    {
        [SerializeField]
        private GameObject target;      // �^�[�Q�b�g

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
        /// �^�[�Q�b�g���擾����
        /// </summary>
        /// <returns></returns>
        public GameObject GetTarget()
        {
            return this.target;
        }

    }

}