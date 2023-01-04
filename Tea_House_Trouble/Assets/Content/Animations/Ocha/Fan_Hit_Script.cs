using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan_Hit_Script : MonoBehaviour
{
   Animator FAN_Animator;
   public GameObject FAN;
    [SerializeField] ParticleSystem Hit01;
    [SerializeField] ParticleSystem Hit02;

    void Start()
    {
        FAN_Animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (FAN_Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit01"))
            {
                FAN_Animator.Play("Hit02");
                Hit02.Play();
            }

            else
            {
                FAN_Animator.Play("Hit01");
                Hit01.Play();
            }
            
        }
    }
}
