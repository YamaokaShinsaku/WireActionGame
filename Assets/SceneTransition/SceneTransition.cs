using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.TransitionKit;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;   // 次のシーンの名前

    private int nextSceneNum;      // 次のシーン

    private int titleSceneNum = 0;
    private int gameSceneNum = 1;

    private void OnGUI()
    {
        // デバッグ用
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneChange(nextSceneName);
        }
        //SceneChange(nextSceneName);
    }

    public void SceneChange(string nextScene)
    {
        Time.timeScale = 1.0f;

        if(SceneManager.GetActiveScene().buildIndex == titleSceneNum)
        {
            nextSceneNum = gameSceneNum;
        }

        if (SceneManager.GetActiveScene().buildIndex == gameSceneNum)
        {
            nextSceneNum = titleSceneNum;
        }

        var fader = new FadeTransition()
        {
            //nextScene = SceneManager.GetSceneByName(nextScene).buildIndex,
            //fadedDelay = 0.2f,
            //fadeToColor = Color.black

            nextScene = nextSceneNum,
            fadeToColor = Color.black
        };
        TransitionKit.instance.transitionWithDelegate(fader);

        //SceneManager.LoadScene(nextScene);
    }

}
