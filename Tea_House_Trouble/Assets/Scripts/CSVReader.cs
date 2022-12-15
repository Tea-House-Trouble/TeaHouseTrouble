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
        public string time;
        public int enemyLenght;
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
        string[] data = textAssetData.text.Split(new String[] { ";", "\n" }, StringSplitOptions.None);

        int tableSize = data.Length / columnAmount - 1;
        mySpawnList.spawn = new Spawn[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            mySpawnList.spawn[i] = new Spawn();
            mySpawnList.spawn[i].time = data[columnAmount * (i + 1)];
            
            string timeString = data[columnAmount * (i + 1)];

            string[] timeElements = timeString.Split(':');

            int minute = int.Parse(timeElements[0]);
            int second = int.Parse(timeElements[1]);

            //Debug.Log(data[columnAmount * (i + 1)]);
            mySpawnList.spawn[i].enemyLenght = int.Parse(data[columnAmount * (i + 1) + 1].Trim());
            if(int.TryParse(data[columnAmount * (i + 1) + 1].Trim(), out int taktlenght))
            {
                mySpawnList.spawn[i].enemyLenght = taktlenght;
            }
            //else
            //{
            //    Debug.Log(data[columnAmount * (i + 1) + 1].Trim());
            //}
        }
    }
}
