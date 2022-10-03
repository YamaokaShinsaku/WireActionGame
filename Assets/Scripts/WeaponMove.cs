using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMove : MonoBehaviour
{
    public GameObject lookTarget;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(lookTarget)
        {
            var direction = lookTarget.transform.position - this.transform.position;
            direction.y = 0;

            var lookRotation = Quaternion.LookRotation(direction, new Vector3(0.0f,0.0f,0.0f));
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, 0.1f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
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
