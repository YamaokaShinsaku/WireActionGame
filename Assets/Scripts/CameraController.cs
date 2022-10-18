using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;
    public float rotate_speed;
    public LockOnTarget.LockOnTarget lockonTarget;
    private GameObject lockOnTarget;

    private const int ROTATE_BUTTON = 1;
    private const float ANGLE_LIMIT_UP = 60f;
    private const float ANGLE_LIMIT_DOWN = -60f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");

        lockonTarget = player.GetComponentInChildren<LockOnTarget.LockOnTarget>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position;
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject target = lockonTarget.GetTarget();

            if (target != null)
            {
                lockOnTarget = target;
            }
            else
            {
                lockOnTarget = null;
            }
        }

        if (lockonTarget)
        {
            lockOnTargetObject(lockOnTarget);
        }
        else
        {
            if (Input.GetMouseButton(ROTATE_BUTTON))
            {
                rotateCmaeraAngle();
            }
        }

        float angle_x = 180f <= transform.eulerAngles.x ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
        transform.eulerAngles = new Vector3(
            Mathf.Clamp(angle_x, ANGLE_LIMIT_DOWN, ANGLE_LIMIT_UP),
            transform.eulerAngles.y,
            transform.eulerAngles.z
        );
    }

    private void lockOnTargetObject(GameObject target)
    {
        this.transform.LookAt(target.transform, Vector3.up);
    }

    private void rotateCmaeraAngle()
    {
        Vector3 angle = new Vector3(
               Input.GetAxis("Mouse X") * rotate_speed,
               Input.GetAxis("Mouse Y") * rotate_speed,
               0
           );

        transform.eulerAngles += new Vector3(angle.y, angle.x);
    }
}
