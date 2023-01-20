using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UIElements;

[System.Serializable]
public class SpeedController : MonoBehaviour
{
    
    [SerializeField] MyBlitFeature Blit;
    [SerializeField] RhythmManager ChainInput;
    //[SerializeField] ButtonTriggerZone TestTrigger;

    [Space]
    [Header("Speed Level One")]
    public float ThresholdOne = 10.0f;

    [Space]
    [Header("Speed Level Two")]
    public float ThresholdTwo = 20.0f;

    [Space]
    [Header("Speed Level Three")]
    public float ThresholdThree = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        SetSpeedLevelZero();
    }

    public void ScanSpeedLevel()
    
    {
        SetSpeedLevelOne();
        Debug.Log("Hitiiiiiiiiiiiiiit");
        /*RhythmManager rhythmManager = GetComponent<RhythmManager>();
        int chainCounter = rhythmManager.ChainCounter;

        if (ChainInput.ChainCounter < ThresholdOne)
            SetSpeedLevelZero();
            

        else if (ChainInput.ChainCounter == ThresholdOne)
            SetSpeedLevelOne();*/

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
        Blit.Create();
    }
}
