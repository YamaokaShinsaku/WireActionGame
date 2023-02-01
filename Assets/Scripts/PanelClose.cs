using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelClose : MonoBehaviour
{
    public GameObject panel;

    public void Close()
    {
        Debug.Log("close");
        panel.SetActive(false);
    }
}
