using System.Collections.Generic;
using UnityEngine;


public class VisualManager : MonoBehaviour
{
    public static VisualManager instance;
    public VisualSettings visualSettings;

    public const string brightnessValue = "Brightness";
    //public const string contrastValue = "Contrast";
    //public const string detailsValue = "Details";

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else { Destroy(gameObject); }
        LoadVisualSettings();
    }

    private void LoadVisualSettings() {
        float brightness = PlayerPrefs.GetFloat(brightnessValue, 0.5f);
        //float contrast = PlayerPrefs.GetFloat(contrastValue, 0.5f);
        //float details = PlayerPrefs.GetFloat(detailsValue, 0.5f);

        visualSettings.brightnessSlider.value = brightness;
    }
}
