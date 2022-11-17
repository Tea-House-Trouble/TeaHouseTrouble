using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class RhythmManager : MonoBehaviour, PlayerControlls.IActionsActions
{
    public GameObject Ocha;
    public AudioSource Song;
    public GameObject Note;
    public GameObject SpawnPointA;
    public GameObject SpawnPointW;
    public GameObject SpawnPointS;
    public GameObject SpawnPointD;

    [SerializeField] GameObject TriggerZoneA;
    [SerializeField] GameObject TriggerZoneW;
    [SerializeField] GameObject TriggerZoneS;
    [SerializeField] GameObject TriggerZoneD;
    [SerializeField] ButtonTriggerZone TestTrigger;

    Collider ColTriggerZoneA;
    Collider ColTriggerZoneW;
    Collider ColTriggerZoneS;
    Collider ColTriggerZoneD;

    public TextMeshProUGUI Feedback;
    public TextMeshProUGUI scoreText;
    public int NoteCounter;

    [Header("Infos")]
    public int CurrentNote;
    private int spawnNote;
    public float currentTime;
    public bool songPlaying;
    public int Score;

    [Header("Settings")]
    public float Tempo;
    public float preBeats;
    private float tempoScale;
    //public float Forgivness;

    [Space]
    public float HitQualityPerfect = 0.3f;
    public float HitQualityGreat = 0.5f;
    public float HitQualityGood = 0.7f;

    [Space]
    public Color[] Colors;
    public int[] successValues;

    [System.Serializable]
    public class NoteData { public NoteID Note; public float Start; public float Length = 1; }

    [Header("Notes")]
    public NoteData[] SavedNotes;
    public float[] NotesTime; // Outdated
    public string[] NotesKind; // Outdated

    public PlayerControlls Controlls;

    public enum NoteID
    {
        none = 0,
        A = 1,
        W = 2,
        S = 3,
        D = 4
    }

    public enum HitQuality
    {
        None = 0,
        Perfect = 1,
        Great = 2,
        Good = 3,
        Miss = 4
    }

    private void Awake()
    {
        tempoScale = 60 / Tempo;
        //Cursor.visible = false;

        ColTriggerZoneW = TriggerZoneW.GetComponent<Collider>();
        ColTriggerZoneS = TriggerZoneS.GetComponent<Collider>();
        ColTriggerZoneA = TriggerZoneA.GetComponent<Collider>();
        ColTriggerZoneD = TriggerZoneD.GetComponent<Collider>();

        ColTriggerZoneW.enabled = false;
        ColTriggerZoneS.enabled = false;
        ColTriggerZoneA.enabled = false;
        ColTriggerZoneD.enabled = false;

    }

    private void OnEnable()
    {
        if (Controlls == null)
        {
            Controlls = new PlayerControlls();
            Controlls.Actions.SetCallbacks(this);
        }

        Controlls.Enable();
    }

    private void OnDisable()
    {
        Controlls.Disable();
    }

    private void OnDestroy()
    {
        Controlls.Dispose();
        Controlls = null;
    }

    void Update()
    {
        if (Time.time >= preBeats * tempoScale && songPlaying == false)
        {
            Song.Play();
            songPlaying = true;
        }

        //currentTime = Time.time;
        //if (Time.time - (preBeats * tempoScale) >= ((NotesTime[CurrentNote] - 1) * tempoScale) + (Forgivness * tempoScale))
        //{
        //    if (NotesTime.Length - 1 > CurrentNote)
        //    {
        //        CurrentNote++;
        //    }
        //}

        //if ((Time.time - (preBeats * tempoScale)) * Tempo / 60 + 12 >= (NotesTime[spawnNote] - 1) && NotesTime.Length - 1 > spawnNote)
        //{
        //    GameObject nextNote;

        //    nextNote = Instantiate(Note, SpawnPointA.transform.position, Quaternion.identity);
        //    nextNote.name = "Note A " + NoteCounter++;

        //    nextNote = Instantiate(Note, SpawnPointW.transform.position, Quaternion.identity);
        //    nextNote.name = "Note W " + NoteCounter++;

        //    nextNote = Instantiate(Note, SpawnPointS.transform.position, Quaternion.identity);
        //    nextNote.name = "Note S " + NoteCounter++;

        //    nextNote = Instantiate(Note, SpawnPointD.transform.position, Quaternion.identity);
        //    nextNote.name = "Note D " + NoteCounter++;

        //    spawnNote++;
        //}
    }

    public HitQuality GetHitQuality(float distance)
    {
        if (distance < 0.3f)
            return HitQuality.Perfect;

        if (distance < 0.5f)
            return HitQuality.Great;

        if (distance < 0.7f)
            return HitQuality.Good;

            return HitQuality.Miss;
    }

    public void Hit(InputAction.CallbackContext context, NoteID Input)
    {
        Debug.Log("Hit Key " + Input);
        if (context.started)
        {
            if (TestTrigger.WasNoteHit(Input, out float distance))
            {
                switch (GetHitQuality(distance))
                {
                    case HitQuality.None:
                        break;
                    case HitQuality.Perfect:
                        Feedback.text = "PERFECT!";
                        Score += successValues[0];
                        break;
                    case HitQuality.Great:
                        Feedback.text = "GREAT!";
                        Score += successValues[1];
                        break;
                    case HitQuality.Good:
                        Feedback.text = "GOOD!";
                        Score += successValues[2];
                        break;
                    case HitQuality.Miss:
                        Feedback.text = "MISS!";
                        break;
                    default:
                        break;
                }
                
                scoreText.text = "" + Score;
                Debug.Log($"Its {GetHitQuality(distance)} Hit");
            }
            //for (float i = 0f; i > 0.3f;)
            //{
            //    float Distance = (TestTrigger.transform.worldToLocalMatrix * Note.transform.position).z - TestTrigger.transform.localPosition.z;
            //    Feedback.text = "PERFECT!";
            //    Score += successValues[0];
            //    scoreText.text = "" + Score;
            //    Debug.Log("Its Perfect_Hit");
            //}

            for (int i = 0; i < 3; i++)
            {
            //    if (Mathf.Abs(Time.time - (preBeats * tempoScale) - (NotesTime[CurrentNote] - 1) * tempoScale) < (Forgivness * 0.5 * tempoScale) && Input == NotesKind[CurrentNote + i])
            //    {
            //        //Debug.Log("Perfect!" + context.control + " Delay: " + (Time.time - (preBeats * tempoScale) - (NotesTime[CurrentNote] - 1) * tempoScale));
            //        Feedback.text = "PERFECT!";
            //        Score += successValues[0];
            //        scoreText.text = "" + Score;
            //        break;
            //    }
            //    else
            //    {
            //if (Mathf.Abs(Time.time - (preBeats * tempoScale) - (NotesTime[CurrentNote] - 1) * tempoScale) < (Forgivness * 0.8 * tempoScale) && Input == NotesKind[CurrentNote + i])
            //{
            //    //Debug.Log("Great!" + context.control + " Delay: " + (Time.time - (preBeats * tempoScale) - (NotesTime[CurrentNote] - 1) * tempoScale));
            //    Feedback.text = "Great!";
            //    Score += successValues[1];
            //    scoreText.text = "" + Score;
            //    //break;
            //}
            //        else
            //        {

            //            if (Mathf.Abs(Time.time - (preBeats * tempoScale) - (NotesTime[CurrentNote] - 1) * tempoScale) < (Forgivness * tempoScale) && Input == NotesKind[CurrentNote + i])
            //            {
            //                //Debug.Log("Ok" + context.control + " Delay: " + (Time.time - (preBeats * tempoScale) - (NotesTime[CurrentNote] - 1) * tempoScale));
            //                Feedback.text = "ok";
            //                Score += successValues[2];
            //                scoreText.text = "" + Score;
            //                break;
            //            }
            //            else
            //            {
            //                if (NotesTime[CurrentNote + i] - 1 != NotesTime[CurrentNote + i + 1] - 1)
            //                {
            //                    //Debug.Log("Nope!" + context.control + " Delay: " + (Time.time - (preBeats * tempoScale) - (NotesTime[CurrentNote + i] - 1) * tempoScale));
            //                    Feedback.text = "Nope";
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //    if (NotesTime[CurrentNote * i] - 1 != NotesTime[CurrentNote + i + 1] - 1)
            //    {
            //        break;
            //    }
            }
        }
    }

    public IEnumerator ColCoroutine(Collider collider)
    {
        Debug.Log("Enu Start");
        collider.enabled = true;
        yield return new WaitForSeconds(1);
        collider.enabled = false;
    }

    public void OnUp(InputAction.CallbackContext context)
    {
        Hit(context, NoteID.W);

        if (context.started)
        {
            Ocha.GetComponent<Animator>().Play("Hit_LaneBC_3m");
            StartCoroutine(ColCoroutine(ColTriggerZoneW));
        }
    }

    public void OnDown(InputAction.CallbackContext context)
    {
        Hit(context, NoteID.S);

        if (context.started)
        {
            Ocha.GetComponent<Animator>().Play("Hit_LaneBC_3m");
            StartCoroutine(ColCoroutine(ColTriggerZoneS));
        }
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        Hit(context, NoteID.D);

        if (context.started)
        {
            Ocha.GetComponent<Animator>().Play("Hit_LaneBC_3m");
            StartCoroutine(ColCoroutine(ColTriggerZoneD));
        }
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        Hit(context, NoteID.A);

        if (context.started)
        {
            Ocha.GetComponent<Animator>().Play("Hit_LaneBC_3m");
            StartCoroutine(ColCoroutine(ColTriggerZoneA));
        }
    }
}