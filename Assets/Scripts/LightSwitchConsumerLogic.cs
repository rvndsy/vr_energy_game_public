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

    protected override void TurnOff() {
        base.TurnOff();
        RotateHingedSwitchToAngle(false);
        lightContainer.SetActive(false);
    }

    protected override void TurnOn(float pwrLvl = 1) {
        base.TurnOn();
        RotateHingedSwitchToAngle(true);
        lightContainer.SetActive(true);
    }

    private void RotateHingedSwitchToAngle(bool state) {
        if (state) hingedSwitch.transform.rotation = Quaternion.Euler(angleOn, defaultRotation.eulerAngles.y, defaultRotation.eulerAngles.z);
        else hingedSwitch.transform.rotation = Quaternion.Euler(angleOff, defaultRotation.eulerAngles.y, defaultRotation.eulerAngles.z);
    }

    private void OnHingedSwitchButtonClick() {
        if (isTurnedOn) TurnOff();
        else TurnOn();
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
        TurnOn();
    }
}