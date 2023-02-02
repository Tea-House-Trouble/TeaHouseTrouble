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

    private float _brightness, _contrast;

    public const string brightnessValue = "Brightness";
    public const string contrastValue = "Contrast";

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        if (instance == null) {
            instance = this;
        }
        else { Destroy(gameObject); }

        _volume = globalVolume.GetComponent<Volume>();
        if(_volume == null) { }

        LoadVisualSettings();
        SetBrightness(_brightness);
        SetContrast(_contrast);
    }

    private void LoadVisualSettings() {
        _brightness = PlayerPrefs.GetFloat(brightnessValue, 1.2f);
        _contrast = PlayerPrefs.GetFloat(contrastValue, 0f);       
    }

    public void SetBrightness(float value) { if (_volume.profile.TryGet(out ColorAdjustments colAdj)) { colAdj.postExposure.value = value; } }
    private void SetContrast(float value) { if (_volume.profile.TryGet(out ColorAdjustments colAdj)) { colAdj.contrast.value = value; } }
}
