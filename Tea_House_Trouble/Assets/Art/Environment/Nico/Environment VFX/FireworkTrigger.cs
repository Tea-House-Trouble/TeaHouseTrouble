using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FireworkTrigger : MonoBehaviour
{
    public VisualEffect fireworkVFX;
    public void StartFirework()
    { 
        fireworkVFX.Play();
    }
}
