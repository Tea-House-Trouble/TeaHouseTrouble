using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Note : MonoBehaviour
{
    public RhythmManager.NoteID MyNoteID;

    public GameObject AnimationObjekt;

    public Quaternion BaseRotation;

    //public void Destroy()
    //{
    //   Destroy(gameObject);
    //}

    private void Update()
    {
        transform.rotation  = BaseRotation * PlayerFollower.PlayerFollow.transform.rotation;
    }

    public void StartDeathSequenz()
    {
        Daruma_Anim_Script DeathAnim = AnimationObjekt.GetComponent<Daruma_Anim_Script>();

        DeathAnim.Destroy();
        StartCoroutine(Despawn()); 
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

}