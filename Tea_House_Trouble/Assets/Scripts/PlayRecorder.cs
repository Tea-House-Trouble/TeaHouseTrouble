using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class PlayRecorder : MonoBehaviour
{

    string fileFolderPath => Application.streamingAssetsPath + "/PlayRecordings/";

    string fileName = "recording_{0}.csv";

    [SerializeField] List<CSVReader.Spawn> pressedButtons = new List<CSVReader.Spawn>();

    float elapsedTime = 0;

    float lastPressedTime;
    RhythmManager.NoteID lastPressedNote;

    RhythmManager rhythmManager;

    private void OnEnable()
    {
        RhythmManager.ButtonPressed += RecordButtonPressed;
    }

    private void OnDisable()
    {
        RhythmManager.ButtonPressed -= RecordButtonPressed;
    }

    private void OnDestroy()
    {
        WriteToCSV();    
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    private void RecordButtonPressed(RhythmManager.NoteID note)
    {
        CSVReader.Spawn noteData = new CSVReader.Spawn();
        noteData.Note = note;

        int roundedSeconds = Mathf.RoundToInt(elapsedTime);
        noteData.minute = roundedSeconds / 60; // integer division intended
        noteData.second = roundedSeconds - 60 * noteData.minute;

        noteData.enemyLenght = 1;

        pressedButtons.Add(noteData);
        Debug.Log($"Pressed Button {noteData.ToString()}.", this);
    }

    private void WriteToCSV()
    {
        string csvText = CSVReader.expectedHeader + "\n";

        foreach (var note in pressedButtons)
        {
            string[] line = new string[CSVReader.columnAmount];

            line[CSVReader.columnLenght] = note.enemyLenght.ToString();
            line[CSVReader.columnNoteID] = note.Note.ToString();
            line[CSVReader.columnTime] = note.minute + ":" + note.second;

            for (int i = 0; i < line.Length; i++)
            {
                csvText += line[i] + (i == line.Length - 1 ? "\n" : ",");
            }
        }

        Debug.Log(csvText);

        string playerRecordingName = string.Format(fileName, DateTime.Now.ToString("yyyy-MM-dd_HHmmss"));

        string path = Path.Combine(fileFolderPath, playerRecordingName);

        if (!Directory.Exists(Path.GetDirectoryName(path)))
            Directory.CreateDirectory(Path.GetDirectoryName(path));

        File.WriteAllText(path, csvText, Encoding.UTF8);
    }
}
