using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_SpawnerScript : MonoBehaviour
{
    public GameObject Daruma;
    bool KeepSpawnerRunning;
    float Distance;
    float SpawnerPosX;

    void Start()
    {
        KeepSpawnerRunning = true;
        StartCoroutine (Spawn());
        

    }

IEnumerator Spawn()
    {
         while(KeepSpawnerRunning==true)
         {
            GameObject Dar = (GameObject) Instantiate(Daruma, new Vector3(0, 0, Distance), Quaternion.identity);
            Dar.transform.Rotate(new Vector3(0, 180, 0));
            int wait_time = Random.Range (3, 5);
            yield return new WaitForSeconds (wait_time);
         }
    }
       
 private void Update()
    {
    Distance = Distance + 1 * Time.deltaTime;
    }
        


}
