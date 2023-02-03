using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using System.Runtime.CompilerServices;
using UnityEngine.TextCore.Text;
//using UnityEditor.Rendering.LookDev;

public class RhythmManager : MonoBehaviour, PlayerControlls.IActionsActions
{
    public bool _isPaused = false;
    private int _chainCounter, _miss, _bad, _good, _perfect;
    public Scores temp;

    public static event System.Action<NoteID> ButtonPressed;

    public GameObject Ocha, FANLeft, FANRight;
    Animator OCHA_Animator, LeftFAN_Animator, RightFAN_Animator;
    public AudioSource Song;
    public GameObject ChainCounterMessage;

    [SerializeField] ButtonTriggerZone TestTrigger;

    public TextMeshProUGUI Feedback, scoreText, chainCounterText, chainCounterNumberText, GameStartTimerText;

    [Header("Infos")]
    public bool songPlaying;
    public static float Score;

    [Header("Settings")]
    public float Tempo, preBeats;
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
    public int GameStartTimer;
    private float MusicTime;
    [SerializeField] Slider MusicTimeSlider;

    [Space]
    [SerializeField] ParticleSystem Hit01, Hit02, Sparkle, LeftFANHit01, LeftFANHit02, RightFANHit01, RightFANHit02;

    [Space]
    [Header("SFX Sounds")]
    [SerializeField] AudioSource BattleSounds;
    [SerializeField] AudioClip PerfectSwordHit, GoodSwordHit, BadSwordHit, MissSwordHit, PerfectFANHit, GoodFANHit, BadFANHit, MissFANHit;
    [Range(0f, 1f)] public float BattleSoundsVolume;

    [Space]
    [Header("Arrow VFX")]

    public GameObject ArrowUp, ArrowDown, ArrowRight, ArrowLeft;

    [SerializeField] MyBlitFeature Blit;
    [Space]
    [Header("Speed Level One")]
    public float ThresholdOne = 15.0f;
    public float MaskOne = 1.0f;
    public float DensityOne = 0.3f;
    [Space]
    [Header("Speed Level Two")]
    public float ThresholdTwo = 30.0f;
    public float MaskTwo = 1.5f;
    public float DensityTwo = 0.35f;
    [Space]
    [Header("Speed Level Three")]
    public float ThresholdThree = 50.0f;
    public float MaskThree = 2.0f;
    public float DensityThree = 0.4f;

    [Space]
    [Header("Feedback Scale")]
    public float ScaleTime = 0.5f;
    public float DownScaleTime = 0.5f;
    public Vector3 Size;

    [Space]
    [Header("Firework Slect")]
    public FireworkTrigger Firework20;
    public FireworkTrigger Firework50;
    public FireworkTrigger Firework100;
    public FireworkTrigger Firework150;
    public FireworkTrigger Firework200;

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
    private void Awake()
    {
        _chainCounter = 0;
        _miss = 0;
        _bad = 0;
        _good = 0;
        _perfect = 0;
        tempoScale = 60 / Tempo;
        Song.PlayDelayed(8);
        songPlaying = true;
    }

    void Start()
    {
        temp = new Scores();
        OCHA_Animator = Ocha.GetComponent<Animator>();
        LeftFAN_Animator = FANLeft.GetComponent<Animator>();
        RightFAN_Animator = FANRight.GetComponent<Animator>();
        StartCoroutine(CountDownGameStart());
        SetSpeedLevelZero();
    }

    void Update()
    {
        
        if (ChainCounterMessage.activeSelf)
        {
            ChainCounterElapsedTime += Time.deltaTime;

            if (ChainCounterElapsedTime >= 2) { Feedback.gameObject.SetActive(false); }
            else { Feedback.gameObject.SetActive(true); }
        }
        if (ChainCounter < ThresholdOne)
            SetSpeedLevelZero();

        //if (songPlaying == false && Time.time == preBeats * tempoScale)
        //{
        //    Song.PlayDelayed(8);
        //    songPlaying = true;
        //}
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

    private void OnDisable() { Controlls.Disable(); }

    private void OnDestroy()
    {
        Controlls.Dispose();
        Controlls = null;
    }

    IEnumerator CountDownGameStart()
    {
        while (GameStartTimer > 0)
        {
            GameStartTimerText.text = GameStartTimer.ToString();
            yield return new WaitForSeconds(1);

            GameStartTimer--;
        }
        GameStartTimerText.text = "GO!";
        yield return new WaitForSeconds(1);
        GameStartTimerText.gameObject.SetActive(false);
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
                    //Feedback.text = "PERFECT!";
                    _perfect++;
                    Score += successValues[0] * MultiplikationPerfect * Mathf.Pow(1f + ChainCounter / 100f, 2f);
                    ChainCounter++;
                    ChainCounterMessage.SetActive(true);
                    Feedback.text = "PERFECT! x" + ChainCounter;
                    ScaleFeedback(Size, ScaleTime, DownScaleTime);
                    chainCounterNumberText.text = "" + ChainCounter;
                    ChainCounterElapsedTime = 0;

                    if (Input == NoteID.S || Input == NoteID.W) { BattleSounds.PlayOneShot(PerfectSwordHit, BattleSoundsVolume); }
                    else { BattleSounds.PlayOneShot(PerfectFANHit, BattleSoundsVolume); }
                    Sparkle.Play();

                    if (HitNote != null)
                        HitNote.StartDeathSequenz();
                    break;

                case HitQuality.Good:
                    //Feedback.text = "GOOD!";
                    _good++;
                    Score += successValues[1] * MultiplikationGood * Mathf.Pow(1f + ChainCounter / 100f, 2f);
                    ChainCounter++;
                    ChainCounterMessage.SetActive(true);
                    Feedback.text = "GOOD! x" + ChainCounter;
                    ScaleFeedback(Size, ScaleTime, DownScaleTime);
                    chainCounterNumberText.text = "" + ChainCounter;
                    ChainCounterElapsedTime = 0;

                    if (Input == NoteID.S || Input == NoteID.W) { BattleSounds.PlayOneShot(GoodSwordHit, BattleSoundsVolume); }
                    else { BattleSounds.PlayOneShot(GoodFANHit, BattleSoundsVolume); }

                    if (HitNote != null)
                        HitNote.StartDeathSequenz();
                    break;

                case HitQuality.Bad:
                    //Feedback.text = "Bad!";
                    _bad++;
                    Score += successValues[2] * MultiplikationBad * Mathf.Pow(1f + ChainCounter / 100f, 2f);
                    ChainCounter = 0;
                    ChainCounterMessage.SetActive(true);
                    Feedback.text = "Bad!";
                    ScaleFeedback(Size, ScaleTime, DownScaleTime);
                    chainCounterNumberText.text = "" + ChainCounter;
                    ChainCounterElapsedTime = 0;

                    if (Input == NoteID.S || Input == NoteID.W) { BattleSounds.PlayOneShot(BadSwordHit, BattleSoundsVolume); }
                    else { BattleSounds.PlayOneShot(BadFANHit, BattleSoundsVolume); }

                    if (HitNote != null)
                        HitNote.StartDeathSequenz();
                    break;

                case HitQuality.Miss:
                    MissedNote();

                    if (Input == NoteID.S || Input == NoteID.W) { BattleSounds.PlayOneShot(MissSwordHit, BattleSoundsVolume); }
                    else { BattleSounds.PlayOneShot(MissFANHit, BattleSoundsVolume); }
                    break;

                default:
                    break;
            }

            scoreText.text = ((int)Score).ToString();
            Debug.Log($"Its {GetHitQuality(distance)} Hit, CurrentScore" + Score);
            ScanSpeedLevel();
        }
        else
        {
            if (Input == NoteID.S || Input == NoteID.W) { BattleSounds.PlayOneShot(MissSwordHit, BattleSoundsVolume); }
            else { BattleSounds.PlayOneShot(MissFANHit, BattleSoundsVolume); }
        }
        //  Dient noch als evtl. Rechenhilfe
        //for (int i = 0; i < 3; i++)
        ////    if (Mathf.Abs(Time.time - (preBeats * tempoScale) - (NotesTime[CurrentNote] - 1) * tempoScale) < (Forgivness * 0.5 * tempoScale) && Input == NotesKind[CurrentNote + i])
    }

    private bool isScaling = false;
    private Coroutine scaleCoroutine;
    private Coroutine downscaleCoroutine;

    public void ScaleFeedback(Vector3 targetScale, float duration, float decreaseDuration)
    {
        if (isScaling)
        {
            StopAllCoroutines();
            Feedback.transform.localScale = new Vector3(1, 1, 1);
        }

        scaleCoroutine = StartCoroutine(ScaleUICoroutine(Feedback, targetScale, duration, decreaseDuration));
    }

    private IEnumerator ScaleUICoroutine(TextMeshProUGUI text, Vector3 targetScale, float duration, float decreaseDuration)
    {
        isScaling = true;
        Vector3 startScale = text.transform.localScale;
        float startTime = Time.time;
        float t;
        while (Time.time - startTime < duration)
        {
            t = (Time.time - startTime) / duration;
            text.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        downscaleCoroutine = StartCoroutine(DownscaleUICoroutine(text, decreaseDuration));
    }

    private IEnumerator DownscaleUICoroutine(TextMeshProUGUI text, float decreaseDuration)
    {
        Vector3 startScale = text.transform.localScale;
        Vector3 targetScale = new Vector3(1, 1, 1);
        float startTime = Time.time;
        float t;
        while (Time.time - startTime < decreaseDuration)
        {
            t = (Time.time - startTime) / decreaseDuration;
            text.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }
        text.transform.localScale = targetScale;
        isScaling = false;
    }

public void ScanSpeedLevel()
    {
        if (ChainCounter < ThresholdOne)
            SetSpeedLevelZero();
        else if (ChainCounter == ThresholdOne)
            SetSpeedLevelOne();
        else if (ChainCounter == ThresholdTwo)
            SetSpeedLevelTwo();
        else if (ChainCounter == ThresholdThree)
        {
            SetSpeedLevelThree();
            Firework50.StartFirework();
        }

        else if (ChainCounter == 20.0f)
            Firework20.StartFirework();
        // else if (ChainCounter == 50.0f)
        //   Firework50.StartFirework();
        else if (ChainCounter == 100.0f)
            Firework100.StartFirework();
        else if (ChainCounter == 150.0f)
            Firework150.StartFirework();
        else if (ChainCounter == 200.0f)
            Firework200.StartFirework();
    }

    void SetSpeedLevelZero()
    {
        Blit.settings.MaterialToBlit.SetFloat("_Speed_Lines_Active", 0);
        Blit.settings.MaterialToBlit.SetFloat("_Radial_Blur_Active", 0);
    }
    void SetSpeedLevelOne()
    {
        Blit.settings.MaterialToBlit.SetFloat("_Speed_Lines_Active", 1);
        Blit.settings.MaterialToBlit.SetFloat("_Radial_Blur_Active", 1);
        Blit.settings.MaterialToBlit.SetFloat("_Mask_Amount", MaskOne);
        Blit.settings.MaterialToBlit.SetFloat("_Line_Density", DensityOne);
        Blit.Create();
    }
    void SetSpeedLevelTwo()
    {
        Blit.settings.MaterialToBlit.SetFloat("_Speed_Lines_Active", 1);
        Blit.settings.MaterialToBlit.SetFloat("_Radial_Blur_Active", 1);
        Blit.settings.MaterialToBlit.SetFloat("_Mask_Amount", MaskTwo);
        Blit.settings.MaterialToBlit.SetFloat("_Line_Density", DensityTwo);
        Blit.Create();
    }
    void SetSpeedLevelThree()
    {
        Blit.settings.MaterialToBlit.SetFloat("_Speed_Lines_Active", 1);
        Blit.settings.MaterialToBlit.SetFloat("_Radial_Blur_Active", 1);
        Blit.settings.MaterialToBlit.SetFloat("_Mask_Amount", MaskThree);
        Blit.settings.MaterialToBlit.SetFloat("_Line_Density", DensityThree);
        Blit.Create();
    }

    public void MissedNote()
    {
        Feedback.text = "MISS!";
        _miss++;
        if (_chainCounter <= ChainCounter) { _chainCounter = ChainCounter; }
        ChainCounter = 0;
        ChainCounterMessage.SetActive(true);
        chainCounterNumberText.text = "" + ChainCounter;
        ChainCounterElapsedTime = 0;
    }

    public void OnUp(InputAction.CallbackContext context)
    {
        if(_isPaused == false) { 
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

                ArrowColor arrowColor = ArrowUp.GetComponent<ArrowColor>();
                arrowColor.PerformAction();
            }
        }
    }

    public void OnDown(InputAction.CallbackContext context)
    {
        if(_isPaused == false) {
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

                ArrowColor arrowColor = ArrowDown.GetComponent<ArrowColor>();
                arrowColor.PerformAction();
            }
        }
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        if (_isPaused == false) {
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

                ArrowColor arrowColor = ArrowRight.GetComponent<ArrowColor>();
                arrowColor.PerformAction();
            }
        }
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        if (_isPaused == false) {
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

                ArrowColor arrowColor = ArrowLeft.GetComponent<ArrowColor>();
                arrowColor.PerformAction();
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

    private void StartAttackAnimation(NoteID note) { Ocha.GetComponent<Animator>().Play("Hit"); }

    public Scores SetUpCurrentScore()
    {
        temp.Points = (int)Score;

        switch (temp.Points)
        {
            case > 40:
                temp.Rank = "S";
                break;

            case > 30:
                temp.Rank = "A";
                break;

            case > 20:
                temp.Rank = "B";
                break;

            case > 10:
                temp.Rank = "C";
                break;

            case > 0:
                temp.Rank = "D";
                break;

            default:
                break;
        }

        temp.Chain = _chainCounter;
        temp.Miss = _miss;
        temp.Bad = _bad;
        temp.Good = _good;
        temp.Perfect = _perfect;

        Score = 0;
        _chainCounter = 0;
        _miss = 0;
        _bad = 0;
        _good = 0;
        _perfect = 0;

        return temp;
    }
}