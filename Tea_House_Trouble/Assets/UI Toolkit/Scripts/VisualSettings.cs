using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System;

public class VisualSettings : MonoBehaviour
{
    public Slider brightnessSlider, contrastSlider; //, detailsSlider;
    public GameObject globalVolume;

    private Volume _volume;
    private ColorAdjustments _colorAdjustments;

    public const string brightnessValue = "Brightness";
    public const string contrastValue = "Contrast";

    private void Awake() {
        brightnessSlider = GameObject.Find("BrightnessSlider").GetComponent<Slider>();
        contrastSlider = GameObject.Find("ContrastSlider").GetComponent<Slider>();
        if (brightnessSlider == null) { brightnessSlider = GameObject.Find("BrightnessSlider").GetComponent<Slider>(); }
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
        
        if (contrastSlider == null) { Debug.Log("NOCONTRAST"); contrastSlider = GameObject.Find("ContrastSlider").GetComponent<Slider>(); }
        contrastSlider.onValueChanged.AddListener(SetContrast);

        //if (detailsSlider == null) { detailsSlider = GameObject.Find("DetailsSlider").GetComponent<Slider>(); }
        //detailsSlider.onValueChanged.AddListener(SetDetails);

        _volume = globalVolume.GetComponent<Volume>();
        ColorAdjustments _cAtmp;
        if(!_volume.profile.TryGet<ColorAdjustments>(out _cAtmp)) {  _colorAdjustments = _cAtmp; }
    }

    private void Start() { LoadSettings(); }
    private void OnDisable() { SaveSettings(); }

    private void LoadSettings() {
        brightnessSlider.value = PlayerPrefs.GetFloat(VisualManager.brightnessValue, 1.2f);
        contrastSlider.value = PlayerPrefs.GetFloat(VisualManager.contrastValue, 0f);
        //detailsSlider.value = PlayerPrefs.GetFloat(VisualManager.detailsValue, 0.5f);
    }
    private void SaveSettings() {
        PlayerPrefs.SetFloat(VisualManager.brightnessValue, brightnessSlider.value);
        PlayerPrefs.SetFloat(VisualManager.contrastValue, contrastSlider.value);
        //PlayerPrefs.SetFloat(VisualManager.qualityValue, detailsSlider.value);
    }
    public void SetBrightness(float value) { _colorAdjustments.postExposure.value = value; }
    private void SetContrast(float value) { _colorAdjustments.contrast.value = value; }
    //private void SetDetails(float value) { QualitySettings.SetQualityLevel(Convert.ToInt32(value)); }

    public void ResetVisualSettings(float brightness, float contrast) {
        SetBrightness(brightness);
        SetContrast(contrast);
        SaveSettings();
        LoadSettings();
    }

}
