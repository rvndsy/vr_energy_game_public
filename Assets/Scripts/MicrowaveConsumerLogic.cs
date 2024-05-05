using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MicrowaveConsumerLogic : EnergyConsumer {

    [Header("Required Microwave Attributes")]
    [SerializeField] private int maxTimeInSeconds = 120;
    [SerializeField] private int timerInterval = 10;
    [Header("Other Microwave Attributes")]
    [SerializeField] private AudioSource turnedOnAudio;
    [SerializeField] private AudioSource finishedAudio;
    private Button addButton, subtractButton, goButton;
    private TextMeshProUGUI timerDisplayText;
    private int minTimeInSeconds = 0;
    private int timerTimeInSeconds = 0;
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
    // [?] Door locks and snaps into place?

    private void UpdateTimerDisplay() {
        int minutes = timerTimeInSeconds / 60;
        int seconds = timerTimeInSeconds % 60;
        timerDisplayText.text = $"{minutes:00}:{seconds:00}";
    }

    private void ResetTimer() {
        isTimerTicking = false;
        timerTimeInSeconds = 0;
        timerDisplayText.text = "00:00";
        Debug.Log($"{gameObject.name} - Microwave: Subtract Button pressed!");
    }

    private void OnSubtractButtonClick() {
        Debug.Log($"{gameObject.name} - Microwave: Subtract Button pressed!");
        if (isTimerTicking) return;
        int newTimerValue = timerTimeInSeconds - timerInterval;
        if (newTimerValue >= minTimeInSeconds) {
            timerTimeInSeconds = newTimerValue;
            UpdateTimerDisplay();
        }
    }

    private void OnAddButtonClick() {
        Debug.Log($"{gameObject.name} - Microwave: Add Button pressed!");
        if (isTimerTicking) return;
        int newTimerValue = timerTimeInSeconds + timerInterval;
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

    private IEnumerator StartTickTimerDown() { // coroutine
        if (turnedOnAudio.loop == false) Debug.Log($"{gameObject.name} - Microwave: Microwave turned on audio is not looped!");
        turnedOnAudio.Play();

        TurnToMax();

        while (timerTimeInSeconds > minTimeInSeconds && isTimerTicking) {
            timerTimeInSeconds--;
            UpdateTimerDisplay();
            Debug.Log("Microwave: Timer says - " + timerDisplayText.text);
            yield return new WaitForSeconds(1f); //every 1 second
        }

        turnedOnAudio.Stop();
        finishedAudio.Play(0);

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
        timerDisplayText.SetText("00:00");
    }
}
