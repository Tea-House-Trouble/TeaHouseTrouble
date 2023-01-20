using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerKoiScript : MonoBehaviour
{

    public GameObject enableTargetObject;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("trigger entered");
            
            enableTargetObject.gameObject.SetActive(true);
            
        }
    }
}
