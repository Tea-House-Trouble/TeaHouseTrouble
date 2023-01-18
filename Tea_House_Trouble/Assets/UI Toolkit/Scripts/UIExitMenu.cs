using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIExitMenu : MonoBehaviour
{
    public Button Stay;
    public Button Leave;

    // Start is called before the first frame update
    void Start() {
        Button leaveButton = Leave.GetComponent<Button>();
        leaveButton.onClick.AddListener(TaskOnClickLeave);
    }

    void TaskOnClickLeave() {
        Application.Quit();
    }

}
