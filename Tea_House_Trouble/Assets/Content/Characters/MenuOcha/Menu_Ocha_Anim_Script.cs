using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Ocha_Anim_Script : MonoBehaviour
{
    
    Animator MENU_OCHA_Animator;
    bool IdleLoop;
    int wait_time = 2;


void Start()
    {
        MENU_OCHA_Animator = gameObject.GetComponent<Animator>();
        bool IdleLoop;
        StartCoroutine (looper());
    }


IEnumerator looper()
    {
        while(IdleLoop==true)
        {
            //int wait_time = Random.Range (3, 8);
            yield return new WaitForSeconds (wait_time);
            print ("IdleBreaker");
    
        }
         
    }
}
