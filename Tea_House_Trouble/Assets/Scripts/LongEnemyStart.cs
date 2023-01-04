using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongEnemyStart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.tag.Equals("Enemy");
        other.gameObject.GetComponent<Note>().Destroy();
    }
}
