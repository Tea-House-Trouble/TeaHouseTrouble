using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    public float FillTime;

    private Slider MusicTimeSlider;

    void Start()
    {
        MusicTimeSlider = GetComponent<Slider>();
    }
    //public void ResetTime()
    //{
    //    MusicTimeSlider.minValue = Time.deltaTime;
    //    MusicTimeSlider.maxValue = Time.deltaTime + FillTime;
    //}

    void Update()
    {
        MusicTimeSlider.value = Time.time;
    }
}
