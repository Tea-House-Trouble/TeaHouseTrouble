using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Koinobori_Anim_Script : MonoBehaviour
{
   Animator KOI_Animator;
   [SerializeField] ParticleSystem GroundGhostFlames;
   public float AutoDespawnTimer;
   

   void Start()
    {
        KOI_Animator = gameObject.GetComponent<Animator>();
        StartCoroutine (KoiDespawn());
     }

    public void KoiGhostFlamesEnd()
        {
            GroundGhostFlames.Stop();
        }


    IEnumerator KoiDespawn()
        {
            yield return new WaitForSeconds (AutoDespawnTimer);
            Destroy(gameObject);
        }
}