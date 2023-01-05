using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class UIInGameManager : MonoBehaviour
{

    private VisualElement root, pauseVisual, victoryVisual, defeatVisual, optionsContainer, buttonContainer;
    private Button continueButton, optionsButton, difficultyButton, replayButton, mainMenuButton, exitButton;
    private Label messageLabel;
    private Action onContinue;

    private void Awake() {
        root = GetComponent<UIDocument>().rootVisualElement;
        continueButton = root.Q<Button>("ContinueButton");
        optionsButton = root.Q<Button>("OptionsButton");
        difficultyButton = root.Q<Button>("DifficultyButton");
        replayButton = root.Q<Button>("RelayButton");
        mainMenuButton = root.Q<Button>("MainMenuButton");
        exitButton = root.Q<Button>("ExitButton");

        pauseVisual = root.Q<VisualElement>("PauseContainer");
        victoryVisual = root.Q<VisualElement>("VicturyContainer");
        defeatVisual = root.Q<VisualElement>("DefeatContainer");
        optionsContainer = root.Q<VisualElement>("OptionsContainer");
        buttonContainer = root.Q<VisualElement>("ButtonContainer");

        messageLabel = root.Q<Label>("MessageText");

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
        if (Keyboard.current.escapeKey.wasPressedThisFrame) {
            PauseGame();
        }
    }
    private void SetUpMenus() {
        root.SetActive(false);
        pauseVisual.SetActive(false);
        victoryVisual.SetActive(false);
        defeatVisual.SetActive(false);
        buttonContainer.SetActive(false);
}
    /*private void ActivateButtons() {
        root.Query<Button>().ForEach(Activate);
    }
    private void Activate(Button button) { button.SetActive(true); }*/

    private void PauseGame() { 
        root.SetActive(true);
        pauseVisual.SetActive(true);
        buttonContainer.SetActive(true);
    }

    private void OnOptions() {
        optionsContainer.SetActive(true);
    }

    private void OnContinue() {
        Time.timeScale = 1f;
        root.SetActive(false);
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
