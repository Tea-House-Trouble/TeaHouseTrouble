using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Daruma_Anim_Script : MonoBehaviour
{
   public GameObject Daruma;
   Animator DAR_Animator;
   [SerializeField] ParticleSystem GroundGhostFlames;

   void Start()
    {
        DAR_Animator = gameObject.GetComponent<Animator>();
     }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            DAR_Animator.Play("Hit");
        }
    }

    public void GhostFlames()
        {
            GroundGhostFlames.Play();
        }

    public void GhostFlamesEnd()
        {
            GroundGhostFlames.Stop();
        }
}