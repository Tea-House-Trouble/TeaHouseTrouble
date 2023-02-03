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

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        MusicTimeSlider = GetComponent<Slider>();
        //MusicTimeSlider.value = 0;
        MusicTimeSlider.minValue = 0f;
        MusicTimeSlider.maxValue = 140f;
    }

    void Update()
    {
        //MusicTimeSlider.value += Time.time;
        //CheckForReload();
        //if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("NewGameScene"))
        if (SceneManager.GetSceneByName("NewGameScene").isLoaded)
        {
            FillSlider();
        }
        //else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        else if (SceneManager.GetSceneByName("MainMenu").isLoaded)
        {
            ResetTime();
        }
    }

    void FillSlider()
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
