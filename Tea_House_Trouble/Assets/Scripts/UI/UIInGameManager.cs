using System;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInGameManager : MonoBehaviour
{
    private bool SceneIsPaused = false;
    private VisualElement root, pauseVisual, victoryVisual, defeatVisual, optionsContainer, buttonContainer;
    private Button continueButton, optionsButton, difficultyButton, replayButton, mainMenuButton, exitButton;
    //private Label messageLabel;
    //private Action onContinue;

    private void Awake() {
        root = GetComponent<UIDocument>().rootVisualElement;
        continueButton = root.Q<Button>("ContinueButton");
        optionsButton = root.Q<Button>("OptionsButton");
        difficultyButton = root.Q<Button>("DifficultyButton");
        replayButton = root.Q<Button>("ReplayButton");
        mainMenuButton = root.Q<Button>("MainMenuButton");
        exitButton = root.Q<Button>("ExitButton");

        pauseVisual = root.Q<VisualElement>("PauseContainer");
        victoryVisual = root.Q<VisualElement>("VicturyContainer");
        defeatVisual = root.Q<VisualElement>("DefeatContainer");
        optionsContainer = root.Q<VisualElement>("OptionsContainer");
        buttonContainer = root.Q<VisualElement>("ButtonContainer");

        //messageLabel = root.Q<Label>("MessageText");

        continueButton.clicked += OnContinue;
        optionsButton.clicked += OnOptions;
        difficultyButton.clicked += OnDifficulty;
        replayButton.clicked += OnReplay;
        mainMenuButton.clicked += OnOpenMainMenu;
        exitButton.clicked += Application.Quit;

        //WinObject.Win -= OpenWinScreen;
        //WinObject.Win += OpenWinScreen;

        optionsButton.SetEnabled(false);
        difficultyButton.SetEnabled(false);

        root.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {

        if (SceneIsPaused == false){
                Debug.Log("PAUSE");
                PauseGame(); }
            
        else { OnContinue(); }
        } 
    }
    private void ResetMenus() {
        root.SetActive(false);
        pauseVisual.SetActive(false);
        victoryVisual.SetActive(false);
        defeatVisual.SetActive(false);
        buttonContainer.SetActive(false);
}

    private void PauseGame() {
        pauseVisual.SetActive(true);
        buttonContainer.SetActive(true);
        Time.timeScale = 0f;
        SceneIsPaused = true;
        root.SetActive(true);
    }

    private void OnOptions() {
        optionsContainer.SetActive(true);
    }

    private void OnContinue() {
        Time.timeScale = 1f;
        root.SetActive(false);
        SceneIsPaused = false;
    }

    private void OnDifficulty() {

    }

    private void OnReplay() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("NewGameScene");
    }
    private void OnOpenMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
