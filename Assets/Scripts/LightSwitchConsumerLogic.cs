using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LightSwitchConsumerLogic : EnergyConsumer {
    [Header("")]
    [SerializeField] float angleOn = 7f;
    [SerializeField] float angleOff = -7f;
    GameObject lightSource;
    Button hingedSwitchButton;
    GameObject hingedSwitch;

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

    private void Awake() {
        Button[] buttonList = gameObject.GetComponentsInChildren<Button>();
        foreach (Button button in buttonList) {
            if (button.name == "HingedSwitchButton") hingedSwitchButton = button;
        }
        GameObject[] gameObjList = gameObject.GetComponentsInChildren<GameObject>();
        foreach (GameObject obj in gameObjList) {
            switch (obj.name) {
                case "Point Light":
                    lightSource = obj;
                    break;
                case "HingedSwitch":
                    hingedSwitch = obj;
                    break;
            }
        }
        hingedSwitchButton.onClick.AddListener(OnHingedSwitchButtonClick);
    }
    void Start() {
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
