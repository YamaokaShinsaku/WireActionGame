using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField]
    private GameObject target;      // �Ǐ]���������^�[�Q�b�g

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        //�Q�[���J�n���_�̃J�����ƃ^�[�Q�b�g�̋������擾
        offset = gameObject.transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        // �J�����̈ʒu���^�[�Q�b�g�̈ʒu�ɃI�t�Z�b�g�𑫂����ꏊ�ɂ���
        gameObject.transform.position = target.transform.position + offset;

    }
}
