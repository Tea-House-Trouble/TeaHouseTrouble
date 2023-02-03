using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_TeaSpiritFX : MonoBehaviour
{
    [SerializeField] ParticleSystem ConfettiVFX;
    [SerializeField] AudioSource LidSFX;
    [SerializeField] AudioSource JumpSFX;
    [SerializeField] AudioSource ConfettiSFX;

    public void Lid()
    {
        LidSFX.Play();
    }

    public void Jump()
    {
        JumpSFX.Play();
    }

    public void Confetti()
    {
        ConfettiVFX.Play();
        ConfettiSFX.Play();
    }
}
