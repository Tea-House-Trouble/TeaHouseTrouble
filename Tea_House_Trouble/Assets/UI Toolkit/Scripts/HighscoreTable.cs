using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HighscoreTable : MonoBehaviour {
    public Transform highscoreContainer, scoreTemplate;
    private List<Transform> _highscoreEntrys;
    private HighscoreManager _highscoreManager;

    private Scores _currentScore, _high;
    private TMP_Text _currPlace, _currName, _currPoints, _currAccuracy, _currChain, _currRank;
    public TMP_Text highPlace, highName, highPoints, highAcc, highChain, highRank;

    private void Start() {
        _currPlace = GameObject.Find("CurrPlace").GetComponent<TMP_Text>();
        _currName = GameObject.Find("CurrName").GetComponent<TMP_Text>(); 
        _currPoints = GameObject.Find("CurrPoints").GetComponent<TMP_Text>(); 
        _currAccuracy = GameObject.Find("CurrAccuracy").GetComponent<TMP_Text>(); 
        _currChain = GameObject.Find("CurrChain").GetComponent<TMP_Text>(); 
        _currRank = GameObject.Find("CurrRank").GetComponent<TMP_Text>();
        _highscoreManager = FindObjectOfType<HighscoreManager>();
        _high = _highscoreManager.LoadScores();

        SetupTable();
        GameObject.Find("HighScoreDisplay").GetComponent<HighscoreScrolling>().SetUpScrollbar();
        SetHighscore();
        scoreTemplate.gameObject.SetActive(false);
    }

    public void SetupTable() {
        Debug.Log("SET UP SCORE TABLE");
        List<Scores> check = _highscoreManager.highScores;
        _currentScore = _highscoreManager._currentScore;
        if (_currentScore.Name == "EMPTY") {
            _currPlace.text = "-";
            _currName.text = "-";
            _currPoints.text = "-";
            _currAccuracy.text = "-";
            _currChain.text = "-";
            _currRank.text = "-";
        }
        else { 
            _currPlace.text = _highscoreManager.currentIndex.ToString();
            _currName.text = _currentScore.Name.ToString();
            _currPoints.text = _currentScore.Points.ToString();
            _currAccuracy.text = _currentScore.GetAccuracy().ToString();
            _currChain.text = _currentScore.Chain.ToString();
            _currRank.text = _currentScore.Rank.ToString();
        }

        _highscoreEntrys = new List<Transform>();
        foreach (Scores _score in check) { CreateHighscoreTable(_score, _highscoreEntrys); }

        foreach (Transform t in _highscoreEntrys)
        {
            t.gameObject.SetActive(true);
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public void ResetTable() {
            foreach (Transform child in highscoreContainer) {
                child.gameObject.SetActive(false);
            }
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

        foreach (Transform child in newScore)
        {
            child.gameObject.SetActive(true);
        }

        highscoreDisplayList.Add(newScore);
    }
    public void SetHighscore()
    {
        highPlace.text = "1";
        highName.text = _high.Name.ToString();
        highPoints.text = _high.Points.ToString();
        highAcc.text = _high.Accuracy.ToString();
        highChain.text = _high.Chain.ToString();
        highRank.text = _high.Rank.ToString();
    }
}