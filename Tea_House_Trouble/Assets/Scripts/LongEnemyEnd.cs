using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LongEnemyEnd : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        other.gameObject.tag.Equals("Enemy");
        other.gameObject.GetComponent<Note>().Destroy(); 
    }
}
