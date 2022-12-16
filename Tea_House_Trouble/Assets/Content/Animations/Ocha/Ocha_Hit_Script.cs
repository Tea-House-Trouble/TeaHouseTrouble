using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocha_Hit_Script : MonoBehaviour
{
   Animator OCHA_Animator;
   public GameObject Ocha;
    [SerializeField] ParticleSystem Hit01;
    [SerializeField] ParticleSystem Hit02;

    void Start()
    {
        OCHA_Animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (OCHA_Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit01"))
            {
                OCHA_Animator.Play("Hit02");
                Hit02.Play();
            }

            else
            {
                OCHA_Animator.Play("Hit01");
                Hit01.Play();
            }
            
        }
    }
}
