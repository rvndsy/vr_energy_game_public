using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonitorConsumerLogic : EnergyConsumer {
    [Header("")]
    [SerializeField] private Button turnOnButton;
    [SerializeField] private GameObject displayGameObject;

    protected override void TurnOn(float power = 1) {
        base.TurnOn();
        displayGameObject.SetActive(true);
    }

    protected override void TurnOff() {
        base.TurnOff();
        displayGameObject.SetActive(false);
    }

    private void OnTurnOnButtonClick() {
        if (isTurnedOn) TurnOn();
        else TurnOff();
        Debug.LogError($"{gameObject.name} - MonitorConsumerLogic: TurnOnButton clicked!");
    }

    private void Awake() {
        Button[] buttonList = gameObject.GetComponentsInChildren<Button>();
        if (buttonList == null) Debug.LogError($"{transform.parent.gameObject.name} - MonitorConsumerLogic: No buttons found in children!");
        foreach (Button button in buttonList) {
            if (button.name == "TurnOnButton") {
                turnOnButton = button;
                Debug.Log($"{gameObject.name} - MonitorConsumerLogic: turnOnButton added");
            }
        }
        if (turnOnButton == null) Debug.LogError($"{gameObject.name} - MonitorConsumerLogic: turnOnButton was not added!");

        turnOnButton.onClick.AddListener(OnTurnOnButtonClick);
    }

    void Start() {
        TurnOff();
    }
}