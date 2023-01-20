using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGlowTrigger : MonoBehaviour
{
    public float Intensity;
    Material material;

    void Start()
    {
        material.SetFloat("_Intensity", 0);
    }

    void OnTriggerInput(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            material.SetFloat("_Intensity", Intensity);
        }
    }
}   

