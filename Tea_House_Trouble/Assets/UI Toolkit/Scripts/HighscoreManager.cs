using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreManager : MonoBehaviour {
    public static HighscoreManager instanceHighscoreManager;
    public List<Scores> highScores;
    public int currentHigh, currentIndex;
    public Scores _currentScore;
    public bool _amAwake;

    public void Awake() {
        DontDestroyOnLoad(gameObject);
        if (instanceHighscoreManager == null) {
            instanceHighscoreManager = this;
        }
        else { Destroy(gameObject); }

        if (_amAwake == false) {
            _currentScore = new Scores();
            _currentScore.Name = "EMPTY";
            _amAwake = true;
        }
        LoadScores();
    }

    public void AddScore(Scores _newScore) {
        _newScore.Accuracy = _newScore.GetAccuracy();
        _currentScore = _newScore;
        highScores.Add(_newScore);
        SortScores();
        SafeScores();
    }

    private void SortScores() {
        for (int i = 0; i< highScores.Count; i++) {
            for (int j = i+1; j< highScores.Count; j++) {
                if(highScores[j].Points > highScores[i].Points) {
                    Scores tmp = highScores[i];
                    highScores[i] = highScores[j];
                    highScores[j] = tmp;
                }
            }
        }
    }
    public void LoadScores() {

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
        highScores.Clear();
        highScores = highscores.highscoreTestEntrys;
        if (highscores == null) { AddDummyList(); }

        currentHigh = highScores[0].Points;
    }

    private void SafeScores() {
        Highscores highscores = new Highscores();
        highscores.highscoreTestEntrys = highScores;

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();

        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
        LoadScores();
    }

    public void ResetHighscoreList() {
        highScores.Clear();
        AddDummyList();
        SafeScores();
        LoadScores();
    }

    private void AddDummyList() {
        List<Scores> DummyEntries = new List<Scores>() {
        new Scores { Name = "TinyNuki", Rank = "D", Points =100 , Chain =7 , Miss =13 , Bad =2 , Good =5, Perfect =12, Accuracy = 14},
        new Scores { Name = "Teaspirit", Rank = "A", Points =4773 , Chain =27 , Miss =0 , Bad =3, Good =0, Perfect =12, Accuracy = 72},
        };
        highScores = DummyEntries;
    }
}