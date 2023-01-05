using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Camera camera;
    public GameObject teapot;
    public GameObject menuCard;
    public GameObject instrument;
    public GameObject door;

    public float speed = 5.0f;

    //New Camera Positions
    public Transform _baseP;
    public Transform _teapotP;
    public Transform _menuCardP;
    public Transform _instrumentP;
    public Transform _doorP;

    private bool isMoving = false;
    private Transform newPos = null;

     private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            CheckHit();
        }

        if (Input.GetMouseButtonDown(1)) {
            newPos = _baseP;
            isMoving = true;
        }

        if (isMoving == true) {
            MoveToUIPosition(newPos);
        }
    }

    void CheckHit() {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit,1000) && (hit.transform.CompareTag("UIButton"))) 
            {
                switch(hit.transform.name) 
                {
                case "SM_Teapot":
                    newPos = _teapotP;
                    break;

                case "SM_Menucard":
                    newPos = _menuCardP;
                    break;

                case "SM_Instrument":
                    newPos = _instrumentP;
                    break;

                case "SM_Door":
                    newPos = _doorP;
                    break;
                }
            isMoving = true;
            }
    }

    private void MoveToUIPosition(Transform newPos) {
        transform.position = Vector3.MoveTowards(transform.position, newPos.position, speed * Time.deltaTime);   //Lerp(transform.position, newPos.position, Time.deltaTime);
        if (Vector3.Distance(transform.position, newPos.position) <= 1.0f) { isMoving = false; }
        
        /*
        switch (_hitname) {

            case "SM_Teapot":
                transform.position = Vector3.Lerp(transform.position, _teapotP.position, Time.deltaTime);//Vector3.MoveTowards(transform.position, _teapotP.position, speed * Time.deltaTime);  //SmoothDamp(transform.position, _teapotP.position, ref velocity, smoothTime, speed);
                if (Vector3.Distance(transform.position, _teapotP.position) <= 1.0f) { isMoving = false; }
                break;            
            
            case "SM_Menucard":
                transform.position = Vector3.SmoothDamp(transform.position, _menuCardP.position, ref velocity, smoothTime, speed);
                break;            
            
            case "SM_Instrument":
                transform.position = Vector3.SmoothDamp(transform.position, _instrumentP.position, ref velocity, smoothTime, speed);
                break;            
            
            case "SM_Door":
                transform.position = Vector3.SmoothDamp(transform.position, _doorP.position, ref velocity, smoothTime, speed);
                break;
        }
         */
    }

}
