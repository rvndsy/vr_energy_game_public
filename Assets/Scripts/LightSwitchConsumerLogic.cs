using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class LightSwitchConsumerLogic : EnergyConsumer {
    [Header("Required")]
    [SerializeField] GameObject lightSource;
    [SerializeField] Button hingedSwitchButton;
    [SerializeField] GameObject hingedSwitch;
    [Header("")]
    [SerializeField] float angleOn = 7f;
    [SerializeField] float angleOff = -7f;

/*    void TurnOn() {
        isTurnedOn = true;
        lightSource.SetActive(true);
        SetSwitchState(true);
    }*/

/*    void TurnOff() {
        isTurnedOn = false;
        lightSource.SetActive(false);
        SetSwitchState(false);
    }*/

    private void SetSwitchState(bool newState) {
        if (newState) {
            TurnOn();
            hingedSwitch.transform.rotation = Quaternion.Euler(angleOn, 0f, 0f);
        }
        else {
            TurnOff();
            hingedSwitch.transform.rotation = Quaternion.Euler(angleOff, 0f, 0f);
        }
        lightSource.SetActive(newState);
    }

    private void OnHingedSwitchButtonClick() {
        SetSwitchState(!isTurnedOn);
        Debug.Log("LightSwitch: Button pressed!");
    }

    /*private bool ReadSwitchRotationState() {
        if (hingedSwitch.transform.rotation.eulerAngles.x >= angleOn) return true;
        return false;
    }*/

    void Start() {
        hingedSwitchButton.onClick.AddListener(OnHingedSwitchButtonClick);
        SetSwitchState(false);
    }

    void Update() {
        // if (!isTurnedOn && ReadSwitchRotationState()) TurnOn();
        // else if (isTurnedOn && !ReadSwitchRotationState()) TurnOff();
        // int currentTime = (int)Time.time;
        // if (currentTime % 2 == 1 && state) setSwitchState(false);
        // else if (currentTime % 2 == 0) setSwitchState(true);
        /*Debug.Log("Lightswitch: isTurnedOn set to " + isTurnedOn);*/
    }
}
