using System.Collections.Generic;
using System;

[System.Serializable]
public class Scores {
    //falls es nicht funktioniert [Serializefield] vor public Variablen
    [Serializefield] public string Name, Rank;
    [Serializefield] public int Points, Chain, Miss, Bad, Good, Perfect, Accuracy;

    public int GetAccuracy() {
        int notes = Miss + Bad + Good + Perfect;
        if (notes == 0) { Accuracy = 0; }
        else { 
            Accuracy = Convert.ToInt32(((Miss * 0) + (Bad * 0.3) + (Good * 0.5) + (Perfect * 1)/notes)*100); 
        }
        return Accuracy;
    }
}