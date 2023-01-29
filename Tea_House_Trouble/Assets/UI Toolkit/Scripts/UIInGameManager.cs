using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIInGameManager : MonoBehaviour
{
    public RhythmManager _rhythmManager;
    private HighscoreList _highscoreList;

    private bool _wasSubmitted = false;

    public GameObject PauseMenu, SummaryMenu, SubmitScore;
    public Button ContinueBtn, RetryPauseBtn, MainMenuPauseBtn, RetrySummaryBtn, MainMenuSummaryBtn, SubmitBtn, SkipBtn;

    public TMP_Text CurrScore, CurrChain, CurrMiss, CurrBad, CurrGood, CurrPerfect, CurrHighscore, ScoreSubmitted;
    public InputField NameInput;
    public int NameInputCharacterLimit = 10;
    public Image RankImage;

    public Sprite S_Rank, A_Rank, B_Rank, C_Rank, D_Rank;

    public Scores _currentScore;

    private bool SceneIsPaused = false;
    private bool summarySetupCheck = false;

    private void Awake() {
        _highscoreList = FindObjectOfType<HighscoreList>();
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
            if (summarySetupCheck) {
                Debug.Log("RestartCheck");
            }
            Time.timeScale = 0f;
            AudioListener.pause = true;
            SetUpSummary();
            SummaryMenu.SetActive(true);
        }
    }


    public void PauseGame() {
        SceneIsPaused = true;
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
    }
    
    public void OnContinue() {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        SceneIsPaused = false;
        AudioListener.pause = false;
    }

    public void OnRetry() {
        summarySetupCheck = true;
        _currentScore = new Scores();
        _wasSubmitted = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("NewGameScene");
    }

    public void OnMainMenu() {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("MainMenu");
    }

    private void SetUpSummary() {
        _currentScore = _rhythmManager.SetUpCurrentScore();
        _highscoreList.ScoreToCompare(_currentScore);
        
        CurrScore.text = _currentScore.Points.ToString();
        CurrChain.text = _currentScore.Chain.ToString();
        CurrMiss.text = _currentScore.Miss.ToString();
        CurrBad.text = _currentScore.Bad.ToString();
        CurrGood.text = _currentScore.Good.ToString();
        CurrPerfect.text = _currentScore.Perfect.ToString();
        CurrHighscore.text = _highscoreList.currentHigh.ToString();

        switch (_currentScore.Rank) {
            case "S":
                RankImage.sprite = S_Rank;
                break;

            case "A":
                RankImage.sprite = A_Rank;
                break;

            case "B":
                RankImage.sprite = B_Rank;
                break;

            case "C":
                RankImage.sprite = C_Rank;
                break;

            case "D":
                RankImage.sprite = D_Rank;
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
        _highscoreList.AddScore(_currentScore.Name);
        SubmitScore.SetActive(false);
        ScoreSubmitted.enabled = true;
        _wasSubmitted = true;
        }
    }

    public void OnSkip() { }
}
