using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private UIDocument doc;
    private Button continueButton;
    private Button exitButton;

    private void Awake()
    {
        doc = GetComponent<UIDocument>();
        continueButton = doc.rootVisualElement.Q<Button>("ContinueButton");
        exitButton = doc.rootVisualElement.Q<Button>("ExitButton");

        continueButton.clicked += ContinueButtonOnClicked;
        exitButton.clicked += ExitButtonOnClicked;

        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    private void ContinueButtonOnClicked()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    private void ExitButtonOnClicked()
    {
        Application.Quit();
    }
}
