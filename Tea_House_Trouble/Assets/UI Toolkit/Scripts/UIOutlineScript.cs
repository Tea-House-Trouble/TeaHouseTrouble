using UnityEngine;
using TMPro;

public class UIOutlineScript : MonoBehaviour
{
    public string HoverTooltip;
    private GameObject Tooltip;
    private TMP_Text _tooltipText;
    private Outline _outline; 
    private float _min, _max;

    public float outlineMin = 1f;
    public float outlineMax = 2f;
    public float outlineHoverMin = 4f;
    public float outlineHoverMax = 5f;

    private AudioSource _audioSource;
    public AudioClip click, hover;

    private bool _glowUp = true;
    public bool _hasBeenClicked = false;

    private bool _isTeakettle = false;
    private GameObject _startGameTooltip = null;
    private Vector3 _startGameTooltipPosition;

    void Awake() {
        if(transform.name == "SM_Teakettle") { 
            _isTeakettle = true;
            _startGameTooltip = GameObject.Find("StartGameTooltip");
            _startGameTooltip.SetActive(true);
            _startGameTooltipPosition = _startGameTooltip.transform.position;
        }

        Tooltip = GameObject.Find("Tooltip");
        _tooltipText = GameObject.Find("TooltipText").GetComponent<TMP_Text>();

        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
        _outline = GetComponent<Outline>();
        _outline.OutlineColor= new Color32(255,251,221,255);
        _min = outlineMin;
        _max = outlineMax;    
    }

    private void Start() {        Tooltip.SetActive(false);         }

    void Update()  { 
        if(_hasBeenClicked == true) { _outline.OutlineWidth = 0; }
        else {UpdateOutline(); }
        
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape)) {  _hasBeenClicked = false; }

        if(_audioSource.loop == true) { Tooltip.transform.position = Input.mousePosition + new Vector3(5,5,0); }
        if(_isTeakettle == true) { _startGameTooltip.transform.position = new Vector3(_startGameTooltipPosition.x, (_startGameTooltipPosition.y + (_outline.OutlineWidth/25)) ,_startGameTooltipPosition.z); }
    }

    private void UpdateOutline( ) {
        if (_outline.OutlineWidth <=_max && _glowUp == true) { _outline.OutlineWidth += Time.deltaTime; }
        if(_outline.OutlineWidth >= _max) { _glowUp = false; }
        if (_outline.OutlineWidth >= _min && _glowUp == false){ _outline.OutlineWidth -= Time.deltaTime; }
        if(_outline.OutlineWidth <= _min) { _glowUp = true; }
    }

    private void OnMouseDown() {
        if(_hasBeenClicked == false) {
            _hasBeenClicked = true;        
            HoverEnd();
            _audioSource.PlayOneShot(click);
        }
    }

    private void OnMouseEnter() {
        if (_isTeakettle == true) { 
            _startGameTooltip.SetActive(false);
            _isTeakettle = false;
        }
        if (_hasBeenClicked == false) { HoverStart(); }    
    }

    private void OnMouseExit() { if (_hasBeenClicked == false) { HoverEnd(); } }

    private void HoverStart() {         
        Debug.Log("HOVERING" + transform.name);
        if (HoverTooltip == null) { _tooltipText.text = transform.name; }
        else { _tooltipText.text = HoverTooltip; }
        Tooltip.SetActive(true);

        _audioSource.clip = hover;
        _audioSource.Play();
        _audioSource.loop = true;
        _min = outlineHoverMin;
        _max = outlineHoverMax;
        _outline.OutlineWidth = _min;
        _outline.OutlineColor = new Color32(135,142,220,225);
    }

    private void HoverEnd() {
        Tooltip.SetActive(false);
        _audioSource.Stop();
        _audioSource.loop = false;
        _min = outlineMin;
        _max = outlineMax;
        _outline.OutlineWidth = _max;
        _outline.OutlineColor = new Color32(255, 251, 221, 255);        
    }

}
