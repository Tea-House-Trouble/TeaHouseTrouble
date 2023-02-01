using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGlowTrigger : MonoBehaviour
{
    public float Intensity = 2.0f;
    public Material material;
    public float IntensityValue;

    void Start()
    {
        material.SetFloat("_Intensity", 0);
    }
    public void SetLineGlow(bool on)
    {
        IntensityValue = Intensity;
        material.SetFloat("_Intensity", on?IntensityValue:0f);
    }
    

   /* void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            IntensityValue = Intensity;
            material.SetFloat("_Intensity", IntensityValue);
        }
    }
   /* void OnTriggerExit(Collider other)
    {
  
        {

        
            if (other.gameObject.CompareTag("Enemy"))
            {
                void Update()
                    {
                        IntensityValue = Mathf.Clamp(IntensityValue - 5 * Time.deltaTime, 0, 10);
                        material.SetFloat("_Intensity", IntensityValue);
                    }
                
            }
        }
    }*/
}   

