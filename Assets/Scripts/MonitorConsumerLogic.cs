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

    protected override void TurnToIdle() {
        base.TurnToIdle();
        displayGameObject.SetActive(false);
    }

    private void OnTurnOnButtonClick() {
        if (powerLevel == 1) TurnToIdle();
        else TurnOn();
        // Debug.Log($"{gameObject.name} - MonitorConsumerLogic: TurnOnButton clicked!");
    }

    private void Awake() {
        Button[] buttonList = gameObject.GetComponentsInChildren<Button>();
        if (buttonList == null) Debug.LogWarning($"{gameObject.name} - MonitorConsumerLogic: No buttons found in children!");
        foreach (Button button in buttonList) {
            if (button.name == "TurnOnButton") {
                turnOnButton = button;
                // Debug.Log($"{gameObject.name} - MonitorConsumerLogic: turnOnButton added");
            }
        }
        if (turnOnButton == null) Debug.LogWarning($"{gameObject.name} - MonitorConsumerLogic: turnOnButton was not added!");

        turnOnButton.onClick.AddListener(OnTurnOnButtonClick);
    }

    void Start() {
        if (isTurnedOn) TurnOn();
        else            TurnToIdle();
    }
}