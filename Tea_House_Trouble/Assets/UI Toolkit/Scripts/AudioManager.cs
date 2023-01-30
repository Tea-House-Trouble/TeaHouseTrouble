using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer mainMixer;
    public const string masterKey = "MasterVolume";
    public const string musicKey = "MusicVolume";
    public const string sfxKey = "SFXVolume";
    //public const string uiKey = "UIVolume";

    private void Awake() {
        //DontDestroyOnLoad(gameObject);
        if(instance == null) {
            instance = this;
        }
        else { Destroy(gameObject); }
        LoadVolume();
    }

    private void LoadVolume() {
        float masterVolume = PlayerPrefs.GetFloat(masterKey, 0.5f);
        float musicVolume = PlayerPrefs.GetFloat(musicKey, 0.5f);
        float sfxVolume = PlayerPrefs.GetFloat(sfxKey, 0.5f);
        //float uiolume = PlayerPrefs.GetFloat(uiKey, 0.5f);

        mainMixer.SetFloat(AudioSettings.masterMixer, Mathf.Log10(masterVolume)*20);
        mainMixer.SetFloat(AudioSettings.musicMixer, Mathf.Log10(musicVolume)*20);
        mainMixer.SetFloat(AudioSettings.sfxMixer, Mathf.Log10(sfxVolume)*20);
        //uiMixer.SetFloat(AudioSettings.masterMixer, Mathf.Log10(masterVolume)*20);
    }
}
