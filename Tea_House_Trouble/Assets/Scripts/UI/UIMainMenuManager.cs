using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Breakdown
   Teapot / StartLevel Menu 
        -> ("Play?") 
        -> Choose Controls (DanceMat, Keyboard, Controler, Touch)
        -> Choose Difficulty (Easy, Medium, Hard, Ultra)

    MenuCard / Match History / Highscores
        ->List (Place, Name, Score, Time, Accuracy , Difficulty)

    Instrument / Options Menu
        -> Choose (Gameplay / Controls , Audio, Visual)
            ->Gameplay /Controls (Current Controls, Rebind)
            ->Audio (Music Volume(Slider), Musin in MainMenu(Slider), VFX Volume(Slider), Total Volume(Slider))
            ->Visual (Brightness(Slider), Windowed / Fullscreen(Toggle))

    Door / Leave Game:
        ->Exit? (Stay, Leave)

*/
public class UIMainMenuManager : MonoBehaviour
{
    public GameObject StartMenu,ScoreMenu,OptionMenu,ExitMenu;

    private bool isMoving = false;

    private void Start() { BaseDeactivate(); }

    private void Activate(GameObject SetMenu) { SetMenu.SetActive(true); }

    private void Deactivate(GameObject SetMenu) { SetMenu.SetActive(false); }

    private void BaseDeactivate() {
        StartMenu.SetActive(false);
        ScoreMenu.SetActive(false);
        OptionMenu.SetActive(false);
        ExitMenu.SetActive(false);
    }

    private void MovementCheck() {
        if(this.GetComponent<Rigidbody>().velocity.magnitude != 0f) { isMoving = true; }
        else { isMoving = false; }
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.CompareTag("VirtualCam") && isMoving==false) {
            MovementCheck();
            switch (collision.name) {
                case "BaseCam":
                    BaseDeactivate();
                    break;

                case "TeapotStartCam":
                    Activate(StartMenu);
                    break;

                case "MenuHighscoreCam":
                    Activate(ScoreMenu);
                    break;

                case "InstrumentOptionsCam":
                    Activate(OptionMenu);
                    break;

                case "DoorExitCam":
                    Activate(ExitMenu);
                    break;
            }
        }
    }
}
