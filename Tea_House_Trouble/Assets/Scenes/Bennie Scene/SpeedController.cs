/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    [SerializeField] MyBlitFeature Blit;
    [SerializeField] RhythmManager ChainInput;

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

    }
    Material SpeedMaterial;
    void Update()
    { 

        if (ChainInput.ChainCounter == 0)
            SetSpeedLevelZero();

        else if (ChainInput.ChainCounter == ThresholdOne)
            SetSpeedLevelOne();

    }
    void SetSpeedLevelZero()
    {
        Blit.settings.SpeedShader.SetFloat("_Speed_Lines_Active", 0);
        Blit.settings.SpeedShader.SetFloat("_Radial_Blur_Active", 0);
    }

    void SetSpeedLevelOne()
    {
        Blit.settings.SpeedShader.SetFloat("_Speed_Lines_Active", 1);
        Blit.settings.SpeedShader.SetFloat("_Radial_Blur_Active", 1);
    }
}*/
