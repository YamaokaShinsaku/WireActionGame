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
            Ray ray = Camera.main.ScreenPointToRay(RayPosition);

            RaycastHit hit;

            //Debug.DrawRay(RayPosition, transform.forward * 1000, Color.green);

            if(Physics.Raycast(ray,out hit))
            {
                if(hit.collider.CompareTag("Enemy"))
                {
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
