using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pauser : MonoBehaviour
{
	static List<Pauser> targets = new List<Pauser>();    // �|�[�Y�Ώۂ̃X�N���v�g

	// �|�[�Y�Ώۂ̃R���|�[�l���g
	Behaviour[] pauseBehavs = null;
	Rigidbody[] rgBodies = null;
	Vector3[] rgBodyVels = null;
	Vector3[] rgBodyAVels = null;

	// 2D�p�R���|�[�l���g
	Rigidbody2D[] rg2dBodies = null;
	Vector2[] rg2dBodyVels = null;
	float[] rg2dBodyAVels = null;

	// ������
	void Start()
	{
		// �|�[�Y�Ώۂɒǉ�����
		targets.Add(this);
	}

	// �j�������Ƃ�
	void OnDestory()
	{
		// �|�[�Y�Ώۂ��珜�O����
		//targets.Remove(this);
	}

	// �|�[�Y���ꂽ�Ƃ�
	public void OnPause()
	{
		if (pauseBehavs != null)
		{
			return;
		}

		// �L����Behaviour���擾
		pauseBehavs = Array.FindAll(GetComponentsInChildren<Behaviour>(), (obj) => { return obj.enabled; });

		foreach (var com in pauseBehavs)
		{
			com.enabled = false;
		}

		rgBodies = Array.FindAll(GetComponentsInChildren<Rigidbody>(), (obj) => { return !obj.IsSleeping(); });
		rgBodyVels = new Vector3[rgBodies.Length];
		rgBodyAVels = new Vector3[rgBodies.Length];
		for (var i = 0; i < rgBodies.Length; ++i)
		{
			rgBodyVels[i] = rgBodies[i].velocity;
			rgBodyAVels[i] = rgBodies[i].angularVelocity;
			rgBodies[i].Sleep();
		}

		rg2dBodies = Array.FindAll(GetComponentsInChildren<Rigidbody2D>(), (obj) => { return !obj.IsSleeping(); });
		rg2dBodyVels = new Vector2[rg2dBodies.Length];
		rg2dBodyAVels = new float[rg2dBodies.Length];
		for (var i = 0; i < rg2dBodies.Length; ++i)
		{
			rg2dBodyVels[i] = rg2dBodies[i].velocity;
			rg2dBodyAVels[i] = rg2dBodies[i].angularVelocity;
			rg2dBodies[i].Sleep();
		}
	}

	// �|�[�Y�������ꂽ�Ƃ�
	public void OnResume()
	{
		if (pauseBehavs == null)
		{
			return;
		}

		// �|�[�Y�O�̏�ԂɃR���|�[�l���g�̗L����Ԃ𕜌�
		foreach (var com in pauseBehavs)
		{
			com.enabled = true;
		}

		for (var i = 0; i < rgBodies.Length; ++i)
		{
			rgBodies[i].WakeUp();
			rgBodies[i].velocity = rgBodyVels[i];
			rgBodies[i].angularVelocity = rgBodyAVels[i];
		}

		for (var i = 0; i < rg2dBodies.Length; ++i)
		{
			rg2dBodies[i].WakeUp();
			rg2dBodies[i].velocity = rg2dBodyVels[i];
			rg2dBodies[i].angularVelocity = rg2dBodyAVels[i];
		}

		pauseBehavs = null;

		rgBodies = null;
		rgBodyVels = null;
		rgBodyAVels = null;

		rg2dBodies = null;
		rg2dBodyVels = null;
		rg2dBodyAVels = null;
	}

	// �|�[�Y
	public static void Pause()
	{
		foreach (var obj in targets)
		{
			obj.OnPause();
		}
	}

	// �|�[�Y����
	public static void Resume()
	{
		foreach (var obj in targets)
		{
			obj.OnResume();
		}
	}
}
