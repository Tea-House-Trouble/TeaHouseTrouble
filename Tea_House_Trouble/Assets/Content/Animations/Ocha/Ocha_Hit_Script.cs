using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static RhythmManager;

public class Ocha_Hit_Script : MonoBehaviour /*PlayerControlls.IActionsActions*/
{
    Animator OCHA_Animator;
    public GameObject Ocha;
    [SerializeField] ParticleSystem Hit01;
    [SerializeField] ParticleSystem Hit02;

    void Start()
    {
        OCHA_Animator = gameObject.GetComponent<Animator>();
    }

    //void Update()
    //{
    //    if (Input.GetButtonDown("Jump"))
    //    {
    //        if (OCHA_Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit01"))
    //        {
    //            OCHA_Animator.Play("Hit02");
    //            Hit02.Play();
    //        }

    //        else
    //        {
    //            OCHA_Animator.Play("Hit01");
    //            Hit01.Play();
    //        }

    //    }
    //}

    //public void OnUp(InputAction.CallbackContext context)
    //{
    //    if (context.started)
    //    {
    //        OnButtonPressed(NoteID.W);
    //        if (OCHA_Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit01"))
    //        {

    //            OCHA_Animator.Play("Hit02");
    //            Hit02.Play();
    //        }
    //        else
    //        {
    //            OCHA_Animator.Play("Hit01");
    //            Hit01.Play();
    //        }
    //    }
    //}

    //public void OnDown(InputAction.CallbackContext context)
    //{
    //    if (context.started)
    //    {
    //        if (OCHA_Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit01"))
    //        {

    //            OCHA_Animator.Play("Hit02");
    //            Hit02.Play();
    //        }
    //        else
    //        {
    //            OCHA_Animator.Play("Hit01");
    //            Hit01.Play();
    //        }
    //    }
    //}

    //public void OnLeft(InputAction.CallbackContext context)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void OnRight(InputAction.CallbackContext context)
    //{
    //    throw new System.NotImplementedException();
    //}

    //private void OnButtonPressed(NoteID note)
    //{
    //    if (lastPressedNote == note && lastPressedTime == Time.timeSinceLevelLoad)
    //        return;

    //    Hit(note);

    //    ButtonPressed?.Invoke(note);

    //    StartAttackAnimation(note);

    //    lastPressedTime = Time.timeSinceLevelLoad;
    //    lastPressedNote = note;
    //}
}
