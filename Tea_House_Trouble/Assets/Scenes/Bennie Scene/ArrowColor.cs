using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowColor : MonoBehaviour
{
    public Material material;
    public float intensityGlow = 3.0f;
    private float intensityValue = 1.0f;
    public float fadeSpeed = 5f;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        intensityValue = 0.0f;
    }

    // Trigger sets the Intensity as defined
    public void PerformAction()

    {
        intensityValue = intensityGlow;
        material.SetFloat("_Intensity", intensityValue);

    }
    // Reduces the Intensity over time
    private void Update()
    {

        if (intensityValue > 0)
        {
            intensityValue = Mathf.Clamp(intensityValue - fadeSpeed * Time.deltaTime, 0, 10);
            material.SetFloat("_Intensity", intensityValue);
        }

    }

}
