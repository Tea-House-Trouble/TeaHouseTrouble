using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongEnemyMiddle : MonoBehaviour
{
    private bool wasPressedHold = false;

    private void OnTriggerEnter(Collider other)
    {
        wasPressedHold = true;
        other.gameObject.tag.Equals("Enemy");
    }

    private void OnTriggerExit(Collider other)
    {
        wasPressedHold = true;
        other.gameObject.tag.Equals("Enemy");
    }
}
