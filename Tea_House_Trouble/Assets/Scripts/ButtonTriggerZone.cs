using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonTriggerZone : MonoBehaviour
{
    List<Note> Enemys = new List<Note>();

    public PlayerControlls Controlls;
    private PlayerInput playerInput;

    public Transform LineTransform;
    /// <summary>
    /// Pr?ft ob eine Passsende Note im Trigger ist, wenn ein Button gedr?ckt wurde. Distance beschreibt wie weit die Z Werte abweichen (Forgivness)
    /// </summary>
    public bool WasNoteHit(RhythmManager.NoteID PressedNote, out float distance, out Note HitNote)
    {
        foreach (Note note in Enemys)
        {
            if (note.MyNoteID == PressedNote)
            {
                distance = Mathf.Abs(LineTransform.InverseTransformPoint(note.transform.position).z - LineTransform.localPosition.z);
                Debug.DrawLine(transform.position, note.transform.position);
                Debug.Log(transform.InverseTransformPoint(note.transform.position) + ", Distance: " + distance + transform.localPosition);
                HitNote = note;
                Enemys.Remove(note);
                //note(gameObject);
                return true;
            }
        }

        distance = -1f;
        HitNote = null;
        return false;

    }

    private void Awake()
    {
        Controlls = new PlayerControlls();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        
    }

    private void OnEnable()
    {
        Controlls.Enable();
    }

    private void OnDisable()
    {
        Controlls.Disable();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Enemys.Add(other.GetComponent<Note>());

            Debug.Log("Enter Triggerzone");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemys.Remove(other.GetComponent<Note>());
    }
}