using System;
using UnityEngine;

public class DetectMouseClick : MonoBehaviour {

    string _name;
    private void Start() {
        _name =  gameObject.ToString();
    }
    private void OnMouseDown() {
        Debug.Log(_name + "WAS CLICKED");
    }
}