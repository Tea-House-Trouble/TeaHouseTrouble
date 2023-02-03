using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    public float FillTime;

    Slider MusicTimeSlider;

    bool inGameScene = false;
    private RhythmManager rhythmManager;

    void Start()
    {
        rhythmManager = FindObjectOfType<RhythmManager>();
        
        if (rhythmManager != null)
        {
            ResetTime();
        }
        else
        {
            FillSlider();
        }
            
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        MusicTimeSlider = GetComponent<Slider>();
        //MusicTimeSlider.value = 0;
        MusicTimeSlider.minValue = 0f;
        MusicTimeSlider.maxValue = 140f;
    }

    //private void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}



    void Update()
    {
            //MusicTimeSlider.value += Time.time;
            //CheckForReload();
            //if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("NewGameScene"))
            //if (SceneManager.GetSceneByName("NewGameScene").isLoaded)
        //if (SceneManager.GetActiveScene().name == "NewGameScene")
        //{
        //    FillSlider();
        //}
        //else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        //else if (SceneManager.GetSceneByName("MainMenu").isLoaded)
        //else if (SceneManager.GetActiveScene().name == "MainMenu")
        //{
        //    ResetTime();
        //}
    }

    public void FillSlider()
    {
        //MusicTimeSlider.value = Time.time;
        MusicTimeSlider.value = Mathf.Lerp(MusicTimeSlider.minValue, MusicTimeSlider.maxValue, FillTime);

        FillTime += 0.01f * Time.deltaTime;

    }

    //bool CheckForReload()
    //{
    //    //Scene = "NewGameScene";
    //    SceneManager.GetActiveScene();

    //    return true;
    //}

    public void ResetTime()
    {
        //SceneManager.LoadScene("MainMenu");
        MusicTimeSlider.value = MusicTimeSlider.minValue;
        //MusicTimeSlider.maxValue = Time.deltaTime + FillTime;
    }
}
