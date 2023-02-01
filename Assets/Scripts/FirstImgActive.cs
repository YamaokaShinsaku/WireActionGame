using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstImgActive : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Image;

    [SerializeField]
    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            for (int i = 0; i < Image.Length; i++)
            {
                Image[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < Image.Length; i++)
            {
                Image[i].SetActive(false);
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isActive = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isActive = false;
        }
    }
}
