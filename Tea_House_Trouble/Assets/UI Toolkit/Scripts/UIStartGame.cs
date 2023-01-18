using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIStartGame : MonoBehaviour {
    public GameObject StartPanel;// DifficultyPanel ,ControlPanel;
    public Button Play,Easy,Medium,Hard;//Ultra; ,DanceMat, Keyboard, Controller, Touch;

    //private int choosenControls = 0;
    //private int choosenDifficulty = 0;

    public void Start() {
        Button playButton = Play.GetComponent<Button>();
        playButton.onClick.AddListener(TaskOnClickPlay);
    }

    private void Activate(GameObject SetMenu) { SetMenu.SetActive(true); }
    private void Deactivate(GameObject SetMenu) { SetMenu.SetActive(false); }

    void TaskOnClickPlay() {
        SceneManager.LoadScene("NewGameScene");
        /*
        Activate(ControlPanel);

        Button danceMatButton = DanceMat.GetComponent<Button>();
        danceMatButton.onClick.AddListener(TaskOnClickDanceMat);        
        
        Button keyboardButton = Keyboard.GetComponent<Button>();
        keyboardButton.onClick.AddListener(TaskOnClickKeyboard);        
        
        Button controllerButton = Controller.GetComponent<Button>();
        controllerButton.onClick.AddListener(TaskOnClickController);        
        
        Button touchButton = Touch.GetComponent<Button>();
        touchButton.onClick.AddListener(TaskOnClickTouch);

        Deactivate(StartPanel);
    }

    void TaskOnClickDanceMat() {       ChoosenControls(1);    }
    void TaskOnClickKeyboard() { ChoosenControls(2); }
    void TaskOnClickController() { ChoosenControls(3); }
    void TaskOnClickTouch() { ChoosenControls(4); }

    void ChoosenControls(int controls) {
        choosenControls = controls;
        Activate(DifficultyPanel);
        Button easyButton = Easy.GetComponent<Button>();
        easyButton.onClick.AddListener(TaskOnClickEasy);

        Button mediumButton = Medium.GetComponent<Button>();
        mediumButton.onClick.AddListener(TaskOnClickMedium);

        Button hardButton = Hard.GetComponent<Button>();
        hardButton.onClick.AddListener(TaskOnClickHard);

        Button ultraButton = Ultra.GetComponent<Button>();
        ultraButton.onClick.AddListener(TaskOnClickUltra);

        Deactivate(ControlPanel);
    
    Activate(DifficultyPanel);
    void TaskOnClickEasy() { }
    void TaskOnClickMedium() { }
    void TaskOnClickHard() { }
    void TaskOnClickUltra() { }

    void StartGame(int difficulty) {
        choosenDifficulty = difficulty;
        SceneManager.LoadScene("NormanScene");
    }*/
    
    }
}

