using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


/*
    highscoreTestEntrys = new List<Score>() {
        new Score { Name = "AAA", Rank = "A", Points =Random.Range(0,100000) , Chain =Random.Range(0,100) , Miss =Random.Range(0,50) , Bad =Random.Range(0,50) , Good =Random.Range(0,50), Perfect =Random.Range(0,50), Accuracy = 0},
        new Score { Name = "BBB", Rank = "A", Points =Random.Range(0,100000) , Chain =Random.Range(0,100) , Miss =Random.Range(0,50) , Bad =Random.Range(0,50) , Good =Random.Range(0,50), Perfect =Random.Range(0,50), Accuracy = 0},
        new Score { Name = "CCC", Rank = "A", Points =Random.Range(0,100000) , Chain =Random.Range(0,100) , Miss =Random.Range(0,50) , Bad =Random.Range(0,50) , Good =Random.Range(0,50), Perfect =Random.Range(0,50), Accuracy = 0},
        new Score { Name = "DDD", Rank = "A", Points =Random.Range(0,100000) , Chain =Random.Range(0,100) , Miss =Random.Range(0,50) , Bad =Random.Range(0,50) , Good =Random.Range(0,50), Perfect =Random.Range(0,50), Accuracy = 0},
        new Score { Name = "EEE", Rank = "A", Points =Random.Range(0,100000) , Chain =Random.Range(0,100) , Miss =Random.Range(0,50) , Bad =Random.Range(0,50) , Good =Random.Range(0,50), Perfect =Random.Range(0,50), Accuracy = 0},
    };


    Highscores highscores = new Highscores { highscoreTestEntrys = highscoreTestEntrys };
    
    [System.Serializable]
    private class Score {
        public string Name,Rank;
        public int Points,Chain, Miss,Bad,Good,Perfect, Accuracy;
        
        public int GetAccuracy() {
            int notes = Miss + Bad + Good + Perfect;
            if(notes == 0) {   Accuracy = 0;  }
            else { Accuracy = Points / notes; }
            return Accuracy;
        }
    }

    private class Highscores {        public List<Scores> highscoreTestEntrys;   }
        //Score addTest = new Score { Name = "###", Rank = "$$$", Points = Random.Range(0, 100000), Chain = Random.Range(0, 100), Miss = Random.Range(0, 50), Bad = Random.Range(0, 50), Good = Random.Range(0, 50), Perfect = Random.Range(0, 50), Accuracy = 0 };
        //AddScoreEntry(addTest);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        List<Scores> check = highscores.highscoreTestEntrys;

        //SortScores
        for (int i = 0; i < check.Count; i++) {
            for (int j = i + 1; j < check.Count; j++) {
                if (check[j].Points > check[i].Points) {
                    Scores temp = check[i];
                    check[i] = check[j];
                    check[j] = temp;
                }
            }
        }

    private void AddScoreEntry(Scores _score) {
        //Calculate Accuracy
        _score.Accuracy = _score.GetAccuracy();

        //Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //Add new entry
        highscores.highscoreTestEntrys.Add(_score);


        //Saved updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
    }
*/

public class HighscoreTable : MonoBehaviour {
    public Transform highscoreContainer, scoreTemplate;
    private List<Transform> _highscoreEntrys;
    private HighscoreManager _highscoreManager;

    private void Start() {
        _highscoreManager = FindObjectOfType<HighscoreManager>();
        scoreTemplate.gameObject.SetActive(false);

        List<Scores> check = _highscoreManager.highScores;

        _highscoreEntrys = new List<Transform>();
        foreach (Scores _score in check) { CreateHighscoreTable(_score, _highscoreEntrys); }
    }

    private void CreateHighscoreTable(Scores scoreToAdd, List<Transform> highscoreDisplayList) {
        Transform newScore = Instantiate(scoreTemplate, highscoreContainer);
        newScore.Find("Place").GetComponent<TMP_Text>().text = (highscoreDisplayList.Count + 1).ToString();
        newScore.Find("Name").GetComponent<TMP_Text>().text = scoreToAdd.Name;
        newScore.Find("Rank").GetComponent<TMP_Text>().text = scoreToAdd.Rank;
        newScore.Find("Points").GetComponent<TMP_Text>().text = scoreToAdd.Points.ToString();
        newScore.Find("Accuracy").GetComponent<TMP_Text>().text = scoreToAdd.GetAccuracy().ToString() + "%";
        newScore.Find("Chain").GetComponent<TMP_Text>().text = scoreToAdd.Chain.ToString() + "x";

        newScore.gameObject.SetActive(true);

        highscoreDisplayList.Add(newScore);
    }
}