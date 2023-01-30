using UnityEngine;
using TMPro;


public class CreditHover : MonoBehaviour
{
    public GameObject Credit;

    void Awake() {   Credit.SetActive(false);  }

    private void OnMouseEnter() {       Credit.SetActive(true);   }

    private void OnMouseExit() {        Credit.SetActive(false);    }
}
