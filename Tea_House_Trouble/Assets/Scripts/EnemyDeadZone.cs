using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadZone : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            Debug.Log("Enter Collider" + collision.gameObject.name);
            collision.gameObject.GetComponent<Notes>().Destroy();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.GetComponent<Notes>().Destroy();
            Debug.Log("Enter Triggerzone" + other.gameObject.name);
        }
    }
}
