using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine;

public class LooseScreen : MonoBehaviour
{
    private UIDocument doc;
    private Button tryAgainButton;
    private Button exitButton; 

    private void Awake()
    {
        doc = GetComponent<UIDocument>();
        tryAgainButton = doc.rootVisualElement.Q<Button>("TryAgainButton");
        exitButton = doc.rootVisualElement.Q<Button>("ExitButton");

        tryAgainButton.clicked += TryAgainButtonOnClicked;
        exitButton.clicked += ExitButtonOnClicked;

        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    private void TryAgainButtonOnClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void ExitButtonOnClicked()
    {
        Application.Quit();
    }
}
