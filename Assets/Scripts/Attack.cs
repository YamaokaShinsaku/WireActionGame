using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Attack : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            animator.SetBool("Attack", true);
            //Play(particle);
        }
    }

    public void Blade_Play()
    {
        particle.Play();
    }

    public void Blade_Stop()
    {
        particle.Stop();
    }
}
