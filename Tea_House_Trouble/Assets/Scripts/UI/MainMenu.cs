using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private UIDocument doc;
    private Button startButton;
    private Button exitButton;

    private void Awake()
    {
        doc = GetComponent<UIDocument>();
        startButton = doc.rootVisualElement.Q<Button>("StartButton");
        exitButton = doc.rootVisualElement.Q<Button>("ExitButton");

        startButton.clicked += StartButtonOnClicked;
        exitButton.clicked += ExitButtonOnClicked;
    }

    private void StartButtonOnClicked()
    {
        SceneManager.LoadScene("SampleScene");
        Destroy(this);
        Debug.Log("Start Button clicked");
    }

    private void ExitButtonOnClicked()
    {
        Application.Quit();
    }
}
