using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] CSVReader csvReader;
    [SerializeField] Transform noteParent;

    [Space]
    [SerializeField] Note shortEnemyPrefabA;
    [SerializeField] Note shortEnemyPrefabW;
    [SerializeField] Note shortEnemyPrefabS;
    [SerializeField] Note shortEnemyPrefabD;
    //[SerializeField] Note longEnemyPrefab2Seconds;
    //[SerializeField] Note longEnemyPrefab4Seconds;

    [Space]
    [SerializeField] Transform spawnPointA;
    [SerializeField] Transform spawnPointS;
    [SerializeField] Transform spawnPointW;
    [SerializeField] Transform spawnPointD;


    int noteCounter = 0;

    float elapsedTime = 0;

    /// <summary>
    /// time when next note will spawn in seconds 
    /// </summary>
    float spawnTime;
    CSVReader.Spawn nextSpawnData;

    private void Awake()
    {
        //csvReader.ReadCSV();
        PrepareNextNote();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= spawnTime)
        {
            do
            {
                SpawnNextNote();
                PrepareNextNote();
            }
            while (elapsedTime >= spawnTime);
        }
    }

    private void PrepareNextNote()
    {
        if (noteCounter >= csvReader.spawnList.Length)
        {
            spawnTime = elapsedTime + 60f;
            return;
        }

        nextSpawnData = csvReader.spawnList[noteCounter];
        spawnTime = nextSpawnData.minute * 60f + nextSpawnData.second + nextSpawnData.miliSecond / 1000f;
        Debug.Log(spawnTime + "NextSpawnTime");
        noteCounter++;
    }

    private void SpawnNextNote()
    {
        if (nextSpawnData.Note == RhythmManager.NoteID.A)
        {
            Note newNoteA = Instantiate(shortEnemyPrefabA, GetSpawnPoint(nextSpawnData.Note).position, Quaternion.identity);
            newNoteA.transform.SetParent(noteParent, worldPositionStays: true);

            newNoteA.gameObject.name = nextSpawnData.ToString();
            newNoteA.MyNoteID = nextSpawnData.Note;
            
        }

        if (nextSpawnData.Note == RhythmManager.NoteID.W)
        {
            Note newNoteW = Instantiate(shortEnemyPrefabW, GetSpawnPoint(nextSpawnData.Note).position, Quaternion.identity);
            newNoteW.transform.SetParent(noteParent, worldPositionStays: true);

            newNoteW.gameObject.name = nextSpawnData.ToString();
            newNoteW.MyNoteID = nextSpawnData.Note;
            
        }

        if (nextSpawnData.Note == RhythmManager.NoteID.S)
        {
            Note newNoteS = Instantiate(shortEnemyPrefabS, GetSpawnPoint(nextSpawnData.Note).position, Quaternion.identity);
            newNoteS.transform.SetParent(noteParent, worldPositionStays: true);

            newNoteS.gameObject.name = nextSpawnData.ToString();
            newNoteS.MyNoteID = nextSpawnData.Note;
        }

        if (nextSpawnData.Note == RhythmManager.NoteID.D)
        {
            Note newNoteD = Instantiate(shortEnemyPrefabD, GetSpawnPoint(nextSpawnData.Note).position, Quaternion.identity);
            newNoteD.transform.SetParent(noteParent, worldPositionStays: true);

            newNoteD.gameObject.name = nextSpawnData.ToString();
            newNoteD.MyNoteID = nextSpawnData.Note;
        }

        //Note newLongNote = Instantiate(longEnemyPrefab2Seconds, GetSpawnPoint(nextSpawnData.Note).position, Quaternion.identity);
        //newLongNote.transform.SetParent(noteParent, worldPositionStays: true);
    }

    private Transform GetSpawnPoint(RhythmManager.NoteID note)
    {
        switch (note)
        {
            case RhythmManager.NoteID.A:
                return spawnPointA;
            case RhythmManager.NoteID.W:
                return spawnPointW;
            case RhythmManager.NoteID.S:
                return spawnPointS;
            case RhythmManager.NoteID.D:
                return spawnPointD;

            default:
                Debug.Log("Spawnpoint Undefined for" + note);
                return transform;
        }
    }
}
