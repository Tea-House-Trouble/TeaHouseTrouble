using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftColor : MonoBehaviour
{
    public Material material;
    public float intensityValue = 1.0f;
    public float fadeSpeed = 5.0f;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
       
    }

 
    public void PerformAction()

    {
         material.SetFloat("_Intensity", intensityValue);

    }

    private void Update()
    {

        if (intensityValue > 0)
        {
            intensityValue = Mathf.Clamp01(intensityValue - fadeSpeed * Time.deltaTime);

        }

        // timer -= Time.deltaTime;
        // if (timer <= 0)
        // {
        //    intensityValue = 0.0f;
        //   timer = resetTime;
        // }
    }
}


    // Update is called once per frame
   

