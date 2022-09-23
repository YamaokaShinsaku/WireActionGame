using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSlash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Stage")
        {
            Debug.Log("StageHit");
        }
    }
}
