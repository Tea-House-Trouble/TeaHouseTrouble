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

    /// <summary>
    /// Prüft ob eine Passsende Note im Trigger ist, wenn ein Button gedrückt wurde. Distance beschreibt wie weit die Z Werte abweichen (Forgivness)
    /// </summary>
    public bool WasNoteHit(RhythmManager.NoteID PressedNote, out float distance)
    {
        foreach (Notes note in Enemys)
        {
            if (note.MyNoteID == PressedNote)
            {
                distance = Mathf.Abs(transform.InverseTransformPoint(note.transform.position).z - transform.localPosition.z);
                Debug.DrawLine(transform.position, note.transform.position);
                Debug.Log(transform.InverseTransformPoint(note.transform.position));
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