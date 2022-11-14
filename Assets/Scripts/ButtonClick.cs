using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public bool sw;

    public void Push()
    {
        sw = true;
        Time.timeScale = 1.0f;
    }

    public void noPush()
    {
        sw = false;
        Time.timeScale = 1.0f;
    }

    public void Update()
    {
        Time.timeScale = 1.0f;
        if (sw)
        {
            Time.timeScale = 1.0f;
            Debug.Log("Push");
        }
    }
}
