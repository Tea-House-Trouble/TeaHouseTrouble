using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskObject : MonoBehaviour
{
    public GameObject maskObj;

    private void Start()
    {
        //for(int i = 0; i < maskObj.Length; i++)
        maskObj.GetComponent<SkinnedMeshRenderer>().material.renderQueue = 3002;
    }

    
}
