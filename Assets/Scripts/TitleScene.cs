using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    [SerializeField]
    private SceneTransition sceneTransition;

    public string nextGameScene;

    public string nextTutrialScene;

    // Start is called before the first frame update
    void Start()
    {
        sceneTransition = sceneTransition.GetComponent<SceneTransition>();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 1.0f;
        if(Input.GetButtonDown("GameSceneChange"))
        {
            sceneTransition.nextSceneName = nextGameScene;
            sceneTransition.SceneChange(nextGameScene);
        }
        else if (Input.GetButtonDown("TutrialSceneChange"))
        {
            sceneTransition.nextSceneName = nextTutrialScene;
            sceneTransition.SceneChange(nextTutrialScene);
        }
    }
}
