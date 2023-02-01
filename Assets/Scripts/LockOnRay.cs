using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LockOn_Ray
{
    public class LockOnRay : MonoBehaviour
    {
        [SerializeField]
        private GameObject aimImg;

        // Update is called once per frame
        void Update()
        {
            // âÊñ íÜâõÇÃç¿ïWÇéÊìæ
            int screenWidth = Screen.width / 2;
            int screenHeight = Screen.height / 2;

            Vector3 RayPosition = new Vector3(screenWidth, screenHeight, 0.1f);

            // RayÇÃçÏê¨
            //Ray ray = Camera.main.ScreenPointToRay(RayPosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            
            //Debug.DrawRay(RayPosition, this.transform.forward * 1000, Color.green);

            //if(Physics.Raycast(ray,out hit))
            if(Physics.SphereCast(this.transform.position, 20.0f, this.transform.forward, out hit))
            {
                GameObject gameObject = hit.collider.gameObject;
                //Debug.DrawRay(this.transform.position, ray.direction, Color.green);
                if (hit.collider.CompareTag("Enemy"))
                {
                    Debug.Log(gameObject.name);
                    aimImg.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                }
                else
                {
                    aimImg.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }
            }
            else
            {
                aimImg.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
    }
}
