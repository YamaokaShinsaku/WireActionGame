using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDragChanger : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private WeaponChanger.WeaponController wp;

    public bool isSlow;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        wp = wp.GetComponent<WeaponChanger.WeaponController>();

        isSlow = wp.isShot;
    }

    // Update is called once per frame
    void Update()
    {
        isSlow = wp.isShot;

        if(isSlow)
        {
            rb.drag = 10.0f;
        }
        else
        {
            rb.drag = 0.0f;
        }
    }
}
