using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftColor : MonoBehaviour
{
    public Material material;
    public float intensityGlow = 3.0f;
    public float intensityValue = 1.0f;
    public float fadeSpeed = 0.1f;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        intensityValue = 0.0f;
    }

 
    public void PerformAction()

    {
         intensityValue = intensityGlow;
         material.SetFloat("_Intensity", intensityValue);

    }

    private void Update()
    {

        if (intensityValue > 0)
        {
            intensityValue = Mathf.Clamp(intensityValue - fadeSpeed * Time.deltaTime, 0, 10);
            material.SetFloat("_Intensity", intensityValue);
        }
        /*
           timer -= Time.deltaTime;
           if (timer <= 0)
           {
              intensityValue = 0.0f;
             timer = resetTime;
           }
        */
    }//Mathf.Clamp(

}


    // Update is called once per frame
   

