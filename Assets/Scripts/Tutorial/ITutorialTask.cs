using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITutorialTask
{
    /// <summary>
    /// �`���[�g���A���̃^�C�g�����擾
    /// </summary>
    /// <returns>�`���[�g���A���̃^�C�g��</returns>
    string GetTitle();

    /// <summary>
    /// ���������擾
    /// </summary>
    /// <returns>������</returns>
    string GetText();

    /// <summary>
    /// �`���[�g���A���^�X�N���ݒ肳�ꂽ�ۂɎ��s
    /// </summary>
    void OnTaskSetting();

    /// <summary>
    /// �`���[�g���A�����B�����ꂽ���𔻒�
    /// </summary>
    /// <returns>�B�����ꂽ���ǂ���</returns>
    bool CheckTask();

    /// <summary>
    /// ���̃^�X�N�֑J�ڂ��邽�߂̎���
    /// </summary>
    /// <returns>���ԁi�b�j</returns>
    float GetTransitionTime();
}
