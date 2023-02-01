using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

/*
 Breakdown
   1.MainCam starts as baseCam
   2.Rightclick sets back to baseCam & deactivates UIElements
   3.Leftclick on determined objects changes VirtualCam priorities & activates set UI Elements
 */
public class CameraTransitionManager : MonoBehaviour {

    public CinemachineVirtualCamera baseCam, teapotCam,menuCam,instrumentCam, doorCam;

    private CinemachineVirtualCamera currentCam = null;
    private CinemachineVirtualCamera targetCam = null;

    public void Start() {
        baseCam = GameObject.Find("BaseCam").GetComponent<CinemachineVirtualCamera>();
        teapotCam = GameObject.Find("TeapotStartCam").GetComponent<CinemachineVirtualCamera>();
        menuCam = GameObject.Find("MenuHighscoreCam").GetComponent<CinemachineVirtualCamera>();
        instrumentCam = GameObject.Find("InstrumentOptionsCam").GetComponent<CinemachineVirtualCamera>();
        doorCam = GameObject.Find("DoorExitCam").GetComponent<CinemachineVirtualCamera>();

        currentCam = baseCam; 
    }

    public void CheckHit() {
        Debug.Log("CLICK");
        RaycastHit hit;
        Ray ray = this.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000f) && (hit.transform.CompareTag("UIButton"))) {
            switch (hit.transform.name) {
                case "SM_Teakettle":
                    targetCam = teapotCam;
                    break;

                case "SM_Menu_Card":
                    targetCam = menuCam;
                    break;
                case "MenuScoreTarget":
                    targetCam = menuCam;
                    break;

                case "SM_Shamisen":
                    targetCam = instrumentCam;
                    break;
                case "InstrumentOptionsTarget":
                    targetCam = instrumentCam;
                    break;

                case "SM_ExitDoor":
                    targetCam = doorCam;
                    break;
            }
            SwitchCameraOnClick(targetCam);
        }
    }

    public void SwitchCameraOnClick(CinemachineVirtualCamera target) {
         currentCam.Priority = 0;
         currentCam.GetComponent<Collider>().enabled = false;
         currentCam = target;
         currentCam.Priority++;
         currentCam.GetComponent<Collider>().enabled=true;
    }

    public void BackToBase() {
        teapotCam.Priority = 0;
        menuCam.Priority = 0;
        instrumentCam.Priority = 0;
        doorCam.Priority = 0;
        teapotCam.GetComponent<Collider>().enabled = false;
        menuCam.GetComponent<Collider>().enabled = false;
        instrumentCam.GetComponent<Collider>().enabled = false;
        doorCam.GetComponent<Collider>().enabled = false;
        baseCam.Priority = 1;
        baseCam.GetComponent<Collider>().enabled = true;
    }
}