using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VisualSettings : MonoBehaviour
{
    public Slider brightnessSlider;// contrastSlider, detailsSlider;

    public GameObject globalVolume;
    private Volume volume;

    private void Awake() {
        volume = globalVolume.GetComponent<Volume>();
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
        //contrastSlider.onValueChanged.AddListener(SetContrast);
        //detailsSlider.onValueChanged.AddListener(SetDetails);
    }

    private void Start() {
        brightnessSlider.value = PlayerPrefs.GetFloat(VisualManager.brightnessValue, 0.5f);
        //contrastSlider.value = PlayerPrefs.GetFloat(VisualManager.contrastValue, 0.5f);
        //detailsSlider.value = PlayerPrefs.GetFloat(VisualManager.detailsValue, 0.5f);
    }
    private void OnDisable() {
        PlayerPrefs.SetFloat(VisualManager.brightnessValue, brightnessSlider.value);
        //PlayerPrefs.SetFloat(VisualManager.musicKey, contrastSlider.value);
        //PlayerPrefs.SetFloat(VisualManager.sfxKey, detailsSlider.value);
    }
    public void SetBrightness(float value) { volume.weight = value; }
    //private void SetContrast(float value) { }
    //private void SetDetails(float value) { }

}
