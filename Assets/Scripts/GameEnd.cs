using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    void Update()
    {
        EndGame();
    }

    //�Q�[���I��
    public void EndGame()
    {
        //Esc�������ꂽ��
//        if (Input.GetKey(KeyCode.Escape))
//        {

//#if UNITY_EDITOR
//            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
//#else
//    Application.Quit();//�Q�[���v���C�I��
//#endif
//        }

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
#endif

    }
}
