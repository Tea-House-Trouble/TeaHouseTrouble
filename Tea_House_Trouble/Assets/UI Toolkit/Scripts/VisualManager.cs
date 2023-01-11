using System.Collections.Generic;
using UnityEngine;


public class VisualManager : MonoBehaviour
{
    public static VisualManager instance;
    public VisualSettings visualSettings;

    public const string brightnessValue = "Brightness";
    public const string contrastValue = "Contrast";
    //public const string detailsValue = "Details";

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else { Destroy(gameObject); }
        LoadVisualSettings();
    }

    private void LoadVisualSettings() {
        float brightness = PlayerPrefs.GetFloat(brightnessValue, 1.2f);
        float contrast = PlayerPrefs.GetFloat(contrastValue, 0f);
        //float details = PlayerPrefs.GetFloat(detailsValue, 0.5f);

        visualSettings.brightnessSlider.value = brightness;
        visualSettings.contrastSlider.value = contrast;
    }
}
