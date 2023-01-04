using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Note : MonoBehaviour
{
    public RhythmManager.NoteID MyNoteID;

    public void Destroy()
    {
       Destroy(gameObject);
    }
    
}