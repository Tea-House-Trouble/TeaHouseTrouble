using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abbadoned
/*
 {
    public List<Scores> highScores, tempScores;
    public List<int> highIndexi, tempIndexi;
    public int rankIndex, currentPoints, currentIndex, currentHigh;
    public Scores newScore;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        newScore = new Scores();
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
        newScore.Points = compareScore.Points;
        newScore.Rank = compareScore.Rank;
        newScore.Chain = compareScore.Chain;
        newScore.Miss = compareScore.Miss ;
        newScore.Bad = compareScore.Bad;
        newScore.Good = compareScore.Good;
        newScore.Perfect = compareScore.Perfect;
        CompareScore(newScore);
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
        newScore.Name = name;
        tempScores.Add(newScore);
        tempIndexi.Add(currentIndex);
        for (int i = currentIndex; i < highScores.Count; ++i) {
            tempScores.Add(highScores[i]);
            tempIndexi.Add(i+1);
        }
        ClearList(highScores, highIndexi);
        highScores = tempScores;
        highIndexi = tempIndexi;

        newScore = new Scores();
    }

}
 */
public class HighscoreList : MonoBehaviour
{
    public List<Scores> highScores, tempScores;
    public List<int> highIndexi, tempIndexi;
    public int rankIndex, currentPoints, currentIndex, currentHigh;
    public Scores newScore;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        newScore = new Scores();
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
        newScore.Points = compareScore.Points;
        newScore.Rank = compareScore.Rank;
        newScore.Chain = compareScore.Chain;
        newScore.Miss = compareScore.Miss ;
        newScore.Bad = compareScore.Bad;
        newScore.Good = compareScore.Good;
        newScore.Perfect = compareScore.Perfect;
        CompareScore(newScore);
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
        newScore.Name = name;
        tempScores.Add(newScore);
        tempIndexi.Add(currentIndex);
        for (int i = currentIndex; i < highScores.Count; ++i) {
            tempScores.Add(highScores[i]);
            tempIndexi.Add(i+1);
        }
        ClearList(highScores, highIndexi);
        highScores = tempScores;
        highIndexi = tempIndexi;

        newScore = new Scores();
    }

}