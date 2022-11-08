using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDownSpeedChanger : MonoBehaviour
{
    [SerializeField]
    private Rigidbody player;       // �v���C���[��Rigidbody

    [SerializeField]
    private GroundCheck isGround;   // �ݒu����

    [SerializeField]
    private PlayerController.PlayerController playerController;     // �v���C���[�R���g���[���[

    [SerializeField]
    private WeaponChanger.WeaponController isWeapon;            // ����R���g���[���[

    private bool nowWeapon;     // ���ݑ������Ă��镐��

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
        // ���@���g�p���Ă���
        nowWeapon = isWeapon.isShot;
        isGround = isGround.GetComponent<GroundCheck>();

        // ���@���g�p���Ă��āA���󒆂ɂ���Ƃ�
        if (nowWeapon == true && isGround.isGround == false)
        {
            // �v���C���[�̍R�͂�5��
            player.drag = 5.0f;
            playerController.enabled = false;
        }
        
        // ���@���g���Ă��Ȃ��A���v���C���[�̍R�͂�0�ł͂Ȃ��Ƃ�
        if(nowWeapon == false && player.drag != 0)
        {
            // �R�͂�0��
            player.drag = 0.0f;
            playerController.enabled = true;
        }
    }
}
