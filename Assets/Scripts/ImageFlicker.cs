using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFlicker : MonoBehaviour
{
	[SerializeField]
	Image img;

	[Header("1���[�v�̒���(�b�P��)")]
	[SerializeField]
	[Range(0.1f, 10.0f)]
	float duration = 1.0f;

	//�X�N���v�g�Ŏw�肵�����ꍇ��[SerializeField]���R�����g�A�E�g����B
	//���[�v�J�n���̐F��0�`255�܂ł̐����Ŏw��B
	//���摜�����̏ꍇ�́A�w�肵���F�ɂȂ�B�h�b�g�G���̏ꍇ�́A���F���w�肷��ƌ��摜�ւ̉e���Ȃ��B�A���t�@�l�[���Ŋ��S�ɓ����B
	[Header("���[�v�J�n���̐F")]
	[SerializeField]
	Color32 startColor = new Color32(255, 255, 255, 255);
	//���[�v�I��(�܂�Ԃ�)���̐F��0�`255�܂ł̐����Ŏw��B
	[Header("���[�v�I�����̐F")]
	[SerializeField]
	Color32 endColor = new Color32(255, 255, 255, 128);


	//�C���X�y�N�^�[����ݒ肵���ꍇ�́AGetComponent����K�v���Ȃ��Ȃ�ׁAAwake���폜���Ă��ǂ��B
	void Awake()
	{
		if (img == null)
			img = GetComponent<Image>();
	}

	void Update()
	{
		//Color.Lerp�ɊJ�n�̐F�A�I���̐F�A0�`1�܂ł�float��n���ƒ��Ԃ̐F���Ԃ����B
		//Mathf.PingPong�Ɍo�ߎ��Ԃ�n���ƁA0�`1�܂ł̒l���Ԃ����B
		img.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time / duration, 1.0f));
	}
}
