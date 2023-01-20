using System.Collections;
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
    public Material SpeedMaterial;
    // Update is called once per frame
    void Update()
    {
        if (ChainInput.ChainCounter < ThresholdOne)
            SpeedMaterial = Resources.Load<Material>("Assets/Scenes/Bennie Scene/Speed Shader Level 0.mat");
        else if (ChainInput.ChainCounter >= ThresholdOne && ChainInput.ChainCounter < ThresholdTwo)
            SpeedMaterial = Resources.Load<Material>("Assets/Scenes/Bennie Scene/Speed Shader Level 1.mat");
        else if (ChainInput.ChainCounter >= ThresholdTwo && ChainInput.ChainCounter < ThresholdThree)
            SpeedMaterial = Resources.Load<Material>("Assets/Scenes/Bennie Scene/Speed Shader Level 2.mat");

        Blit.UpdateMaterial(SpeedMaterial);
    }
}