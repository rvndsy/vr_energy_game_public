using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImprovedLightSwitchConsumerLogic : EnergyConsumer {
/*    [Header("")]
    [SerializeField] private float angleOn = 7f;
    [SerializeField] private float angleOff = -7f;
    [SerializeField] private GameObject hingedSwitch;
    [SerializeField] private Collider hingedSwitchCollider;
    [SerializeField] private GameObject lightContainer;

    private Quaternion defaultRotation;

    protected override void TurnOff() {
        DeactivateButtonForTime();
        base.TurnOff();
        RotateHingedSwitchToAngle(false);
        lightContainer.SetActive(false);
    }

    protected override void TurnOn(float pwrLvl = 1) {
        DeactivateButtonForTime();
        base.TurnOn();
        RotateHingedSwitchToAngle(true);
        lightContainer.SetActive(true);
    }

    private void RotateHingedSwitchToAngle(bool state) {
        if (state) hingedSwitch.transform.rotation = Quaternion.Euler(angleOn, defaultRotation.eulerAngles.y, defaultRotation.eulerAngles.z);
        else hingedSwitch.transform.rotation = Quaternion.Euler(angleOff, defaultRotation.eulerAngles.y, defaultRotation.eulerAngles.z);
    }

    private void OnHingedSwitchButtonClick(Collider collider) {
        if (collider.CompareTag("Player")) {
            if (isTurnedOn) TurnOff();
            else TurnOn();
            Debug.LogWarning($"{gameObject.name} - LightSwitch: Button pressed!");
        }
        Debug.LogWarning($"{gameObject.name} - LightSwitch: Button pressed BY THE WRONG OBJECT!");
    }

*//*    private IEnumerator DeactivateButtonForTime(float time = 0.2f) {
        if (hingedSwitchButton.isActiveAndEnabled) yield break;

        hingedSwitchButton.enabled = false;

        yield return new WaitForSeconds(time);

        hingedSwitchButton.enabled = true;
    }*//*

    private void Awake() {
        hingedSwitchCollider = hingedSwitch.GetComponent<Collider>();
        if (hingedSwitchCollider != null) {
            hingedSwitchButton.onClick.AddListener(OnHingedSwitchButtonClick(hingedSwitchCollider));
        }
        else {
            Debug.Log($"{gameObject.name} - LightSwitch: No collider component found!");
        }

        defaultRotation = hingedSwitch.transform.rotation;
    }

    void Start() {
        TurnOn();
    }*/
}