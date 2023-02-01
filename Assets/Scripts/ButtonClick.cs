using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public bool sw;

    public void Push()
    {
        sw = true;
    }

    public void noPush()
    {
        sw = false;
    }

    public void Update()
    {
        if (sw)
        {
            Debug.Log("Push");
        }
    }
}
