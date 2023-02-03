using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;

    public GameObject LoadingPanel;
    public GameObject ControllImageAndIntroduction;

    private string targetScene;

    private void Awake()
    {
        LoadingPanel.SetActive(true);
        ControllImageAndIntroduction.SetActive(false);
        Time.timeScale = 0f;
    }

    public void LoadScene(string sceneName)
    {
        targetScene = sceneName;
    }

    private void Update()
    {
        StartCoroutine(LoadSceneRoutine());
    }

    private IEnumerator LoadSceneRoutine()
    {
        yield return new WaitForSecondsRealtime(3);
        StartCoroutine(ActivateImagesAndIntroductionText());
    }

    private IEnumerator ActivateImagesAndIntroductionText()
    {
        ControllImageAndIntroduction.SetActive(true);

        yield return new WaitForSecondsRealtime(5);
        LoadingPanel.SetActive(false);
        Time.timeScale = 1f;   
    }
}