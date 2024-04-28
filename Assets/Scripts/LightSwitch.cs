using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LightSwitch : MonoBehaviour {
    bool state;
    float angleOn = 0f;
    float angleOff = 90f;
    GameObject switchPivot, lightSource;

    void Start() {
        switchPivot = GameObject.Find("SwitchPivot");
        setSwitchState(false);
    }

    void setSwitchState(bool isOn) {
        state = isOn;
        if (isOn) switchPivot.transform.rotation = Quaternion.Euler(angleOn, 0f, 0f);
        else switchPivot.transform.rotation = Quaternion.Euler(angleOff, 0f, 0f);
    }

    void Update() {
        int currentTime = (int)Time.time;
        if (currentTime % 2 == 1 && state) setSwitchState(false);
        else if (currentTime % 2 == 0) setSwitchState(true);
        // Debug.Log("Set to: " +state);
    }
}
