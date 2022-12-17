using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVReader : MonoBehaviour
{
    const int columnAmount = 2;

    public TextAsset textAssetData;

    [System.Serializable]
    public class Spawn
    {
        [HideInInspector] public string identifier;
        public int enemyLenght;

        public int minute;
        public int second;

        public RhythmManager.NoteID Note;
        public override string ToString()
        {
            return $"{minute}:{second} {Note} ({enemyLenght})";
        }
    }

    [System.Serializable]
    public class SpawnList
    {
        public Spawn[] spawn;
    }

    public SpawnList mySpawnList = new SpawnList();

    // Start is called before the first frame update
    void Start()
    {
        ReadCSV();
    }

    [ContextMenu("Read CSV")]
    void ReadCSV()
    {
        string[] Lines = textAssetData.text.Split(new String[] { "\n" }, StringSplitOptions.None);
        string[][] data = new string[Lines.Length][];

        Debug.Log(data.Length);
        int tableSize = data.Length / columnAmount - 1;
        mySpawnList.spawn = new Spawn[tableSize];

        for (int i = 1; i < tableSize + 1; i++) // Use = 1 ignore from your Header
        {
            Debug.Log(data[columnAmount * i]);

            data[i] = Lines[i].Split(';');


            mySpawnList.spawn[i - 1] = new Spawn();

            string timeString = data[i][0];

            string[] timeElements = timeString.Split(':');

            if (!int.TryParse(timeElements[0].Trim(), out mySpawnList.spawn[i - 1].minute))
            {
                Debug.Log("Line " + i + " Time Elements 0 : " + timeElements[0].Trim());
            }

            if (!int.TryParse(timeElements[1].Trim(), out mySpawnList.spawn[i - 1].second))
            {
                Debug.Log("Line " + i + " Time Elements 1 : " + timeElements[1].Trim());
            }

            if (!int.TryParse(data[i][1].Trim(), out mySpawnList.spawn[i - 1].enemyLenght))
            {
                Debug.Log("Line " + i + " Enemy Lenght : " + data[i][1].Trim());
            }

            if (!Enum.TryParse(data[i][2].Trim(), out mySpawnList.spawn[i - 1].Note))
            {
                Debug.Log("Line " + i + " NoteID : " + data[i][2].Trim());
            }

            mySpawnList.spawn[i - 1].identifier = mySpawnList.spawn[i - 1].ToString();
        }
    }
}
