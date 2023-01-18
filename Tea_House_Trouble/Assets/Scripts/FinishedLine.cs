using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedLine : MonoBehaviour
{
    [SerializeField] GameObject ResultPanel;

    private void OnTriggerExit(Collider other)
    {
        ResultPanel.SetActive(true);
        Time.timeScale = 0f;
        RhythmManager.FindObjectOfType<AudioSource>().Stop();
    }
}
