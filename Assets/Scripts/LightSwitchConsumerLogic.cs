using UnityEngine;
using UnityEngine.UI;

public class LightSwitchConsumerLogic : EnergyConsumer {
    [Header("")]
    [SerializeField] float angleOn = 7f;
    [SerializeField] float angleOff = -7f;
    [SerializeField] private GameObject hingedSwitch;
    [SerializeField] private GameObject lightContainer;

    private Button hingedSwitchButton;

    private Quaternion defaultRotation;

    private void SetSwitchState(bool state) {
        Debug.Log($"{gameObject.name} - LightSwitch: Changing state to {state}!");

        if (state) Activate();
        else Deactivate();

        RotateHingedSwitchToAngle(state);
        lightContainer.SetActive(state);
    }

    private void RotateHingedSwitchToAngle(bool state) {
        if (state) hingedSwitch.transform.rotation = Quaternion.Euler(angleOn, defaultRotation.eulerAngles.y, defaultRotation.eulerAngles.z);
        else hingedSwitch.transform.rotation = Quaternion.Euler(angleOff, defaultRotation.eulerAngles.y, defaultRotation.eulerAngles.z);
    }

    private void OnHingedSwitchButtonClick() {
        SetSwitchState(!isTurnedOn);
        Debug.Log($"{gameObject.name} - LightSwitch: Button pressed!");
    }

    private void Awake() {
        Button[] buttonList = hingedSwitch.GetComponentsInChildren<Button>();
        if (buttonList == null) Debug.LogError($"{transform.parent.gameObject.name} - LightSwitch: No buttons found in children!");
        foreach (Button button in buttonList) {
            if (button.name == "HingedSwitchButton") {
                hingedSwitchButton = button;
                Debug.Log($"{gameObject.name} - LightSwitch: HingedSwitchButton added");
            }
        }
        if (hingedSwitchButton == null) Debug.LogError($"{gameObject.name} - LightSwitch: HingedSwitchButton was not added!");

        hingedSwitchButton.onClick.AddListener(OnHingedSwitchButtonClick);

        defaultRotation = hingedSwitch.transform.rotation;
    }

    void Start() {
        SetSwitchState(true);
    }
}