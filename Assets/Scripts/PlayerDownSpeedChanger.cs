using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDownSpeedChanger : MonoBehaviour
{
    [SerializeField]
    private Rigidbody player;       // プレイヤーのRigidbody

    [SerializeField]
    private GroundCheck isGround;   // 設置判定

    [SerializeField]
    private PlayerController.PlayerController playerController;     // プレイヤーコントローラー

    [SerializeField]
    private WeaponChanger.WeaponController isWeapon;            // 武器コントローラー

    private bool nowWeapon;     // 現在装備している武器

    // Start is called before the first frame update
    void Start()
    {
        player = player.GetComponent<Rigidbody>();
        isWeapon = isWeapon.GetComponent<WeaponChanger.WeaponController>();
        playerController = playerController.GetComponent<PlayerController.PlayerController>();
        isGround = isGround.GetComponent<GroundCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        // 魔法を使用している
        nowWeapon = isWeapon.isShot;
        isGround = isGround.GetComponent<GroundCheck>();

        // 魔法を使用していて、かつ空中にいるとき
        if (nowWeapon == true && isGround.isGround == false)
        {
            // プレイヤーの抗力を5に
            player.drag = 5.0f;
            playerController.enabled = false;
        }
        
        // 魔法を使っていない、かつプレイヤーの抗力が0ではないとき
        if(nowWeapon == false && player.drag != 0)
        {
            // 抗力を0に
            player.drag = 0.0f;
            playerController.enabled = true;
        }
    }
}
