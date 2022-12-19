using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pauser : MonoBehaviour
{
    static List<Pauser> targets = new List<Pauser>();
    Behaviour[] pauseBehavs = null;

    void Start()
    {
        targets.Add(this);
    }

    void OnDestory()
    {
        targets.Remove(this);
    }

    void OnPause()
    {
        if (pauseBehavs != null)
        {
            return;
        }

        pauseBehavs = Array.FindAll(GetComponentsInChildren<Behaviour>(), (obj) => {
            if (obj == null)
            {
                return false;
            }
            return obj.enabled;
        });

        foreach (var com in pauseBehavs)
        {
            com.enabled = false;
        }
    }
    void OnResume()
    {
        if (pauseBehavs == null)
        {
            return;
        }

        foreach (var com in pauseBehavs)
        {
            com.enabled = true;
        }
        pauseBehavs = null;
    }

    public static void Pause()
    {
        foreach (Pauser obj in GameObject.FindObjectsOfType<Pauser>())
        {
            //Debug.Log(obj.gameObject.name);
            if (obj != null)
            {
                obj.OnPause();
            }
        }
    }

    public static void Resume()
    {
        foreach (var obj in targets)
        {
            obj.OnResume();
        }
    }
}
