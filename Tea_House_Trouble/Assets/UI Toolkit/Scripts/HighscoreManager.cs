using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abbadoned
/*
 {
    public List<Scores> highScores, tempScores;
    public List<int> highIndexi, tempIndexi;
    public int rankIndex, currentPoints, currentIndex, currentHigh;
    public Scores _currentScore;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        _currentScore = new Scores();
        Scores temp = new Scores();
        temp.Name = "TEST";
        temp.Rank = "D";
        temp.Points = 15;
        temp.Chain = 5;
        temp.Miss = 5;
        temp.Bad = 5;
        temp.Good = 5;
        temp.Perfect = 5;
        currentHigh = 15;
        highScores.Add(temp);
    }


    public void ScoreToCompare(Scores compareScore) {
        _currentScore.Points = compareScore.Points;
        _currentScore.Rank = compareScore.Rank;
        _currentScore.Chain = compareScore.Chain;
        _currentScore.Miss = compareScore.Miss ;
        _currentScore.Bad = compareScore.Bad;
        _currentScore.Good = compareScore.Good;
        _currentScore.Perfect = compareScore.Perfect;
        CompareScore(_currentScore);
    }

    public void ClearList(List<Scores> scoreClear, List<int> rankClear) {
        scoreClear.Clear();
        rankClear.Clear();
    }

    public void CompareScore(Scores compareScore) {
        rankIndex = 0;
        currentIndex = 0;
        ClearList(tempScores, tempIndexi);
        if(compareScore.Points >= currentHigh) {
            currentHigh = compareScore.Points;
            currentIndex = 1;
            rankIndex = 1;
        }

        else{
            for (int i = 0; (highScores[i].Points > compareScore.Points) && (i < highScores.Count); ++i) {
                tempScores.Add(highScores[i]);
                tempIndexi.Add(highIndexi[i]);
                rankIndex = highIndexi[i];
                currentIndex = i;
            }
        }
    }

    public void CancelScore() {    }

    public void AddScore(string name) {
        _currentScore.Name = name;
        tempScores.Add(_currentScore);
        tempIndexi.Add(currentIndex);
        for (int i = currentIndex; i < highScores.Count; ++i) {
            tempScores.Add(highScores[i]);
            tempIndexi.Add(i+1);
        }
        ClearList(highScores, highIndexi);
        highScores = tempScores;
        highIndexi = tempIndexi;

        _currentScore = new Scores();
    }
        List<Scores> DummyEntries = new List<Scores>() {
        new Scores { Name = "AAA", Rank = "A", Points =Random.Range(0,100000) , Chain =Random.Range(0,100) , Miss =Random.Range(0,50) , Bad =Random.Range(0,50) , Good =Random.Range(0,50), Perfect =Random.Range(0,50), Accuracy = 0},
        new Scores { Name = "BBB", Rank = "A", Points =Random.Range(0,100000) , Chain =Random.Range(0,100) , Miss =Random.Range(0,50) , Bad =Random.Range(0,50) , Good =Random.Range(0,50), Perfect =Random.Range(0,50), Accuracy = 0},
        new Scores { Name = "CCC", Rank = "A", Points =Random.Range(0,100000) , Chain =Random.Range(0,100) , Miss =Random.Range(0,50) , Bad =Random.Range(0,50) , Good =Random.Range(0,50), Perfect =Random.Range(0,50), Accuracy = 0},
        new Scores { Name = "DDD", Rank = "A", Points =Random.Range(0,100000) , Chain =Random.Range(0,100) , Miss =Random.Range(0,50) , Bad =Random.Range(0,50) , Good =Random.Range(0,50), Perfect =Random.Range(0,50), Accuracy = 0},
        new Scores { Name = "EEE", Rank = "A", Points =Random.Range(0,100000) , Chain =Random.Range(0,100) , Miss =Random.Range(0,50) , Bad =Random.Range(0,50) , Good =Random.Range(0,50), Perfect =Random.Range(0,50), Accuracy = 0},
        };
        highScores = DummyEntries;
}
 */

public class HighscoreManager : MonoBehaviour {
    public static HighscoreManager instanceHighscoreManager;
    public List<Scores> highScores; //, tempScores;
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
        new Scores { Name = "JackBlack", Rank = "A", Points =666 , Chain =13 , Miss =0 , Bad =4, Good =8, Perfect =1, Accuracy = 69},
        new Scores { Name = "TinyNuki", Rank = "C", Points =100 , Chain =7 , Miss =13 , Bad =2 , Good =5, Perfect =12, Accuracy = 14},
        new Scores { Name = "Teapot", Rank = "S", Points =4773 , Chain =27 , Miss =0 , Bad =Random.Range(0,50) , Good =0, Perfect =888, Accuracy = 81},
        };
        highScores = DummyEntries;
    }
}