using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;

public class LightSwitchConsumerLogic : EnergyConsumer {
    [Header("")]
    [SerializeField] private GameObject hingedSwitchColliderGameObject;
    [SerializeField] private GameObject lightSourcesContainer;
    private Collider hingedSwitchCollider;

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
        if (hingedSwitchColliderGameObject != null) {
            hingedSwitchCollider = hingedSwitchColliderGameObject.GetComponent<Collider>();
        } else {
            Collider[] hingedSwitchList = gameObject.GetComponentsInChildren<Collider>();
            if (hingedSwitchList != null) hingedSwitchCollider = hingedSwitchList[0];
        }

        if (hingedSwitchCollider == null) Debug.LogWarning($"{gameObject.name} - LightSwitchConsumerLogic: No hingedSwitch added!");
        else hingedSwitchCollider.onPress.AddListener(OnHingedSwitchButtonClick);
    }

    void Start() {
        if (hingedSwitchCollider != null) {
            TurnOn();
            hingedSwitchCollider.SetState(true);
        } else {
            Debug.LogWarning($"{gameObject.name} - LightSwitchConsumerLogic: No hingedSwitch added!");
        }
    }
}