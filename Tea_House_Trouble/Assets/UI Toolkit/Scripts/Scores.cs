using System.Collections.Generic;

[System.Serializable]
public class Scores {
    public string Name, Rank;
    public int Points, Chain, Miss, Bad, Good, Perfect, Accuracy;

    public int GetAccuracy() {
        int notes = Miss + Bad + Good + Perfect;
        if (notes == 0) { Accuracy = 0; }
        else { Accuracy = Points / notes; }
        return Accuracy;
    }
}

public class Highscores { public List<Scores> highscoreTestEntrys; }