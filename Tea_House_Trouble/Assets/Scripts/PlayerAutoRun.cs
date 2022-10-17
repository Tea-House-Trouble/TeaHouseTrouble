using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoRun : MonoBehaviour
{
    public static Transform PlayerTransform;
    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(1, 0, 0);
    }

    private void Awake()
    {
        PlayerTransform = transform;
    }
}
