using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject attackEffect;    // 攻撃エフェクト

    private GameObject clone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            attackEffect.transform.position =
            new Vector3(player.transform.position.x, player.transform.position.y + 0.8f, player.transform.position.z);
            clone = Instantiate(attackEffect, attackEffect.transform.position, Quaternion.identity);
        }
    }
}
