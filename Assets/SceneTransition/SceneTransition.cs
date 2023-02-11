using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.TransitionKit;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string nextSceneName;   // 次のシーンの名前

    private int nextSceneNum;      // 次のシーン

    private int titleSceneNum = 0;
    private int gameSceneNum = 1;
    private int tutrialSceneNum = 2;

    public void SceneChange(string nextScene)
    {
        Time.timeScale = 1.0f;

        if (SceneManager.GetActiveScene().buildIndex == titleSceneNum)
        {
            if(nextSceneName == "StageCreate")
            {
                nextSceneNum = gameSceneNum;
            }
            else if(nextSceneName == "TutrialScene")
            {
                nextSceneNum = tutrialSceneNum;
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == gameSceneNum)
        {
            nextSceneNum = titleSceneNum;
        }

        if (SceneManager.GetActiveScene().buildIndex == tutrialSceneNum)
        {
            nextSceneNum = titleSceneNum;
        }

        var fader = new FadeTransition()
        {
            nextScene = nextSceneNum,
            fadeToColor = Color.black
        };
        TransitionKit.instance.transitionWithDelegate(fader);
    }

}
