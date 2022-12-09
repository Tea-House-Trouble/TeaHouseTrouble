using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

/*
 Breakdown
   1.MainCam starts as baseCam
   2.Rightclick sets back to baseCam & deactivates UIElements
   3.Leftclick on determined objects changes VirtualCam priorities & activates set UI Elements
 */
public class CameraTransitionManager : MonoBehaviour {

    public Camera camera;
    public CinemachineVirtualCamera baseCam;
    public CinemachineVirtualCamera teapotCam;
    public CinemachineVirtualCamera menuCam;
    public CinemachineVirtualCamera instrumentCam;
    public CinemachineVirtualCamera doorCam;

    private CinemachineVirtualCamera currentCam;
    private CinemachineVirtualCamera targetCam = null;
    private bool isMoving = false;


    private void Start() {
        currentCam = baseCam;
        currentCam.Priority++;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) { CheckHit(); }
        if (Input.GetMouseButtonDown(1)) { SwitchCameraOnClick(baseCam); }
    }

    void CheckHit() {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000) && (hit.transform.CompareTag("UIButton"))) {
            switch (hit.transform.name) {
                case "SM_Teapot":
                    targetCam = teapotCam;
                    break;

                case "SM_Menucard":
                    targetCam = menuCam;
                    break;

                case "SM_Instrument":
                    targetCam = instrumentCam;
                    break;

                case "SM_Door":
                    targetCam = doorCam;
                    break;
            }
            SwitchCameraOnClick(targetCam);
        }
    }
    void SwitchCameraOnClick(CinemachineVirtualCamera target) {
         currentCam.Priority--;
         currentCam = target;
         currentCam.Priority++;
    }
}