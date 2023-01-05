using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public static GameObject PlayerFollow;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerFollow = gameObject;
    }
}
