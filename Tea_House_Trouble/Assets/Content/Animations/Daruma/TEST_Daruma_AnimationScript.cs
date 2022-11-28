using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daruma_AnimationScript : MonoBehaviour
{
    public GameObject Daruma;
    bool KeepWaiterRunning;
    Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        KeepWaiterRunning = true;
        StartCoroutine (waiter());
     }
 
 //Play Idle Breaker every 3-8 seconds
 IEnumerator waiter()
     {
         while(KeepWaiterRunning==true)
         {
            int wait_time = Random.Range (3, 8);
            yield return new WaitForSeconds (wait_time);
            //print ("IdleBreak");
            m_Animator.SetBool("Idle Breaker", true);
         }
         
     }

//Back to Idle through Animation Event
     public void DisableIdleBreaker()
        {
            m_Animator.SetBool("Idle Breaker", false);
        }

}
