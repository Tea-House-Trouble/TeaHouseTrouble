using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    public RythmManager Rhy;

    private void Awake()
    {
        Rhy = FindObjectOfType<RythmManager>();
    }

    private void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * (Rhy.Tempo / 60);
        if (transform.position.y > 0)
        {
            //Time.timeScale = 0;
            //Rhy.Song.Pause();
        }
        if (transform.position.y > 7)
        {
            Destroy(gameObject);
        }
    }
}