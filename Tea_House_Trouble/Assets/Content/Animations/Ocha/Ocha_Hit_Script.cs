using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocha_Hit_Script : MonoBehaviour
{
   Animator OCHA_Animator;
   public GameObject Ocha;

   void Start()
    {
        OCHA_Animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("OchaHit");
            OCHA_Animator.Play("Hit");
        }
    }
}
