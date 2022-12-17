using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] CSVReader csvReader;
    [SerializeField] Transform noteParent;

    [Space]
    [SerializeField] Note shortEnemyPrefab;

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
        csvReader.ReadCSV();
        PrepareNextNote();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= spawnTime)
        {
            SpawnNextNote();
            PrepareNextNote();
        }
    }

    private void PrepareNextNote()
    {
        nextSpawnData = csvReader.spawnList[noteCounter];
        spawnTime = nextSpawnData.minute * 60f + nextSpawnData.second;

        noteCounter++;
    }

    private void SpawnNextNote()
    {
        Note newNote = Instantiate(shortEnemyPrefab, GetSpawnPoint(nextSpawnData.Note).position, Quaternion.identity);
        newNote.transform.SetParent(noteParent, worldPositionStays: true);

        newNote.gameObject.name = nextSpawnData.ToString();
        newNote.MyNoteID = nextSpawnData.Note;
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
