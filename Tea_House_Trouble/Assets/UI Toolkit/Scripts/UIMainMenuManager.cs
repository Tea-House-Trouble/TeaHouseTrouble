using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/* Breakdown
   Teapot / StartLevel Menu 
        -> ("Play?") 
        -> Choose Controls (DanceMat, Keyboard, Controler, Touch)
        -> Choose Difficulty (Easy, Medium, Hard, Ultra)

    MenuCard / Match History / Highscores
        ->List (Place, Name, Score, Time, Accuracy , Difficulty)

    Instrument / Options Menu
        -> Choose (Gameplay / Controls , Audio, Visual)
            ->Gameplay /Controls (Current Controls, Rebind)
            ->Audio (Music Volume(Slider), Musin in MainMenu(Slider), VFX Volume(Slider), Total Volume(Slider))
            ->Visual (Brightness(Slider), Windowed / Fullscreen(Toggle))

    Door / Leave Game:
        ->Exit? (Stay, Leave)

*/

public class UIMainMenuManager : MonoBehaviour
{
    private GameObject _exitMenu, _controlPanel, _audioPanel, _visualPanel, _creditsPanel, _resetPanel, _passwordPanel, _resetText, _passwordRequest; // _startGamePanel, _chooseControlPanel, _chooseDifficultyPanel;

    private Button _playBtn, _notYetBtn, _stayBtn, _leaveBtn, _controlBtn, _audioBtn, _visualBtn,_resetBtn, _resetOptionsBtn, _resetHighscoresBtn, _enterPasswordBtn; // _dancematBtn, _keyboardBtn, controlerBtn, _touchBtn
    private Toggle _themeOne, _themeTwo; // _fullScreen, _windowed;
    private Slider _masterSlider, _musicSlider, _sfxSlider, _brightnessSlider, _contrastSlider; // _detailsSlider
    private TMP_InputField _passwordInput;
    private string _password = "TeamTanuki";
    //private int _choosenControls, _choosenDifficulty;

    public CameraTransitionManager _cameraTransitionManager;
    private AudioSettings _audioSettings;
    private VisualSettings _visualSettings;
    private HighscoreManager _highscoreManager;
    private HighscoreTable _highscoreTable;

    private void Awake() {
        AudioListener.pause = false;
        _exitMenu = GameObject.Find("ExitPanel");
        _controlPanel = GameObject.Find("ControlPanel");
        _audioPanel = GameObject.Find("AudioPanel");
        _visualPanel = GameObject.Find("VisualPanel");
        _creditsPanel = GameObject.Find("Credits");
        _resetPanel = GameObject.Find("ResetPanel");
        _passwordPanel = GameObject.Find("PasswordPanel");
        _passwordRequest = GameObject.Find("PasswordRequest_InputField");
        _resetText = GameObject.Find("ResetText") ;

        _playBtn = GameObject.Find("Play").GetComponent<Button>();
        _notYetBtn = GameObject.Find("NotYet").GetComponent<Button>();
        _stayBtn = GameObject.Find("Stay").GetComponent<Button>();
        _leaveBtn = GameObject.Find("Leave").GetComponent<Button>();
        _controlBtn = GameObject.Find("ControlButton").GetComponent<Button>() ;
        _audioBtn = GameObject.Find("AudioButton").GetComponent<Button>() ;
        _visualBtn = GameObject.Find("VisualButton").GetComponent<Button>();
        _resetBtn = GameObject.Find("ResetBtn").GetComponent<Button>();
        _resetOptionsBtn = GameObject.Find("ResetOptions").GetComponent<Button>();
        _resetHighscoresBtn = GameObject.Find("ResetHighscores").GetComponent<Button>();
        _enterPasswordBtn = GameObject.Find("EnterPassword").GetComponent<Button>();

        _masterSlider = GameObject.Find("MasterSlider").GetComponent<Slider>();
        _musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        _sfxSlider = GameObject.Find("SFXSlider").GetComponent<Slider>();
        _brightnessSlider = GameObject.Find("BrightnessSlider").GetComponent<Slider>();
        _contrastSlider = GameObject.Find("ContrastSlider").GetComponent<Slider>();

        _themeOne = GameObject.Find("Theme 1").GetComponent<Toggle>();
        _themeTwo = GameObject.Find("Theme 2").GetComponent<Toggle>();

        _passwordInput = _passwordRequest.GetComponent<TMP_InputField>();
        _passwordInput.characterLimit= 10;
        
        _cameraTransitionManager = GameObject.Find("Main_Camera").GetComponent<CameraTransitionManager>();
        _audioSettings = GetComponent<AudioSettings>();
        _visualSettings = GetComponent<VisualSettings>();
        _highscoreManager = GameObject.Find("HighscoreManager").GetComponent<HighscoreManager>();
        _highscoreTable = GameObject.Find("HighScoreDisplay").GetComponent<HighscoreTable>();
    }

        // Control & Difficulty Pick
        /*
        _dancematBtn = GameObject.Find("DanceMat").GetComponent<Button>();
        _keyboardBtn = GameObject.Find("Keyboard").GetComponent<Button>();
        _controlerBtn = GameObject.Find("Controler").GetComponent<Button>();
        _touchBtn = GameObject.Find("Touch").GetComponent<Button>();

        _easyBtn =  GameObject.Find("Easy").GetComponent<Button>();
        _mediumBtn =  GameObject.Find("Medium").GetComponent<Button>();
        _hardBtn = GameObject.Find("Hard").GetComponent<Button>();
        _ultraBtn = GameObject.Find("Ultra").GetComponent<Button>();

        _fullScreen = GameObject.Find("Fullscreen").GetComponent<Toggle>();
        _windowed = GameObject.Find("Windowed").GetComponent<Toggle>();
        */

    private void Start() { 
        Deactivate(_exitMenu);
        Deactivate(_audioPanel);
        Deactivate(_visualPanel);
        ResetResetPanel();
        Deactivate(_resetText);
        Deactivate(_resetPanel);

        _playBtn.onClick.AddListener(() =>TaskOnClickPlay());
        _notYetBtn.onClick.AddListener(() => _cameraTransitionManager.BackToBase()) ;
        _stayBtn.onClick.AddListener(() => _cameraTransitionManager.BackToBase());
        _leaveBtn.onClick.AddListener(() => TaskOnClickLeave());
        _controlBtn.onClick.AddListener(() => OptionsDisplayThis(_controlPanel));
        _audioBtn.onClick.AddListener(() => OptionsDisplayThis(_audioPanel));
        _visualBtn.onClick.AddListener( () => OptionsDisplayThis(_visualPanel));
        _resetBtn.onClick.AddListener(() => OptionsDisplayThis(_resetPanel));
        _resetOptionsBtn.onClick.AddListener(() => OnResetOptions());
        _resetHighscoresBtn.onClick.AddListener(() => Activate(_passwordPanel));
        _enterPasswordBtn.onClick.AddListener(() => OnResetHighscores());

        _themeOne.onValueChanged.AddListener(ThemeOneChange);
        _themeTwo.onValueChanged.AddListener(ThemeTwoChange);
    }

    // Control & Difficulty Pick
    /*
    _dancematBtn.onClick.AddListener(() => OptionsDisplayThis(_visualPanel));
    _keyboardBtn.onClick.AddListener(() => OptionsDisplayThis(_visualPanel));
    _controlerBtn.onClick.AddListener(() => OptionsDisplayThis(_visualPanel));
    _touchBtn.onClick.AddListener(() => OptionsDisplayThis(_visualPanel));

    _easyBtn.onClick.AddListener (() => );
    _mediumBtn.onClick.AddListener (() => ); 
    _hardBtn.onClick.AddListener (() => );
    _ultraBtn.onClick.AddListener (() => );

     _fullscreen.onValueChanged.AddListener(FullscreenChange);
    _windowed.onValueChanged.AddListener(WindowedChange);
    */

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            //Debug.Log("CLICKED_IN_MAINMENU");
            _cameraTransitionManager.CheckHit(); 
        }
        if (Input.GetMouseButtonDown(1)) { CameraBackToBase(); }
        if (Input.GetKeyDown(KeyCode.Escape)) { CameraBackToBase(); }
    }
    private void Activate(GameObject SetMenu) { SetMenu.SetActive(true); }
    private void Deactivate(GameObject SetMenu) { 
        SetMenu.SetActive(false);
        //Debug.Log(SetMenu);
    }

    private void CameraBackToBase() {
        _passwordInput.text = null;
        Deactivate(_resetText);
        _cameraTransitionManager.BackToBase();
        Deactivate(_exitMenu);
    }

    private void OnTriggerExit(Collider other) {  if(other.name=="BaseCam") { Deactivate(_creditsPanel); }    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("VirtualCam")) {
            switch (other.name) {
                case "BaseCam":
                    Deactivate(_exitMenu);
                    Activate(_creditsPanel);
                    break;
                case "DoorExitCam":
                    Activate(_exitMenu);
                    break;
                default:
                    break;
            }
        }
    }

    void TaskOnClickPlay() { SceneManager.LoadScene("NewGameScene"); }
    void TaskOnClickLeave() { Application.Quit();}

    //ChooseControls & Difficulty
    /*
    void TaskOnClickStart(){ Activate(_chooseControlPanel);    }

    void TaskOnClickControls(int pickedControls) {
      _choosenControls = pickedControls;
      Deactivate(_chooseControlPanel);
      Activate(_chooseDifficultyPanel);
    }

    void TaskOnClickDifficulty(int pickedDifficulty) {
      _choosenDifficulty = pickedDifficulty;
      Deactivate(_chooseDifficultyPanel);
      Activate(_startGamePanel);
    }     
     */

    private void OptionsDisplayThis(GameObject ThisMenu) {
        Deactivate(_controlPanel);
        Deactivate(_audioPanel);
        Deactivate(_visualPanel);
        Deactivate(_resetPanel);
        Deactivate(_resetText);
        Activate(ThisMenu);
    }

    private void ToggleSwitch(bool valueChanged, Toggle valueUpdate) {
        if (valueChanged == true) { valueUpdate.isOn = false; }
        else if (valueChanged == false) { valueUpdate.isOn = true; }
    }

    void ThemeOneChange(bool themeOne) { ToggleSwitch(themeOne, _themeTwo); }
    void ThemeTwoChange(bool themeTwo) { ToggleSwitch(themeTwo, _themeOne); }
    //void FullscreenChange(bool fullscreen) { ToggleSwitch(fullscreen, Windowed); }
    //void WindowedChange(bool windowed) { ToggleSwitch(windowed, Fullscreen); }

    void ResetResetPanel() {
        //_passwordInput.placeholder.GetComponent<Text>().text = "Enter password";
        Deactivate(_resetText);
        Deactivate(_passwordPanel);
    }

    void OnResetOptions() { 
        _audioSettings.ResetAudioSettings(0.5f, 0.5f, 0.5f);
        _visualSettings.ResetVisualSettings(1.2f, -6.0f);
        _resetText.GetComponent<TMP_Text>().text = "Options reset!";
        Activate(_resetText);
    }

    void OnResetHighscores() {
        if(_passwordInput.text == _password) {
            Deactivate(_resetText);
            Deactivate(_passwordPanel);
            _highscoreManager.ResetHighscoreList();
            _highscoreTable.ResetTable();
            _highscoreTable.SetupTable();
            _resetText.GetComponent<TMP_Text>().text = "Highscores reset!";
            Activate(_resetText);
        }

        else {
            _passwordInput.placeholder.GetComponent<TMP_Text>().text = null;
            _passwordInput.text = "Not the password"; 
        }
    }
}