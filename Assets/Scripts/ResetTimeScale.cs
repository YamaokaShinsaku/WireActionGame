using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTimeScale : MonoBehaviour
{
    [SerializeField]
    private PostEffect postEffect;      // グレースケール

    private float beforeLeftTrigger;

    // Start is called before the first frame update
    void Start()
    {
        postEffect = postEffect.GetComponent<PostEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        float leftTrigger = Input.GetAxis("StopBulletTime");

        if (leftTrigger > 0 && beforeLeftTrigger == 0.0f)
        {
            Time.timeScale = 1.0f;

            // グレースケールをoff
            postEffect.enabled = false;
        }

        beforeLeftTrigger = leftTrigger;
    }
}
