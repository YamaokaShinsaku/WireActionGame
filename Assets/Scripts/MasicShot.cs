using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasicShot : MonoBehaviour
{
    public GameObject[] shotObj;       // ��΂�����
    public int count;               // ���˃J�E���g
    private GameObject clone;       // ����̃N���[��
    private GameObject cloneSecond;       // ����̃N���[��
    private GameObject cloneThird;       // ����̃N���[��
    private GameObject cloneForce;       // ����̃N���[��

    [SerializeField]
    public GameObject cloneIdleFire;     // �N���[���𐶐�����ꏊ

    public Vector3 position;
    public Quaternion rotation;

    public Vector3 offset;
    public Transform target;

    private float angle;
    //�@��]����X�s�[�h
    [SerializeField]
    private float rotateSpeed = 180f;
    //�@�^�[�Q�b�g����̋���
    [SerializeField]
    private Vector3 distanceFromTarget;

    private float beforeTrigger;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        for(int i = 0; i < shotObj.Length; i++)
        {
            shotObj[i].GetComponent<Homing_2>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ���W�X�V
        for (int i = 0; i < shotObj.Length; i++)
        {
            //�@���j�b�g�̈ʒu = �^�[�Q�b�g�̈ʒu �{ �^�[�Q�b�g���猩�����j�b�g�̊p�x �~�@�^�[�Q�b�g����̋���
            shotObj[i].transform.position = target.position + Quaternion.Euler(0f, angle, 0f) * distanceFromTarget;
            //�@���j�b�g���g�̊p�x = �^�[�Q�b�g���猩�����j�b�g�̕����̊p�x���v�Z����������j�b�g�̊p�x�ɐݒ肷��
            shotObj[i].transform.rotation =
                Quaternion.LookRotation(shotObj[i].transform.position -
                new Vector3(target.position.x, shotObj[i].transform.position.y, target.position.z), Vector3.up);
            //�@���j�b�g�̊p�x��ύX
            angle += rotateSpeed * Time.deltaTime;
            //�@�p�x��0�`360�x�̊ԂŌJ��Ԃ�
            angle = Mathf.Repeat(angle, 360f);
        }

        float rightTrigger = Input.GetAxis("magicShot");

        if (Input.GetKeyDown(KeyCode.J) || rightTrigger > 0 && beforeTrigger == 0.0f
            && count < 4)
        {
            // ����̃N���[���𐶐�
            clone = Instantiate(shotObj[0], cloneIdleFire.transform.position, cloneIdleFire.transform.rotation);
            cloneSecond = Instantiate(shotObj[1], cloneIdleFire.transform.position, cloneIdleFire.transform.rotation);
            cloneThird = Instantiate(shotObj[2], cloneIdleFire.transform.position, cloneIdleFire.transform.rotation);
            cloneForce = Instantiate(shotObj[3], cloneIdleFire.transform.position, cloneIdleFire.transform.rotation);

            //shotObj.SetActive(false);        // ����{�̂��\����
            clone.GetComponent<Homing_2>().enabled = true;
            cloneSecond.GetComponent<Homing_2>().enabled = true;
            cloneThird.GetComponent<Homing_2>().enabled = true;
            cloneForce.GetComponent<Homing_2>().enabled = true;

            // 3�b��ɃN���[�����폜
            Destroy(clone, 3f);
            Destroy(cloneSecond, 3f);
            Destroy(cloneThird, 3f);
            Destroy(cloneForce, 3f);

            count++;
        }
        // �c�e���ɂ���ăT�C�Y��ύX
        if(count == 1)
        {
            cloneIdleFire.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }
        if (count == 2)
        {
            cloneIdleFire.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        if (count == 3)
        {
            cloneIdleFire.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }
        if (count == 4)
        {
            cloneIdleFire.SetActive(false);
        }


        beforeTrigger = rightTrigger;

        //�@����𑕓U����
        if (Input.GetKeyDown(KeyCode.K) || Input.GetButtonDown("magicSet"))
        {
            count = 0;
            cloneIdleFire.SetActive(true);        // ����{�̂��\����
            cloneIdleFire.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
}
