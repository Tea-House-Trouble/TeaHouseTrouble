using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Button Button = .GetComponent<Button>();
//Button.onClick.AddListener(TaskOnClick);

public class UIOptions : MonoBehaviour
{
    public Button Control, Audio, Visual;
    public GameObject OptionsPanel;

    public GameObject ControlPanel;

    public GameObject AudioPanel;
    public Toggle ThemeOne,ThemeTwo;
    public Slider MusicSlider, VFXSlider, TotalSlider;
    
    public GameObject VisualPanel;
    public Toggle Fullscreen, Windowed;
    public Slider BrightnessSlider, ContrastSlider, DetailsSlider;


    /*private float musicVolume, vfxVolume, totalVolume;

    private float brightness, contrast, details;*/

    private void Activate(GameObject SetMenu) { SetMenu.SetActive(true); }
    private void Deactivate(GameObject SetMenu) { SetMenu.SetActive(false); }

    private void AllInactive() { 
        Deactivate(ControlPanel);
        Deactivate(AudioPanel);
        Deactivate(VisualPanel);
    }

    private void DisplayThis(GameObject ThisMenu) {
        AllInactive();
        Activate(ThisMenu);
    }

    private void ValueUpdate(float sliderValue, float setValue) {  setValue = sliderValue;    }

    private void ToggleSwitch(bool valueChanged, Toggle valueUpdate) { 
        if (valueChanged == true) { valueUpdate.isOn = false; } 
        else if(valueChanged== false) { valueUpdate.isOn = true; }
    
    }

    private void Start() {
        Button controlButton = Control.GetComponent<Button>();
        controlButton.onClick.AddListener(TaskOnClickControl);

        Button audioButton = Audio.GetComponent<Button>();
        audioButton.onClick.AddListener(TaskOnClickAudio);

        Button visualButton = Visual.GetComponent<Button>();
        visualButton.onClick.AddListener(TaskOnClickVisual);

        AllInactive();
    }

    void TaskOnClickControl() {
        DisplayThis(ControlPanel);

    }
    void TaskOnClickAudio() {
        DisplayThis(AudioPanel);

        Toggle themeOneToggle = ThemeOne.GetComponent<Toggle>();
        themeOneToggle.onValueChanged.AddListener(ThemeOneChange);

        Toggle themeTwoToggle = ThemeTwo.GetComponent<Toggle>();
        themeTwoToggle.onValueChanged.AddListener(ThemeTwoChange);

        /*Slider musicSlider = MusicSlider.GetComponent<Slider>();
        musicSlider.onValueChanged.AddListener(MusicValueUpdate);

        Slider vfxSlider = VFXSlider.GetComponent<Slider>();
        vfxSlider.onValueChanged.AddListener(VFXValueUpdate);

        Slider totalSlider = TotalSlider.GetComponent<Slider>();
        totalSlider.onValueChanged.AddListener(TotalValueUpdate);*/
    }
    void TaskOnClickVisual() {
        DisplayThis(VisualPanel);

        Toggle fullscreenToggle = Fullscreen.GetComponent<Toggle>();
        fullscreenToggle.onValueChanged.AddListener(FullscreenChange);

        Toggle windowedToggle = Windowed.GetComponent<Toggle>();
        windowedToggle.onValueChanged.AddListener(WindowedChange);

        /*Slider brightnessSlider = BrightnessSlider.GetComponent<Slider>();
        brightnessSlider.onValueChanged.AddListener(BrightnessValueUpdate);

        Slider contrastSlider = ContrastSlider.GetComponent<Slider>();
        contrastSlider.onValueChanged.AddListener(ContrastValueUpdate);

        Slider detailsSlider = DetailsSlider.GetComponent<Slider>();
        detailsSlider.onValueChanged.AddListener(DetailsValueUpdate);*/

    }

        /*void MusicValueUpdate(float sliderValue) { ValueUpdate(sliderValue, musicVolume); }
        void VFXValueUpdate(float sliderValue) { ValueUpdate(sliderValue, vfxVolume); }
        void TotalValueUpdate(float sliderValue) { ValueUpdate(sliderValue, totalVolume); }
        void BrightnessValueUpdate(float sliderValue) { ValueUpdate(sliderValue, brightness); }
        void ContrastValueUpdate(float sliderValue) { ValueUpdate(sliderValue, contrast); }
        void DetailsValueUpdate(float sliderValue) { ValueUpdate(sliderValue, details); }*/

        void ThemeOneChange(bool themeOne) { ToggleSwitch(themeOne, ThemeTwo); }
        void ThemeTwoChange(bool themeTwo) { ToggleSwitch(themeTwo, ThemeOne); }
        void FullscreenChange(bool fullscreen) { ToggleSwitch(fullscreen, Windowed); }
        void WindowedChange(bool windowed) { ToggleSwitch(windowed, Fullscreen); }
}
