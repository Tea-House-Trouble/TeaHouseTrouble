using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] GameObject ReallyRestartOrMainMenuPanel;
    [SerializeField] GameObject PefectHits;
    [SerializeField] GameObject GoodHits;
    [SerializeField] GameObject BadHits;
    [SerializeField] GameObject MissHits;

    [SerializeField] AudioSource ButtonSound;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartButton()
    {
        SceneManager.LoadScene("NewGameScene");
        //ReallyRestartOrMainMenuPanel.SetActive(true);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");

        Time.timeScale = 1f;

        Debug.Log("Loading Menu");
    }
}
