using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIInGameManager : MonoBehaviour
{
    public RhythmManager _rhythmManager;
    private HighscoreManager _highscoreManager;

    public GameObject PauseMenu, SummaryMenu, SubmitScore, TeaSpirit, TeaSpiritBG;
    public Button ContinueBtn, RetryPauseBtn, MainMenuPauseBtn, RetrySummaryBtn, MainMenuSummaryBtn, SubmitBtn, SkipBtn;

    public TMP_Text CurrScore, CurrChain, CurrMiss, CurrBad, CurrGood, CurrPerfect, CurrHighscore, ScoreSubmitted;
    public InputField NameInput;
    public int NameInputCharacterLimit = 10;
    public Image RankImage;

    public Sprite S_Rank, A_Rank, B_Rank, C_Rank, D_Rank;

    public Scores _currentScore;

    private bool _wasSubmitted = false;
    private bool SceneIsPaused = false;
    private bool summarySetupCheck = false;

    private AudioSource _audioSource, _audioSourceTusch, _audioSourceGrillen;
    public int _counter = 0;

    private Animator AnimTeaSpirit;

    [SerializeField] MyBlitFeature blit;

    private void Awake() {
       TeaSpirit = GameObject.Find("TeaSpirit_WinScreen");
       TeaSpiritBG = GameObject.Find("BG");
       AnimTeaSpirit = TeaSpirit.GetComponent<Animator>();

        _audioSource = GetComponent<AudioSource>();
       _audioSourceTusch = GameObject.Find("AudioSourceTusch").GetComponent<AudioSource>();
       _audioSourceGrillen = GameObject.Find("AudioSourceGrillen").GetComponent<AudioSource>();
       AudioListener.pause = false;
        _audioSource.ignoreListenerPause = true;
       _audioSourceTusch.ignoreListenerPause = true;
       _audioSourceGrillen.ignoreListenerPause = true;

        _highscoreManager = FindObjectOfType<HighscoreManager>();
        _rhythmManager = FindObjectOfType<RhythmManager>();

        _currentScore = new Scores();

        NameInput.characterLimit = NameInputCharacterLimit;

        Button _continue = ContinueBtn.GetComponent<Button>();
        _continue.onClick.AddListener(OnContinue);

        Button _retryP = RetryPauseBtn.GetComponent<Button>();
        _retryP.onClick.AddListener(OnRetry);

        Button _mainMenuP= MainMenuPauseBtn.GetComponent<Button>();
        _mainMenuP.onClick.AddListener(OnMainMenu);

        Button _retryS= RetrySummaryBtn.GetComponent<Button>();
        _retryS.onClick.AddListener(OnRetry);

        Button _mainMenuS= MainMenuSummaryBtn.GetComponent<Button>();
        _mainMenuS.onClick.AddListener(OnMainMenu);

        Button _submit = SubmitBtn.GetComponent<Button>();
        _submit.onClick.AddListener(OnSubmit);

        Button _skip = SkipBtn.GetComponent<Button>();
        _skip.onClick.AddListener(OnSkip);

        SubmitScore.SetActive(true);
        ScoreSubmitted.enabled = false;
        PauseMenu.SetActive(false);
        SummaryMenu.SetActive(false);
        TeaSpirit.SetActive(false);
        TeaSpiritBG.SetActive(false);
    }

    private void Start()
    {
        MusicTimer = GameObject.Find("MusicTimer");
        _gameTime = MusicTimer.GetComponent<GameTime>();
        _gameTime.ResetTime();
    }

    private void Update() {

        if (Input.GetMouseButtonDown(0)) { Debug.Log("CLICK"); }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (SceneIsPaused == false){
                    Debug.Log("PAUSE");
                    PauseGame(); }
            
            else {
                    Debug.Log("UNPAUSE");
                    OnContinue(); }
        } 
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("FINISHED GAME");
        if (other.gameObject.CompareTag("Player")) {
            _counter++;
            if(_counter ==2) {
                if (summarySetupCheck) {
                    Debug.Log("RestartCheck");
                }
                Time.timeScale = 0f;
                AudioListener.pause = true;
                SetUpSummary();
                SummaryMenu.SetActive(true);
                TeaSpiritBG.SetActive(true);
                TeaSpirit.SetActive(true);
                switch (_currentScore.Rank) {
                    case "S":
                        AnimTeaSpirit.Play("Win");
                        break;

                    case "A":
                        AnimTeaSpirit.Play("Win");
                        break;

                    case "B":
                        AnimTeaSpirit.Play("Medium");
                        break;

                    case "C":
                        AnimTeaSpirit.Play("Medium");
                        break;

                    case "D":
                        AnimTeaSpirit.Play("Loose");
                        break;
                }
            }
        }
    }


    public void PauseGame() {
        _rhythmManager._isPaused = true;
        SceneIsPaused = true;
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
        _audioSource.Play();
    }
    
    public void OnContinue() {
        _rhythmManager._isPaused = false;
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        SceneIsPaused = false;
        AudioListener.pause = false;
        _audioSource.Stop();
    }

    public void OnRetry() {
        _rhythmManager._isPaused = false;
        summarySetupCheck = true;
        _currentScore = new Scores();
        _wasSubmitted = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("NewGameScene");
        _audioSource.Stop();
    }

    public void OnMainMenu() {
        _rhythmManager._isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        _audioSource.Stop();
    }

    private void SetUpSummary() {
        blit.settings.MaterialToBlit.SetFloat("_Speed_Lines_Active", 0);
        blit.settings.MaterialToBlit.SetFloat("_Radial_Blur_Active", 0);

        _rhythmManager._isPaused = true;
        _currentScore = _rhythmManager.SetUpCurrentScore();
        _highscoreManager._currentScore = _currentScore;
        
        CurrScore.text = _currentScore.Points.ToString();
        CurrChain.text = _currentScore.Chain.ToString();
        CurrMiss.text = _currentScore.Miss.ToString();
        CurrBad.text = _currentScore.Bad.ToString();
        CurrGood.text = _currentScore.Good.ToString();
        CurrPerfect.text = _currentScore.Perfect.ToString();
        CurrHighscore.text = _highscoreManager.currentHigh.ToString();

        switch (_currentScore.Rank) {
            case "S":
                RankImage.sprite = S_Rank;
                AnimTeaSpirit.Play("Win");
                break;

            case "A":
                RankImage.sprite = A_Rank;
                AnimTeaSpirit.Play("Win");
                break;

            case "B":
                RankImage.sprite = B_Rank;
                AnimTeaSpirit.Play("Medium");
                break;

            case "C":
                RankImage.sprite = C_Rank;
                AnimTeaSpirit.Play("Medium");
                break;

            case "D":
                RankImage.sprite = D_Rank;
                AnimTeaSpirit.Play("Loose");
                break;
        }
    }

    public void ReadInput() {
        if(NameInput.text == null) { _currentScore.Name = "Anonymous";        }
        else { _currentScore.Name = NameInput.text; }
        Debug.Log(_currentScore.Name);
    }

    public void OnSubmit() {
        if(_wasSubmitted == false) {
        SubmitBtn.enabled = false;
        ReadInput();        
        _highscoreManager.AddScore(_currentScore);
        SubmitScore.SetActive(false);
        ScoreSubmitted.enabled = true;
        _wasSubmitted = true;
        }
    }
    public void OnSkip() { }
}
