using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UIElements;
//using UnityEditor.Rendering.LookDev;

public class RhythmManager : MonoBehaviour, PlayerControlls.IActionsActions
{
    public static event System.Action<NoteID> ButtonPressed;


    public GameObject Ocha;
    public GameObject FANLeft;
    public GameObject FANRight;
    Animator OCHA_Animator;
    Animator LeftFAN_Animator;
    Animator RightFAN_Animator;
    public AudioSource Song;
    public GameObject Note;
    public GameObject ChainCounterMessage;

    [SerializeField] ButtonTriggerZone TestTrigger;

    public TextMeshProUGUI Feedback;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI chainCounterText;
    public TextMeshProUGUI chainCounterNumberText;
    public int NoteCounter;

    [Header("Infos")]
    public int CurrentNote;
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
    public float ChainCounterElapsedTime;

    [System.Serializable]
    public class NoteData { public NoteID Note; public float Start; public float Length = 1; }

    [Header("Notes")]
    public NoteData[] SavedNotes;
    public float[] NotesTime; // Outdated
    public string[] NotesKind; // Outdated

    [Space]
    [SerializeField] ParticleSystem Hit01;
    [SerializeField] ParticleSystem Hit02;
    [SerializeField] ParticleSystem LeftFANHit01;
    [SerializeField] ParticleSystem LeftFANHit02;
    [SerializeField] ParticleSystem RightFANHit01;
    [SerializeField] ParticleSystem RightFANHit02;

    public PlayerControlls Controlls;

    private float lastPressedTime;
    private NoteID lastPressedNote;

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

    void Start()
    {
        OCHA_Animator = Ocha.GetComponent<Animator>();
        LeftFAN_Animator = FANLeft.GetComponent<Animator>();
        RightFAN_Animator = FANRight.GetComponent<Animator>();
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
            ChainCounterElapsedTime += Time.deltaTime;

            if (ChainCounterElapsedTime >= 2)
            {
                Feedback.gameObject.SetActive(false);
            }
            else
            {
                Feedback.gameObject.SetActive(true);
            }
        }
        if (songPlaying == false && Time.time >= preBeats * tempoScale)
        {
            Song.Play();
            songPlaying = true;
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

    public void Hit(NoteID Input)
    {
        Debug.Log("Hit Key " + Input, this);

        if (TestTrigger.WasNoteHit(Input, out float distance, out Note HitNote))
        {
            switch (GetHitQuality(distance))
            {
                case HitQuality.None:
                    break;
                case HitQuality.Perfect:
                    Feedback.text = "PERFECT!";
                    Score += successValues[0] * MultiplikationPerfect * Mathf.Pow(1f + ChainCounter / 100f, 2f);
                    ChainCounter++;
                    ChainCounterMessage.SetActive(true);
                    chainCounterNumberText.text = "" + ChainCounter;
                    ChainCounterElapsedTime = 0;

                    if (HitNote != null)
                        HitNote.StartDeathSequenz();
                    break;
                case HitQuality.Good:
                    Feedback.text = "GOOD!";
                    Score += successValues[1] * MultiplikationGood * Mathf.Pow(1f + ChainCounter / 100f, 2f);
                    ChainCounter++;
                    ChainCounterMessage.SetActive(true);
                    chainCounterNumberText.text = "" + ChainCounter;
                    ChainCounterElapsedTime = 0;

                    if (HitNote != null)
                        HitNote.StartDeathSequenz();
                    break;
                case HitQuality.Bad:
                    Feedback.text = "Bad!";
                    Score += successValues[2] * MultiplikationBad * Mathf.Pow(1f + ChainCounter / 100f, 2f);
                    ChainCounter++;
                    ChainCounterMessage.SetActive(true);
                    chainCounterNumberText.text = "" + ChainCounter;
                    ChainCounterElapsedTime = 0;

                    if (HitNote != null)
                        HitNote.StartDeathSequenz();
                    break;
                case HitQuality.Miss:
                    Feedback.text = "MISS!";
                    ChainCounter = 0;
                    ChainCounterMessage.SetActive(true);
                    chainCounterNumberText.text = "" + ChainCounter;
                    ChainCounterElapsedTime = 0;
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

    public void OnUp(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnButtonPressed(NoteID.W);
            if (OCHA_Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit01"))

            {

                OCHA_Animator.Play("Hit02");
                Hit02.Play();
            }
            else
            {
                OCHA_Animator.Play("Hit01");
                Hit01.Play();
            }

        }

    }

    public void OnDown(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnButtonPressed(NoteID.S);
            if (OCHA_Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit01"))

            {

                OCHA_Animator.Play("Hit02");
                Hit02.Play();
            }
            else
            {
                OCHA_Animator.Play("Hit01");
                Hit01.Play();
            }
        }
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnButtonPressed(NoteID.D);

            if (RightFAN_Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit01"))
            {
                RightFAN_Animator.Play("Hit02");
                RightFANHit02.Play();
            }

            else
            {
                RightFAN_Animator.Play("Hit01");
                RightFANHit01.Play();
            }
        }
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnButtonPressed(NoteID.A);

            if (LeftFAN_Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit01"))
            {
                LeftFAN_Animator.Play("Hit02");
                LeftFANHit02.Play();
            }

            else
            {
                LeftFAN_Animator.Play("Hit01");
                LeftFANHit01.Play();
            }

        }

    }

    private void OnButtonPressed(NoteID note)
    {
        if (lastPressedNote == note && lastPressedTime == Time.timeSinceLevelLoad)
            return;

        Hit(note);

        ButtonPressed?.Invoke(note);

        StartAttackAnimation(note);

        lastPressedTime = Time.timeSinceLevelLoad;
        lastPressedNote = note;
    }

    private void StartAttackAnimation(NoteID note)
    {
        Ocha.GetComponent<Animator>().Play("Hit");
    }
}