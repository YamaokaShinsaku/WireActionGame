using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerArea : MonoBehaviour
{
    public bool isArea;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "DangerArea")
        {
            isArea = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "DangerArea")
        {
            isArea = false;
        }
    }
}
