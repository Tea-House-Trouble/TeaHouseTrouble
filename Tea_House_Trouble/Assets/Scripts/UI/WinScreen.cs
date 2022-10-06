using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    private UIDocument doc;
    private Button playAgainButton;
    private Button exitButton; 

    private void Awake()
    {
        doc = GetComponent<UIDocument>();
        playAgainButton = doc.rootVisualElement.Q<Button>("playAgainButton");
        exitButton = doc.rootVisualElement.Q<Button>("ExitButton");

        playAgainButton.clicked += PlayAgainButtonOnClicked;
        exitButton.clicked += ExitButtonOnClicked;

        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    private void PlayAgainButtonOnClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void ExitButtonOnClicked()
    {
        Application.Quit();
    }
}
