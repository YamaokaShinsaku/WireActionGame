using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{

    /// <summary>
    /// �p�[�e�B�N��������GameObject(Collider)�ɓ�����ƌĂяo�����
    /// </summary>
    /// <param name="other"></param>
    void OnParticleCollision(GameObject other)
	{
		// ������������̐F�����F�ɕς���
		other.gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV();
    }
}
