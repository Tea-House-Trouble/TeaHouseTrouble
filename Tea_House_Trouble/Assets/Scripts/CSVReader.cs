using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVReader : MonoBehaviour
{
    public const int columnAmount = 3;

    public const string expectedHeader = "Time,EnemyLength,NoteID";
    public const int columnTime = 0;
    public const int columnLenght = 1;
    public const int columnNoteID = 2;
    
    public TextAsset textAssetData;

    [System.Serializable]
    public class Spawn
    {
        [HideInInspector] public string identifier;
        public int enemyLenght;

        public int minute;
        public int second;
        public int miliSecond;

        public RhythmManager.NoteID Note;
        public override string ToString()
        {
            return $"{minute}:{second}:{miliSecond} {Note} ({enemyLenght})";
        }
    }

    public Spawn[] spawnList;

    [ContextMenu("Read CSV")]
    public void ReadCSV()
    {
        string[] Lines = textAssetData.text.Split(new String[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        string[][] data = new string[Lines.Length][];

        spawnList = new Spawn[data.Length - 1];

        // Use = 1 ignore from your Header
        for (int i = 1; i < data.Length; i++)
        {
           // Debug.Log(Lines[i]);

            data[i] = Lines[i].Split(',');

            // Read Data in CSV (Time, EnemyLenght, NoteID)
            spawnList[i - 1] = new Spawn();

            string timeString = data[i][columnTime];

            string[] timeElements = timeString.Split(':');

            if (!int.TryParse(timeElements[0].Trim(), out spawnList[i - 1].minute))
            {
                Debug.Log("Line " + i + " Time Elements 0 : " + timeElements[0].Trim());
            }

            if (!int.TryParse(timeElements[1].Trim(), out spawnList[i - 1].second))
            {
                Debug.Log("Line " + i + " Time Elements 1 : " + timeElements[1].Trim());
            }

            if (!int.TryParse(timeElements[2].Trim(), out spawnList[i - 1].miliSecond))
            {
                Debug.Log("Line " + i + " Time Elements 2 : " + timeElements[2].Trim());
            }

            if (!int.TryParse(data[i][columnLenght].Trim(), out spawnList[i - 1].enemyLenght))
            {
                Debug.Log("Line " + i + " Enemy Lenght : " + data[i][columnLenght].Trim());
            }

            if (!Enum.TryParse(data[i][columnNoteID].Trim(), out spawnList[i - 1].Note))
            {
                Debug.Log("Line " + i + " NoteID : " + data[i][columnNoteID].Trim());
            }

            spawnList[i - 1].identifier = spawnList[i - 1].ToString();
        }
    }
}
