using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreScrolling : MonoBehaviour
{
    [SerializeField] Scrollbar highScrScrb;
    [SerializeField] GameObject highScrCntr, highScrDsp, scrbTop, scrbBttm;
    [SerializeField] Material testMat;
    Renderer rend;

    private void Awake() {
        highScrScrb = GameObject.Find("HighscoreScrollbar").GetComponent<Scrollbar>();
        highScrCntr = GameObject.Find("HighscoreContainer");
        highScrDsp = GameObject.Find("HighScoreDisplay");
        scrbTop = GameObject.Find("SM_Menu_Card_Scroll_Top 1");
        scrbBttm = GameObject.Find("SM_Menu_Card_Scroll_Bottom 1");
        highScrScrb.size = 0.05f;
        highScrScrb.value = 1;
    }

    public void SetUpScrollbar() {       
        highScrScrb.size = 0.05f;
        highScrScrb.value = 1;
        highScrScrb.numberOfSteps = (highScrCntr.transform.childCount - 10)*10;
    }

    public void SetSizeAndValue() {
        highScrScrb.size = 0.05f;
        float rotation = (highScrScrb.numberOfSteps - (int)(highScrScrb.value * highScrScrb.numberOfSteps)) * 75;
        scrbTop.transform.Rotate(-rotation, 0, 0, Space.Self);
        scrbBttm.transform.Rotate(rotation, 0, 0, Space.Self);
      }
}
