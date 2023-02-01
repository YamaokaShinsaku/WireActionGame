using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject UIStaff;     // UI�p�̏�I�u�W�F�N�g

    [SerializeField]
    private Image UIMagic;          // UI���@�摜

    [SerializeField]
    private WeaponChanger.WeaponController weaponController;

    // Start is called before the first frame update
    void Start()
    {
        weaponController = weaponController.GetComponent<WeaponChanger.WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        weaponController = weaponController.GetComponent<WeaponChanger.WeaponController>();

        if (weaponController.isWire)
        {
            UIStaff.SetActive(true);
            UIMagic.enabled = false;
        }
        if(weaponController.isShot)
        {
            UIStaff.SetActive(false);
            UIMagic.enabled = true;
        }
    }
}
