using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FireworkTrigger : MonoBehaviour
{
    public VisualEffect fireworkVFX;
    public GameObject Spawner;

    public void StartFirework()
    { 
        GameObject newObject = Instantiate(gameObject, Spawner.transform.position, Spawner.transform.rotation);
        fireworkVFX.Play();
        Destroy(newObject, 5f);
    }
}
