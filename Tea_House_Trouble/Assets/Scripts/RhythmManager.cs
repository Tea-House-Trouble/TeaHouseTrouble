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
    //public GameObject shortNote;
    //public GameObject longNote;
    public GameObject SpawnPointA;
    public GameObject SpawnPointW;
    public GameObject SpawnPointS;
    public GameObject SpawnPointD;
    public GameObject ChainCounterMessage;

    [SerializeField] ButtonTriggerZone TestTrigger;

    public TextMeshProUGUI Feedback;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI chainCounterText;
    public TextMeshProUGUI chainCounterNumberText;
    public int NoteCounter;

    [Header("Infos")]
    public int CurrentNote;
    private int spawnNote;
    public float currentTime;
    public bool songPlaying;
    public float Score;

    [Header("Settings")]
    public float Tempo;
    public float preBeats;
    private float tempoScale;

    [Space]
    [Header("Forgivness")]
    public float HitQualityPerfect = 0.3f;
    public float HitQualityGood = 0.5f;
    public float HitQualityBad = 0.7f;

    [Space]
    [Header("Multiplikations")]
    public float MultiplikationPerfect = 2f;
    public float MultiplikationGood = 1.2f;
    public float MultiplikationBad = 0.7f;
    public int ChainCounter;

    [Space]
    public int[] successValues;
    public float elapsedTime;

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
        Good = 2,
        Bad = 3,
        Miss = 4
    }

    private void Awake()
    {
        tempoScale = 60 / Tempo;
        //Cursor.visible = false;
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
        if (ChainCounterMessage.activeSelf)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= 2)
            {
                ChainCounterMessage.SetActive(false);
            }

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
    }
    public HitQuality GetHitQuality(float distance)
    {
        if (distance < 0.3f)
            return HitQuality.Perfect;

        if (distance < 0.5f)
            return HitQuality.Good;

        if (distance < 0.7f)
            return HitQuality.Bad;

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
                        Score += successValues[0] * MultiplikationPerfect + Mathf.Pow(1 + ChainCounter / 100, 2);
                        ChainCounter++;
                        ChainCounterMessage.SetActive(true);
                        chainCounterNumberText.text = "" + ChainCounter;
                        elapsedTime = 0;
                        break;
                    case HitQuality.Good:
                        Feedback.text = "GOOD!";
                        Score += successValues[1] * MultiplikationGood + Mathf.Pow(1 + ChainCounter / 100, 2);
                        ChainCounter++;
                        ChainCounterMessage.SetActive(true);
                        chainCounterNumberText.text = "" + ChainCounter;
                        elapsedTime = 0;
                        break;
                    case HitQuality.Bad:
                        Feedback.text = "Bad!";
                        Score += successValues[2] * MultiplikationBad + Mathf.Pow(1 + ChainCounter / 100, 2);
                        ChainCounter++;
                        ChainCounterMessage.SetActive(true);
                        chainCounterNumberText.text = "" + ChainCounter;
                        elapsedTime = 0;
                        break;
                    case HitQuality.Miss:
                        Feedback.text = "MISS!";
                        ChainCounter = 0;
                        ChainCounterMessage.SetActive(false);
                        chainCounterNumberText.text = "" + ChainCounter;
                        break;
                    default:
                        break;
                }

                //ChainCounterMessage.SetActive(false);
                scoreText.text = ((int)Score).ToString();
                //StartCoroutine(DisplayChainCounter(ChainCounterMessage));
                Debug.Log($"Its {GetHitQuality(distance)} Hit");
            }

            //  Dient noch als evtl. Rechenhilfe
            //for (int i = 0; i < 3; i++)
            ////    if (Mathf.Abs(Time.time - (preBeats * tempoScale) - (NotesTime[CurrentNote] - 1) * tempoScale) < (Forgivness * 0.5 * tempoScale) && Input == NotesKind[CurrentNote + i])

        }
    }

    public IEnumerator DisplayChainCounter(GameObject gameObject)
    {
        Debug.Log("Enu Start");
        yield return new WaitForSeconds(1);
        ChainCounterMessage.SetActive(false);
    }

    public void OnUp(InputAction.CallbackContext context)
    {
        Hit(context, NoteID.W);

        if (context.started)
        {
            Ocha.GetComponent<Animator>().Play("Hit");
        }
    }

    public void OnDown(InputAction.CallbackContext context)
    {
        Hit(context, NoteID.S);

        if (context.started)
        {
            Ocha.GetComponent<Animator>().Play("Hit");
        }
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        Hit(context, NoteID.D);

        if (context.started)
        {
            Ocha.GetComponent<Animator>().Play("Hit");
        }
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        Hit(context, NoteID.A);

        if (context.started)
        {
            Ocha.GetComponent<Animator>().Play("Hit");
        }
    }
}