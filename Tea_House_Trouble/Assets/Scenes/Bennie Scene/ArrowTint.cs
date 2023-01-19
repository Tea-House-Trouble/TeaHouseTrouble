using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTint : MonoBehaviour
{

    private Material material;
    private Color arrowTintColor;
    private float tintFadeSpeed;


    // Start is called before the first frame update
    private void Start()
    {
        arrowTintColor = new Color(1, 0, 0, 0);
        SetMaterial(transform.Find("TestTrigger (1)").GetComponent<MeshRenderer>().material);
        tintFadeSpeed = 6f;
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (arrowTintColor.a > 0) { 
            arrowTintColor.a = Mathf.Clamp01(arrowTintColor.a - tintFadeSpeed * Time.deltaTime);
            material.SetColor("_Left_Arrow_Color", arrowTintColor);
        }
    }

    public void SetMaterial(Material material)
    {
        this.material = material;
    }

    public void SetTintColor(Color color)
    {
        arrowTintColor = color;
        material.SetColor("_Left_Arrow_Color", arrowTintColor);
    }

    public void SetTintFadeSpeed(float tintFadeSpeed)
    {
        this.tintFadeSpeed = tintFadeSpeed;
    }
}
