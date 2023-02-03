using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlitSwitch : MonoBehaviour
{
    [SerializeField] MyBlitFeature Blit;
    private void Awake()
    {
        Blit.settings.MaterialToBlit.SetFloat("_Speed_Lines_Active", 0);
        Blit.settings.MaterialToBlit.SetFloat("_Radial_Blur_Active", 0);
    }
}
