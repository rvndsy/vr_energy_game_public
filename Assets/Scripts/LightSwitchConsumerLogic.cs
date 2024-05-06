using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;

public class LightSwitchConsumerLogic : EnergyConsumer {
    [Header("")]
    [SerializeField] private GameObject hingedSwitchCollider;
    [SerializeField] private GameObject lightSourcesContainer;
    private HingedSwitch hingedSwitch;

    protected override void TurnOff() {
        base.TurnOff();
        lightSourcesContainer.SetActive(false);
    }

    protected override void TurnOn(float pwrLvl = 1) {
        base.TurnOn();
        lightSourcesContainer.SetActive(true);
    }

    private void OnHingedSwitchButtonClick() {
        if (isTurnedOn) TurnOff();
        else TurnOn();
        Debug.Log($"{gameObject.name} - LightSwitch: Button pressed!");
    }

    private void Awake() {
        if (hingedSwitchCollider != null) {
            hingedSwitch = hingedSwitchCollider.GetComponent<HingedSwitch>();
        } else {
            HingedSwitch[] hingedSwitchList = gameObject.GetComponentsInChildren<HingedSwitch>();
            if (hingedSwitchList != null) hingedSwitch = hingedSwitchList[0];
        }

        if (hingedSwitch == null) Debug.LogWarning($"{gameObject.name} - LightSwitchConsumerLogic: No hingedSwitch added!");
        else hingedSwitch.onPress.AddListener(OnHingedSwitchButtonClick);
    }

    void Start() {
        if (hingedSwitch != null) {
            TurnOn();
            hingedSwitch.SetState(true);
        } else {
            Debug.LogWarning($"{gameObject.name} - LightSwitchConsumerLogic: No hingedSwitch added!");
        }
    }
}