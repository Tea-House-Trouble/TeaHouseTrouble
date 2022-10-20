using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private VisualElement root;
    private VisualElement controls;

    private Button playButton, controlsButton, exitButton, controlsCloseButton;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        controls = root.Q<VisualElement>("Controls");
        playButton = root.Q<Button>("PlayButton");
        controlsButton = root.Q<Button>("ControlsButton");
        exitButton = root.Q<Button>("ExitButton");
        controlsCloseButton = root.Q<Button>("ControlsCloseButton");

        playButton.clicked += PlayButtonOnClicked;
        controlsButton.clicked += delegate { controls.SetActive(true); };
        exitButton.clicked += ExitButtonOnClicked;
        controlsCloseButton.clicked += delegate { controls.SetActive(false); };

        controls.SetActive(false);
    }

    private void PlayButtonOnClicked()
    {
        SceneManager.LoadScene("NormansScene");
    }

    private void ExitButtonOnClicked()
    {
        Application.Quit();
    }
}
