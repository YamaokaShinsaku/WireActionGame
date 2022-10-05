using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing_2 : MonoBehaviour
{
    private float DivNum = 40;      //������
    private float Counter = 0f;     //���[�U�[�i�s�t���[���p

    public GameObject target;       // �ڕW�I�u�W�F�N�g
    private float u = 0f;           //�x�W�F�Ȑ��ʒu�p

    private Vector3 P0 = Vector3.zero;      // �x�W�F�Ȑ��̔����ʒu

    private Vector3 P1 = Vector3.zero;      // �x�W�F�Ȑ��̉����̖ڕW���W

    private Vector3 P2 = Vector3.zero;      // �x�W�F�Ȑ��̐^�񒆕t�߂̖ڕW���W

    private Vector3 P3 = Vector3.zero;      // �ڕW���W

    private Vector3 P01 = Vector3.zero;

    private Vector3 P12 = Vector3.zero;

    private Vector3 P23 = Vector3.zero;

    private Vector3 P02 = Vector3.zero;

    private Vector3 P13 = Vector3.zero;

    private Vector3 P03 = Vector3.zero;

    /// <summary>
    /// �ڕW���W�𐶐�
    /// </summary>
    /// <param name="x">P3.x</param>
    /// <param name="y">P3.y</param>
    /// <param name="z">P3.z</param>
    public void Create_P3xyz(float x, float y, float z)
    {
        P3 = new Vector3(x, y, z);
    }

    void Start()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);//�摜��]

        // �x�W�F�Ȑ������ʒu
        P0 = new Vector3(
            this.transform.position.x,
            this.transform.position.y,
            this.transform.position.z);

        // �x�W�F�Ȑ������̖ڕW���W
        P1 = new Vector3(
            Random.Range(-5.0f, 5.0f),
            Random.Range(0.0f,  0.0f),
            Random.Range(-5.0f, 5.0f));

        // �x�W�F�Ȑ��^�񒆕t�߂̖ڕW���W
        P2 = new Vector3(
            Random.Range(-5.0f, 5.0f),
            Random.Range(0.0f, 0.0f),
            Random.Range(-5.0f, 5.0f));

        // �ڕW���W
        Create_P3xyz(target.transform.position.x, target.transform.position.y, target.transform.position.z);

    }

    void Update()
    {
        // �x�W�F�Ȑ��̈ʒu���ړ�
        u = (1.0f / DivNum) * Counter;//�x�W�F�Ȑ��̈ʒu���ړ�������

        P01 = new Vector3(
            (1.0f - u) * P0.x + u * P1.x,
            (1.0f - u) * P0.y + u * P1.y,
            (1.0f - u) * P0.z + u * P1.z);

        P12 = new Vector3(
            (1.0f - u) * P1.x + u * P2.x,
            (1.0f - u) * P1.y + u * P2.y,
            (1.0f - u) * P1.z + u * P2.z);

        P23 = new Vector3(
            (1.0f - u) * P2.x + u * P3.x,
            (1.0f - u) * P2.y + u * P3.y,
            (1.0f - u) * P2.z + u * P3.z);

        P02 = new Vector3(
            (1.0f - u) * P01.x + u * P12.x,
            (1.0f - u) * P01.y + u * P12.y,
            (1.0f - u) * P01.z + u * P12.z);

        P13 = new Vector3(
            (1.0f - u) * P12.x + u * P23.x,
            (1.0f - u) * P12.y + u * P23.y,
            (1.0f - u) * P12.z + u * P23.z);

        P03 = new Vector3(
            (1.0f - u) * P02.x + u * P13.x,
            (1.0f - u) * P02.y + u * P13.y,
            (1.0f - u) * P02.z + u * P13.z);

        Vector3 pos = transform.position;
        pos = P03;

        // �\�����W
        this.transform.position = pos;//���[�U�[�\�����W

        // ����
        Counter += 0.025f;//���[�U�[�̑����G

        if (u >= 1.0f)
        {
            //this.transform.DetachChildren();
            //Destroy(this.gameObject);
        }
    }
}
