using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pauser : MonoBehaviour
{
    static List<Pauser> targets = new List<Pauser>();   // �|�[�Y�Ώۂ̃X�N���v�g
    Behaviour[] pauseBehavs = null; // �|�[�Y�Ώۂ̃R���|�[�l���g

    // ������
    void Start()
    {
        // �|�[�Y�Ώۂɒǉ�����
        targets.Add(this);
    }

    // �j�������Ƃ�
    void OnDestory()
    {
        // �|�[�Y�Ώۂ��珜�O����
        targets.Remove(this);
    }

    // �|�[�Y���ꂽ�Ƃ�
    void OnPause()
    {
        if (pauseBehavs != null)
        {
            return;
        }

        // �L����Behaviour��擾
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

    // �|�[�Y������ꂽ�Ƃ�
    void OnResume()
    {
        if (pauseBehavs == null)
        {
            return;
        }

        // �|�[�Y�O�̏�Ԃ�Behaviour�̗L����Ԃ𕜌�
        foreach (var com in pauseBehavs)
        {
            com.enabled = true;
        }
        pauseBehavs = null;
    }

    // �|�[�Y
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

    // �|�[�Y���
    public static void Resume()
    {
        foreach (var obj in targets)
        {
            obj.OnResume();
        }
    }
}
