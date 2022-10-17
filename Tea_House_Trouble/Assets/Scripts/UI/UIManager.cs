using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


public class UIManager : MonoBehaviour
{ 
    private const string lostText = "You lost.";
    private const string replayText = "PLAY AGAIN";
    private const string winText = "You won.";
    private const string pauseText = "Game Paused";
    private const string continueText = "CONTINUE GAME";

    private VisualElement root;
    private Button playButton, mainMenuButton, exitButton, controlsCloseButton;
    private Label messageLabel;
    private Action onContinue;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        playButton = root.Q<Button>("PlayButton");
        mainMenuButton = root.Q<Button>("MainMenuButton");
        exitButton = root.Q<Button>("ExitButton");
        messageLabel = root.Q<Label>("MessageText");

        playButton.clicked += onContinue; 
        exitButton.clicked += Application.Quit;
        mainMenuButton.clicked += OpenMainMenu;

        //PlayerController.OnPlayerDeath -= OpenLooseScreen;
        //PlayerController.OnPlayerDeath += OpenLooseScreen;

        //WinObject.Win -= OpenWinScreen;
        //WinObject.Win += OpenWinScreen;

        root.SetActive(false);
    }

    private void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ConfigureWindow(pauseText, continueText, ResumeGame);
        }
    }

    private void ConfigureWindow(string messageText, string buttonText, Action continueFunction)
    {
        Time.timeScale = 0f;
        messageLabel.text = messageText;
        playButton.text = buttonText;
        onContinue = continueFunction;
        root.SetActive(true);
    }

    private void OpenMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void OpenLooseScreen() => ConfigureWindow(lostText, replayText, RestartGame); //Expression-Body-Syntax, für Funktionen die eine Anweisung lang sind

    private void OpenWinScreen() => ConfigureWindow(winText, replayText, RestartGame);

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        root.SetActive(false);
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }
}
