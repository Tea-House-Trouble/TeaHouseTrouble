using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Daruma_Anim_Script : MonoBehaviour
{
   Animator DAR_Animator;
   [SerializeField] ParticleSystem GroundGhostFlames;
   [SerializeField] ParticleSystem DeathVFX;
    [SerializeField] ParticleSystem Hit;
    [SerializeField] GameObject Body;
   [SerializeField] GameObject Face;
   public float AutoDespawnTimer;
   

   void Start()
    {
        DAR_Animator = gameObject.GetComponent<Animator>();
        StartCoroutine (Despawn());
     }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("DarumaHit");
            if (DAR_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                Hit.Play();
            }
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

    public void DarumaDeathFX()
        {
            DeathVFX.Play();
        }

    public void DarumaDeathDespawn()
        {
            Body.SetActive(false);
            Face.SetActive(false);
        }

    IEnumerator Despawn()
        {
            yield return new WaitForSeconds (AutoDespawnTimer);
            Destroy(gameObject);
        }
}