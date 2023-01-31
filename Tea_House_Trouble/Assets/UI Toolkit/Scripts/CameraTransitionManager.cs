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

    public Button Stay,NotYet;

    private CinemachineVirtualCamera currentCam;
    private CinemachineVirtualCamera targetCam = null;

     void Start() {
        currentCam = baseCam;
        Button stayButton = Stay.GetComponent<Button>();
        stayButton.onClick.AddListener(TaskOnClickStay);

        Button notYetButton = NotYet.GetComponent<Button>();
        notYetButton.onClick.AddListener(TaskOnClickStay);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) { CheckHit(); }
        if (Input.GetMouseButtonDown(1)) { BackToBase(); }
        if (Input.GetKeyDown(KeyCode.Escape)) { BackToBase(); }
    }

    void CheckHit() {
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
    void SwitchCameraOnClick(CinemachineVirtualCamera target) {
         currentCam.Priority = 0;
         currentCam.GetComponent<Collider>().enabled = false;
         currentCam = target;
         currentCam.Priority++;
         currentCam.GetComponent<Collider>().enabled=true;
    }

    void BackToBase() {
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
    void TaskOnClickStay() { SwitchCameraOnClick(baseCam); }
}