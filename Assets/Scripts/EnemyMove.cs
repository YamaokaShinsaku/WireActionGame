using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        //if(target.GetComponent<DangerArea>().isArea == true)
        //{
        //    GetComponent<Renderer>().material.color = Color.red;
        //    transform.LookAt(target.transform);
        //    transform.position += transform.forward * speed;
        //}
=======
        if (target.GetComponent<DangerArea>().isArea == true)
        {
            GetComponent<Renderer>().material.color = Color.red;
            transform.LookAt(target.transform);
            transform.position += transform.forward * speed;
        }
>>>>>>> main
        //else
        //{
        //    GetComponent<Renderer>().material.color = Color.white;
        //}
    }
}
