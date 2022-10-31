using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonTriggerZone : MonoBehaviour
{
    
    public PlayerControlls Controlls;
    private PlayerInput playerInput;

    private void Awake()
    {
        Controlls = new PlayerControlls();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < PlayerAutoRun.PlayerTransform.position.x)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        Controlls.Enable();
    }

    private void OnDisable()
    {
        Controlls.Disable();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            Debug.Log("Enter Collider");
            //Destroy(collision.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Destroy(other.gameObject);
            Debug.Log("Enter Triggerzone");

            //if (Controlls.Actions.Up.ReadValue<int>() > 1)
            //{
            //    Destroy(other.gameObject);
            //    Debug.Log("Kill Note");
            //}
            
        }
    }
}
