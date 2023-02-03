using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class Scores 
{
    [SerializeField] public string Name, Rank;
    [SerializeField] public int Points, Chain, Miss, Bad, Good, Perfect, Accuracy;

    public int GetAccuracy() {
        int notes = Miss + Bad + Good + Perfect;
        if (notes == 0) { Accuracy = 0; }
        else { 
            Accuracy = Convert.ToInt32(((Miss * 0) + (Bad * 0.3) + (Good * 0.5) + (Perfect * 1)/notes)*100); 
        }
        return Accuracy;
    }
}