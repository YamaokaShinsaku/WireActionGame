using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMove : MonoBehaviour
{
    public float nowPosition;

    // Start is called before the first frame update
    void Start()
    {
        nowPosition = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =
            new Vector3(transform.position.x,
            nowPosition + Mathf.PingPong(Time.time / 3, 0.3f),
            transform.position.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("enemyHit");
            collision.gameObject.GetComponent<Renderer>().material.color = Color.black;
        }
        else
        {
            Debug.Log("elseHit");
        }
    }
}
