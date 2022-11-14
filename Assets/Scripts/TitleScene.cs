using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    [SerializeField]
    private SceneTransition sceneTransition;

    [SerializeField]
    private string nextScene;

    // Start is called before the first frame update
    void Start()
    {
        sceneTransition = sceneTransition.GetComponent<SceneTransition>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("SceneChange"))
        {
            sceneTransition.SceneChange(nextScene);
        }
    }
}
