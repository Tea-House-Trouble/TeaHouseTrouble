using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class RythmManager : MonoBehaviour, PlayerControlls.IActionsActions
{
    public AudioSource Song;
    public GameObject Note;
    public TextMeshProUGUI Feedback;
    public TextMeshProUGUI scoreText;

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
    public float Forgivness;
    public Color[] Colors;
    public int[] successValues;

    [Header("Notes")]
    public float[] NotesTime;
    public string[] NotesKind;

    private PlayerControlls Controlls;

    private void Awake()
    {
        if (Controlls == null)
        {
            Controlls = new PlayerControlls();
            Controlls.Enable();
            Controlls.Actions.SetCallbacks(this);
        }
        tempoScale = 60 / Tempo;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Time.time >= preBeats * tempoScale && songPlaying == false)
        {
            Song.Play();
            songPlaying = true;
        }

        currentTime = Time.time;
        if (Time.time - (preBeats * tempoScale) >= ((NotesTime[CurrentNote] - 1) * tempoScale) + (Forgivness * tempoScale))
        {
            if (NotesTime.Length - 1 > CurrentNote)
            {
                CurrentNote++;
            }
        }

        if ((Time.time - (preBeats * tempoScale)) * Tempo / 60 + 12 >= (NotesTime[spawnNote] - 1) && NotesTime.Length - 1 > spawnNote)
        {
            GameObject go;
            if (NotesKind[spawnNote] == "a")
            {
                go = Instantiate(Note, new Vector3(-3, -8, 0), Quaternion.Euler(0, 0, 180));
              //  go.GetComponent<SpriteRenderer>().color = Colors[0];
            }
            if (NotesKind[spawnNote] == "s")
            {
                go = Instantiate(Note, new Vector3(-1, -8, 0), Quaternion.Euler(0, 0, -90));
              //  go.GetComponent<SpriteRenderer>().color = Colors[1];
            }
            if (NotesKind[spawnNote] == "w")
            {
                go = Instantiate(Note, new Vector3(1, -8, 0), Quaternion.Euler(0, 0, 90));
              //  go.GetComponent<SpriteRenderer>().color = Colors[2];
            }
            if (NotesKind[spawnNote] == "d")
            {
                go = Instantiate(Note, new Vector3(3, -8, 0), Quaternion.Euler(0, 0, 0));
              //  go.GetComponent<SpriteRenderer>().color = Colors[3];
            }

            spawnNote++;
        }
    }

    public void Hit(InputAction.CallbackContext context, string Input)
    {
        if (context.started)
        {
            for (int i = 0; i < 3; i++)
            {
                if (Mathf.Abs(Time.time - (preBeats * tempoScale) - (NotesTime[CurrentNote] - 1) * tempoScale) < (Forgivness * 0.5 * tempoScale) && Input == NotesKind[CurrentNote + i])
                {
                    Debug.Log("Perfect!" + context.control + " Delay: " + (Time.time - (preBeats * tempoScale) - (NotesTime[CurrentNote] - 1) * tempoScale));
                    Feedback.text = "PERFECT!";
                    Score += successValues[0];
                    scoreText.text = "" + Score;
                    break;
                }
                else
                {
                    if (Mathf.Abs(Time.time - (preBeats * tempoScale) - (NotesTime[CurrentNote] - 1) * tempoScale) < (Forgivness * 0.8 * tempoScale) && Input == NotesKind[CurrentNote + i])
                    {
                        Debug.Log("Great!" + context.control + " Delay: " + (Time.time - (preBeats * tempoScale) - (NotesTime[CurrentNote] - 1) * tempoScale));
                        Feedback.text = "Great!";
                        Score += successValues[1];
                        scoreText.text = "" + Score;
                        break;
                    }
                    else
                    {

                        if (Mathf.Abs(Time.time - (preBeats * tempoScale) - (NotesTime[CurrentNote] - 1) * tempoScale) < (Forgivness * tempoScale) && Input == NotesKind[CurrentNote + i])
                        {
                            Debug.Log("Ok" + context.control + " Delay: " + (Time.time - (preBeats * tempoScale) - (NotesTime[CurrentNote] - 1) * tempoScale));
                            Feedback.text = "ok";
                            Score += successValues[2];
                            scoreText.text = "" + Score;
                            break;
                        }
                        else
                        {
                            if (NotesTime[CurrentNote + i] - 1 != NotesTime[CurrentNote + i + 1] - 1)
                            {
                                Debug.Log("Nope!" + context.control + " Delay: " + (Time.time - (preBeats * tempoScale) - (NotesTime[CurrentNote + i] - 1) * tempoScale));
                                Feedback.text = "Nope";
                                break;
                            }
                        }
                    }
                }
                if (NotesTime[CurrentNote * i] - 1 != NotesTime[CurrentNote + i + 1] - 1)
                {
                    break;
                }
            }
        }
    }

    public void OnUp(InputAction.CallbackContext context)
    {
        Hit(context, "w");
    }

    public void OnDown(InputAction.CallbackContext context)
    {
        Hit(context, "s");
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        Hit(context, "d");
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        Hit(context, "a");
    }
}
