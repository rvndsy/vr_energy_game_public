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

    private void SetSwitchState(bool newState) {
        Debug.Log($"{gameObject.name} - LightSwitch: Changing state!");

        if (newState) Activate();
        else Deactivate();

        RotateHingedSwitchToAngle(newState);
        lightContainer.SetActive(newState);
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
        if (buttonList == null) Debug.LogWarning($"{transform.parent.gameObject.name} - LightSwitch: No buttons found in children!");
        foreach (Button button in buttonList) {
            if (button.name == "HingedSwitchButton") {
                hingedSwitchButton = button;
                Debug.Log($"{gameObject.name} - LightSwitch: HingedSwitchButton added");
            }
            Debug.Log($"{gameObject.name} - LightSwitch: {button.name} skipped");
        }
        if (hingedSwitchButton == null) Debug.LogWarning($"{transform.parent.gameObject.name} - LightSwitch: HingedSwitchButton was not added!");

        hingedSwitchButton.onClick.AddListener(OnHingedSwitchButtonClick);

        defaultRotation = hingedSwitch.transform.rotation;
    }

    void Start() {
        SetSwitchState(true);
    }
}