using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Notes : MonoBehaviour
{
    public RhythmManager Rhy;

    public GameObject Note;
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;

    public RhythmManager.NoteID MyNoteID;

    //private void Awake()
    //{
    //    Rhy = FindObjectOfType<RhythmManager>();
    //}

    //private void Start()
    //{
    //    InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    //}

    public void SpawnObject()
    {
        Instantiate(Note, transform.position, Quaternion.identity);
        if (stopSpawning)
        {
            CancelInvoke("SpawnObject");
        }
    }

    //private void Update()
    //{
    //    //transform.position += Vector3.left * Time.deltaTime * (Rhy.Tempo / 60);
    //    if (transform.position.x > 0)
    //    {
    //        //Time.timeScale = 0;
    //        //Rhy.Song.Pause();
    //    }
    //    if (transform.position.x < PlayerAutoRun.PlayerTransform.position.x)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}