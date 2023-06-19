using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreScrolling : MonoBehaviour
{
    [SerializeField] Scrollbar highScrScrb;
    [SerializeField] GameObject highScrCntr, scrbTop, scrbBttm;

    private void Awake() {
        scrbTop = GameObject.Find("SM_Menu_Card_Scroll_Top");
        scrbBttm = GameObject.Find("SM_Menu_Card_Scroll_Bottom");
    }

    public void SetUpScrollbar() {       
        highScrScrb.size = 0.05f;
        highScrScrb.numberOfSteps = highScrCntr.transform.childCount;
        Debug.Log("HIGHSCORES" + highScrCntr.transform.childCount);
    }

    public void SetSizeAndValue() {
        highScrScrb.size = 0.05f;
    }
}
