using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MicrowaveConsumerLogic : EnergyConsumer {

    [Header("Required Microwave Attributes")]
    [SerializeField] private int maxTimeInSeconds = 120;
    [SerializeField] private int timerInterval = 10;
    [Header("Other Microwave Attributes")]
    [SerializeField] private AudioSource cookingAudio;
    [SerializeField] private AudioSource finishedAudio;
    private Button addButton, subtractButton, goButton;
    private TextMeshProUGUI timerDisplayText;
    private int minTimeInSeconds = 0;
    private float timerTimeInSeconds = 0;
    private bool isTimerTicking = false;

    // REMOVE after making the inheritance work
    // Energy consumption specific values:
    // public bool isTurnedOn = true;
    // public float powerLevel = 0; // keep this value between 0 and 1. value of 1 indicates that maxPowerConsumption == powerConsumption

    // TODO:
    // [?] Cannot execute Go whilst the door is open
    // [-] (optional) Light inside the microwave
    // [+] Sound effect
    // [] Tactile feedback?
    // [+] Press button physically?

    private void UpdateTimerDisplay() {
        int minutes = (int)(timerTimeInSeconds / 60);
        int seconds = (int)(timerTimeInSeconds % 60);
        timerDisplayText.text = $"{minutes:00}:{seconds:00}";
    }

    private void ResetTimer() {
        isTimerTicking = false;
        timerTimeInSeconds = 0;
        timerDisplayText.text = "00:00";
        Debug.Log($"{gameObject.name} - Microwave: Subtract Button pressed!");
    }

    private void RunTimer() {
        Debug.Log($"{gameObject.name} - Microwave: Timer running  - {timerTimeInSeconds}!");
        timerTimeInSeconds -= (int)(TimeManager.TimeUpdateRefreshRateDecimal * TimeManager.TimeMultiplier);
        UpdateTimerDisplay();
    }

    private void OnSubtractButtonClick() {
        Debug.Log($"{gameObject.name} - Microwave: Subtract Button pressed!");
        if (isTimerTicking) return;
        float newTimerValue = timerTimeInSeconds - timerInterval;
        if (newTimerValue >= minTimeInSeconds) {
            timerTimeInSeconds = newTimerValue;
            UpdateTimerDisplay();
        }
    }

    private void OnAddButtonClick() {
        Debug.Log($"{gameObject.name} - Microwave: Add Button pressed!");
        if (isTimerTicking) return;
        float newTimerValue = timerTimeInSeconds + timerInterval;
        if (newTimerValue <= maxTimeInSeconds) {
            timerTimeInSeconds = newTimerValue;
            UpdateTimerDisplay();
        }
    }

    private void OnGoButtonClick() {
        Debug.Log($"{gameObject.name} - Microwave: Go Button pressed!");
        if (!isTimerTicking) {
            isTimerTicking = true;
            StartCoroutine(StartTickTimerDown());
        }
    }

    private IEnumerator StartTickTimerDown() {
        if (cookingAudio.loop == false) Debug.Log($"{gameObject.name} - Microwave: Microwave turned on audio is not looped!");
        if (cookingAudio != null) cookingAudio.Play();

        Debug.Log($"{gameObject.name} - Microwave: StartTickTimerDown started!");

        TurnToMax();

        TimeManager.onTimerTick.AddListener(RunTimer);
        while (timerTimeInSeconds > minTimeInSeconds) { 
            yield return null;
        }
        TimeManager.onTimerTick.RemoveListener(RunTimer);

        if (cookingAudio != null) cookingAudio.Stop();
        if (finishedAudio != null) finishedAudio.Play();

        TurnToIdle();

        ResetTimer();
    }

    void Awake() {
        Button[] buttonList = gameObject.GetComponentsInChildren<Button>();
        if (buttonList == null) Debug.LogError($"{gameObject.name} - Microwave: No buttons found in children!");
        foreach (Button button in buttonList) {
            switch (button.name) {
                case "AddButton":
                    addButton = button;
                    break;
                case "SubtractButton":
                    subtractButton = button;
                    break;
                case "GoButton":
                    goButton = button;
                    break;
            }
        }
        TextMeshProUGUI[] textMeshProList = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI obj in textMeshProList) {
            if (obj.name == "Display") {
                timerDisplayText = obj;
                break;
            }
        }
        if (timerDisplayText == null) Debug.LogError("Microwave: 'Display' component not found!");

        if (addButton == null || subtractButton == null || goButton == null) {
            Debug.LogError("Microwave: Buttons misconfigured!");
        }

        addButton.onClick.AddListener(OnAddButtonClick);
        subtractButton.onClick.AddListener(OnSubtractButtonClick);
        goButton.onClick.AddListener(OnGoButtonClick);
    }

    void Start() {
        TurnToIdle();
        timerDisplayText.SetText("00:00");
    }
}
