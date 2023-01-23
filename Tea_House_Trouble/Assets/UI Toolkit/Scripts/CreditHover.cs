using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditHover : MonoBehaviour
{
    private Outline _outline; 
    private float _min, _max;

    public float outlineMin = 1f;
    public float outlineMax = 2f;
    public float outlineHoverMin = 4f;
    public float outlineHoverMax = 5f;

    private bool _glowUp = true;

    public GameObject Credit;


    void Awake() {
        _outline = GetComponent<Outline>();
        _outline.OutlineColor= new Color32(255,251,221,255);
        _min = outlineMin;
        _max = outlineMax;
        Credit.SetActive(false);
    }

    void Update() { UpdateOutline(); }

    private void UpdateOutline( ) {
        if (_outline.OutlineWidth <=_max && _glowUp == true) { _outline.OutlineWidth += Time.deltaTime; }
        if(_outline.OutlineWidth >= _max) { _glowUp = false; }
        if (_outline.OutlineWidth >= _min && _glowUp == false){ _outline.OutlineWidth -= Time.deltaTime; }
        if(_outline.OutlineWidth <= _min) { _glowUp = true; }
    }

    private void OnMouseEnter() {
        Debug.Log("HOVERING");
        _min = outlineHoverMin;
        _max = outlineHoverMax;
        _outline.OutlineWidth = _min;
        _outline.OutlineColor = new Color32(135,142,220,225);
        Credit.SetActive(true);
    }

    private void OnMouseExit() {
        _min = outlineMin;
        _max = outlineMax;
        _outline.OutlineWidth = _max;
        _outline.OutlineColor = new Color32(255, 251, 221, 255);
        Credit.SetActive(false);
    }
}
