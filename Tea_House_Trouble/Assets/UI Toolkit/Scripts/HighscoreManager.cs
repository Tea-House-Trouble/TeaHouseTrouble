using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abbadoned
/*
 {
    public List<Scores> highScores, tempScores;
    public List<int> highIndexi, tempIndexi;
    public int rankIndex, currentPoints, currentIndex, currentHigh;
    public Scores _newScore;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        _newScore = new Scores();
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
        _newScore.Points = compareScore.Points;
        _newScore.Rank = compareScore.Rank;
        _newScore.Chain = compareScore.Chain;
        _newScore.Miss = compareScore.Miss ;
        _newScore.Bad = compareScore.Bad;
        _newScore.Good = compareScore.Good;
        _newScore.Perfect = compareScore.Perfect;
        CompareScore(_newScore);
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
        _newScore.Name = name;
        tempScores.Add(_newScore);
        tempIndexi.Add(currentIndex);
        for (int i = currentIndex; i < highScores.Count; ++i) {
            tempScores.Add(highScores[i]);
            tempIndexi.Add(i+1);
        }
        ClearList(highScores, highIndexi);
        highScores = tempScores;
        highIndexi = tempIndexi;

        _newScore = new Scores();
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
    public List<Scores> highScores, tempScores;
    public int currentHigh, currentIndex;
    private Scores _newScore;
    public Scores _currentScore = null;

    public void Awake() {
        _currentScore.Name = "EMPTY";

        DontDestroyOnLoad(gameObject);
        if (instanceHighscoreManager == null) {
            instanceHighscoreManager = this;
        }
        else { Destroy(gameObject); }

        _newScore = new Scores();
        LoadScores();
    }

    public void CompareScore(Scores compareScore) {
        _newScore = compareScore;
        tempScores.Clear();
        Debug.Log(highScores.Count);
        if (compareScore.Points > currentHigh) {  currentHigh = compareScore.Points;  }
        else {
            for (int i = 0; (i < highScores.Count) && (highScores[i].Points > compareScore.Points); ++i) {
                tempScores.Add(highScores[i]);
                currentIndex = i;
            }
        }
    }

    public void AddScore(string name) {
        _newScore.Name = name;
        _newScore.Accuracy = _newScore.GetAccuracy();
        _currentScore = _newScore;
        tempScores.Add(_newScore);
        for (int i = currentIndex; i < highScores.Count; ++i) {
            tempScores.Add(highScores[i]);
        }
        highScores.Clear();
        highScores = tempScores;

        Highscores highscores = new Highscores();
        highscores.highscoreTestEntrys = highScores;

        SafeScores(highscores);
    }

    private void LoadScores() {

        AddDummyList();
        Highscores Temp = new Highscores();
        Temp.highscoreTestEntrys = highScores;
        SafeScores(Temp);
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));


        highScores.Clear();
        highScores = highscores.highscoreTestEntrys;
        if (highScores.Count == 0) { AddDummyList(); }

        currentHigh = highScores[0].Points;
    }

    private void SafeScores(Highscores highscores) {
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
    }

    public void ResetHighscoreList() {

        highScores.Clear();
        //PlayerPrefs.DeleteAll(); ;
        Highscores resetScores = new Highscores();
        resetScores.highscoreTestEntrys = highScores;
        SafeScores(resetScores);
        LoadScores();
    }

    private void AddDummyList() {
        List<Scores> DummyEntries = new List<Scores>() {
        new Scores { Name = "Teapot", Rank = "S", Points =4773 , Chain =27 , Miss =0 , Bad =Random.Range(0,50) , Good =0, Perfect =888, Accuracy = 81},
        new Scores { Name = "JackBlack", Rank = "A", Points =666 , Chain =13 , Miss =0 , Bad =4, Good =8, Perfect =1, Accuracy = 69},
        new Scores { Name = "TinyNuki", Rank = "C", Points =100 , Chain =7 , Miss =13 , Bad =2 , Good =5, Perfect =12, Accuracy = 14},
        };
        highScores = DummyEntries;
    }
}