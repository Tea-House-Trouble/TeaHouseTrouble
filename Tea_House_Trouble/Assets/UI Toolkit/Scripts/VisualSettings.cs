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

    public const string brightnessValue = "Brightness";
    public const string contrastValue = "Contrast";

    private void Awake() {
        brightnessSlider = GameObject.Find("BrightnessSlider").GetComponent<Slider>();
        contrastSlider = GameObject.Find("ContrastSlider").GetComponent<Slider>();
        if (brightnessSlider == null) { brightnessSlider = GameObject.Find("BrightnessSlider").GetComponent<Slider>(); }
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
        
        if (contrastSlider == null) { Debug.Log("NOCONTRAST"); contrastSlider = GameObject.Find("ContrastSlider").GetComponent<Slider>(); }
        contrastSlider.onValueChanged.AddListener(SetContrast);

        _volume = globalVolume.GetComponent<Volume>();
    }

    private void Start() { LoadSettings(); }
    private void OnDisable() { SaveSettings(); }

    private void LoadSettings() {
        brightnessSlider.value = PlayerPrefs.GetFloat(VisualManager.brightnessValue, 1.2f);
        contrastSlider.value = PlayerPrefs.GetFloat(VisualManager.contrastValue, 0f);
    }

    private void SaveSettings() {
        PlayerPrefs.SetFloat(VisualManager.brightnessValue, brightnessSlider.value);
        PlayerPrefs.SetFloat(VisualManager.contrastValue, contrastSlider.value);
    }

    public void SetBrightness(float value) {   if (_volume.profile.TryGet(out ColorAdjustments colAdj)) { colAdj.postExposure.value = value; }   }

    private void SetContrast(float value) { if (_volume.profile.TryGet(out ColorAdjustments colAdj)) { colAdj.contrast.value = value; } }

    public void ResetVisualSettings(float brightness, float contrast) {
        SetBrightness(brightness);
        SetContrast(contrast);
        SaveSettings();
        LoadSettings();
    }
}