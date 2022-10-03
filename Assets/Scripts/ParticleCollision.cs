using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{

    /// <summary>
    /// パーティクルが他のGameObject(Collider)に当たると呼び出される
    /// </summary>
    /// <param name="other"></param>
    void OnParticleCollision(GameObject other)
	{
		// 当たった相手の色を黒色に変える
		other.gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV();
    }
}
