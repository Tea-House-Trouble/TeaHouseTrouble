using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer mainMixer;
    public Slider masterSlider, musicSlider, sfxSlider;//, uiSlider;

    public const string masterMixer = "MasterVolume";
    public const string musicMixer = "MusicVolume";
    public const string sfxMixer = "SFXVolume";
    //public const string uiMixer = "UIVolume";

    private void Awake() {
        if (masterSlider == null)   { masterSlider = GameObject.Find("MasterSlider").GetComponent<Slider>(); }
        if (musicSlider == null)    { musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();   }
        if (sfxSlider == null)      { sfxSlider = GameObject.Find("SFXSlider").GetComponent<Slider>();       }

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        //uiSlider.onValueChanged.AddListener(SetUIVolume);
    }

    private void Start() {
        masterSlider.value = PlayerPrefs.GetFloat(AudioManager.masterKey, 0.5f);
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.musicKey, 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.sfxKey, 0.5f);
        //uiSlider.value = PlayerPrefs.GetFloat(AudioManager.uiKey, 0.5f);
    }
    private void OnDisable() {
        PlayerPrefs.SetFloat(AudioManager.masterKey, masterSlider.value);
        PlayerPrefs.SetFloat(AudioManager.musicKey, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.sfxKey, sfxSlider.value);
        //PlayerPrefs.SetFloat(AudioManager.uiKey, uiSlider.value);
    }
    private void SetMasterVolume(float value) {   mainMixer.SetFloat(masterMixer, Mathf.Log10(value)*20);    }
    private void SetMusicVolume(float value) {   mainMixer.SetFloat(musicMixer, Mathf.Log10(value)*20);    }
    private void SetSFXVolume(float value) {   mainMixer.SetFloat(sfxMixer, Mathf.Log10(value)*20);    }
    //private void SetUIVolume(float value) {   uiMixer.SetFloat(uiMixer, Mathf.Log10(value)*20);    }
}
