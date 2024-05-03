using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class LightSwitchConsumerLogic : EnergyConsumer {
    [Header("")]
    [SerializeField] float angleOn = 7f;
    [SerializeField] float angleOff = -7f;
    [SerializeField] private GameObject hingedSwitch;
    [SerializeField] private GameObject lightContainer;

    private Button hingedSwitchButton;

    private void SetSwitchState(bool newState) {
        if (newState) Activate();
        else Deactivate();

        RotateHingedSwitchToState(newState);
        lightContainer.SetActive(newState);
    }

    private void RotateHingedSwitchToState(bool state) {
        if (state) hingedSwitch.transform.rotation = Quaternion.Euler(angleOn, 0f, 0f);
        else hingedSwitch.transform.rotation = Quaternion.Euler(angleOff, 0f, 0f);
    }

    private void OnHingedSwitchButtonClick() {
        SetSwitchState(!isTurnedOn);
        Debug.Log("LightSwitch: Button pressed!");
    }

    private void Awake() {
        Button[] buttonList = gameObject.GetComponentsInChildren<Button>();
        foreach (Button button in buttonList) {
            if (button.name == "HingedSwitchButton") hingedSwitchButton = button;
        }

        hingedSwitchButton.onClick.AddListener(OnHingedSwitchButtonClick);
    }

    void Start() {
        SetSwitchState(false);
    }

    void Update() {
    }
}
