using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOutlineScript : MonoBehaviour
{
    private Outline _outline; 
    private float _min, _max;

    public float outlineMin = 1f;
    public float outlineMax = 2f;
    public float outlineHoverMin = 4f;
    public float outlineHoverMax = 5f;

    private bool _glowUp = true;

    private AudioSource _audioSource;
    public AudioClip click, hover;

    void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
        _outline = GetComponent<Outline>();
        _outline.OutlineColor= new Color32(255,251,221,255);
        _min = outlineMin;
        _max = outlineMax;
    }

    void Update()
    {
        UpdateOutline();
    }

    private void UpdateOutline( ) {
        if (_outline.OutlineWidth <=_max && _glowUp == true) { _outline.OutlineWidth += Time.deltaTime; }
        if(_outline.OutlineWidth >= _max) { _glowUp = false; }
        if (_outline.OutlineWidth >= _min && _glowUp == false){ _outline.OutlineWidth -= Time.deltaTime; }
        if(_outline.OutlineWidth <= _min) { _glowUp = true; }
    }

    private void OnMouseDown() {
        _audioSource.PlayOneShot(click);
    }
    private void OnMouseEnter() {
        _audioSource.clip = hover;
        _audioSource.Play();
        _audioSource.loop = true;
        Debug.Log("HOVERING");
        _min = outlineHoverMin;
        _max = outlineHoverMax;
        _outline.OutlineWidth = _min;
        _outline.OutlineColor = new Color32(135,142,220,225);
    }

    private void OnMouseExit() {
        _audioSource.Stop();
        _audioSource.loop = false;
        _min = outlineMin;
        _max = outlineMax;
        _outline.OutlineWidth = _max;
        _outline.OutlineColor = new Color32(255, 251, 221, 255);
    }
}
