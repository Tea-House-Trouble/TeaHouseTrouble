using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System;

public class VisualManager : MonoBehaviour
{
    public static VisualManager instance;
    public GameObject globalVolume;
    private Volume _volume;
    private ColorAdjustments _colorAdjustments;    

    private float _brightness, _contrast;

    public const string brightnessValue = "Brightness";
    public const string contrastValue = "Contrast";
    //public const string detailsValue = "Details";

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else { Destroy(gameObject); }

        _volume = globalVolume.GetComponent<Volume>();
        //ColorAdjustments _cAtmp;
        //if (!_volume.profile.TryGet<ColorAdjustments>(out _cAtmp)) { _colorAdjustments = _cAtmp; }
        _colorAdjustments = _volume.GetComponent<ColorAdjustments>();

        LoadVisualSettings();
        SetBrightness(_brightness);
        SetContrast(_contrast);
    }

    private void LoadVisualSettings() {
        _brightness = PlayerPrefs.GetFloat(brightnessValue, 1.2f);
        _contrast = PlayerPrefs.GetFloat(contrastValue, 0f);
        //float details = PlayerPrefs.GetFloat(detailsValue, 0.5f);        
    }

    public void SetBrightness(float value) { _colorAdjustments.postExposure.value = value; }
    private void SetContrast(float value) { _colorAdjustments.contrast.value = value; }
}
