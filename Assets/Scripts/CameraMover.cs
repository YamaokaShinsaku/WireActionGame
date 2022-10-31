using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private float sight_x;
    private float sight_y;

    [SerializeField]
    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        controller();
    }

    private void controller()
    {
        float x = Input.GetAxis("CameraHorizontal");
        float z = Input.GetAxis("CameraVertical");

        float angleH = Input.GetAxis("Horizontal2") * moveSpeed;
        float angleV = Input.GetAxis("Vertical2") * moveSpeed;

        if(sight_x >= 360)
        {
            sight_x = sight_x - 360;
        }
        else if(sight_x < 0)
        {
            sight_x = 360 - sight_x;
        }

        sight_x = sight_x + angleH;

        if(sight_y > 80)
        {
            if(angleV < 0)
            {
                sight_y = sight_y + angleV;
            }
        }
        else if(sight_y < -90)
        {
            if(angleV > 0)
            {
                sight_y = sight_y + angleV;
            }
        }
        else
        {
            sight_y = sight_y + angleV;
        }

        float dx = Mathf.Sin(sight_x * Mathf.Deg2Rad) * z + Mathf.Sin((sight_x + 90.0f) * Mathf.Deg2Rad) * x;
        float dz = Mathf.Cos(sight_x * Mathf.Deg2Rad) * z + Mathf.Cos((sight_x + 90.0f) * Mathf.Deg2Rad) * x;

        this.transform.Translate(dx, 0, dz, 0.0f);

        this.transform.localRotation = Quaternion.Euler(sight_y, sight_x, 0);

        //Debug.Log("dx:dz \n" + dx + " : " + dz);
    }
}
