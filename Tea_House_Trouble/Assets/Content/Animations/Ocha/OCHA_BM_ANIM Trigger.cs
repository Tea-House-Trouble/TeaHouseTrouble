using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OCHA_BM_ANIMTrigger : MonoBehaviour
{
   public GameObject Ocha;

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Ocha.GetComponent<Animator>().Play("Hit_LaneBC_4m");
        }
    }
}
