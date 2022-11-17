using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonTriggerZone : MonoBehaviour
{
    List<Notes> Enemys = new List<Notes>();

    public PlayerControlls Controlls;
    private PlayerInput playerInput;

    public Transform LineTransform;
    /// <summary>
    /// Prüft ob eine Passsende Note im Trigger ist, wenn ein Button gedrückt wurde. Distance beschreibt wie weit die Z Werte abweichen (Forgivness)
    /// </summary>
    public bool WasNoteHit(RhythmManager.NoteID PressedNote, out float distance)
    {
        foreach (Notes note in Enemys)
        {
            if (note.MyNoteID == PressedNote)
            {
                distance = Mathf.Abs(LineTransform.InverseTransformPoint(note.transform.position).z - LineTransform.localPosition.z);
                Debug.DrawLine(transform.position, note.transform.position);
                Debug.Log(transform.InverseTransformPoint(note.transform.position) + ", Distance: " + distance + transform.localPosition);
                Enemys.Remove(note);
                note.Destroy();
                return true;
            }
        }

        distance = -1f;
        return false;
    }

    private void Awake()
    {
        Controlls = new PlayerControlls();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        //foreach (Notes note in Enemys)
        //{
        //    if (note == null) continue;

        //    float distance = Mathf.Abs(LineTransform.InverseTransformPoint(note.transform.position).z - LineTransform.localPosition.z);
        //    distance = Mathf.Round(distance * 100f) / 100f;
        //    note.DistanceText.text = distance.ToString();
        //}

        //if (transform.position.x < PlayerAutoRun.PlayerTransform.position.x)
        //{
        //    Destroy(gameObject);
        //}
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
            Enemys.Add(other.GetComponent<Notes>());

            Debug.Log("Enter Triggerzone");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemys.Remove(other.GetComponent<Notes>());
    }
}