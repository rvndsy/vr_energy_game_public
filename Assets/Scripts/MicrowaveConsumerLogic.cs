using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MicrowaveConsumerLogic : EnergyConsumer {

    [Header("Required Microwave Attributes")]
    [SerializeField] Button addButton;
    [SerializeField] Button subtractButton;
    [SerializeField] Button goButton;
    [SerializeField] TMP_Text timerDisplayText;
    [SerializeField] int maxTimeInSeconds = 120;
    [SerializeField] int timerInterval = 10;
    [Header("Other Microwave Attributes")]
    [SerializeField] AudioSource turnedOnAudio;
    [SerializeField] AudioSource finishedAudio;
    int minTimeInSeconds = 0;
    int timerTimeInSeconds = 0;
    bool isTimerTicking = false;

    // REMOVE after making the inheritance work
    // Energy consumption specific values:
    // public bool isTurnedOn = true;
    // public float powerLevel = 0; // keep this value between 0 and 1. value of 1 indicates that maxPowerConsumption == powerConsumption

    // TODO:
    // [?] Cannot execute Go whilst the door is open
    // [-] (optional) Light inside the microwave
    // [+] Sound effect
    // [] Tactile feedback?
    // [] Press button physically?
    // [?] Door locks and snaps into place?

    void UpdateTimerDisplay() {
        int minutes = timerTimeInSeconds / 60;
        int seconds = timerTimeInSeconds % 60;
        timerDisplayText.text = $"{minutes:00}:{seconds:00}";
    }

    void ResetTimer() {
        isTimerTicking = false;
        timerTimeInSeconds = 0;
        timerDisplayText.text = "00:00";
    }


    /*void TurnOff() {
        isTurnedOn = false;
        timerDisplayText.text = "";
    }*/

/*    void TurnOn() {
        isTurnedOn = true;
        ResetTimer();
    }*/

    void OnSubtractButtonPress() {
        Debug.Log("Microwave: Subtract Button pressed!");
        if (isTimerTicking) return;
        int newTimerValue = timerTimeInSeconds - timerInterval;
        if (newTimerValue >= minTimeInSeconds) {
            timerTimeInSeconds = newTimerValue;
            UpdateTimerDisplay();
        }
    }

    void OnAddButtonPress() {
        Debug.Log("Microwave: Add Button pressed!");
        if (isTimerTicking) return;
        int newTimerValue = timerTimeInSeconds + timerInterval;
        if (newTimerValue <= maxTimeInSeconds) {
            timerTimeInSeconds = newTimerValue;
            UpdateTimerDisplay();
        }
    }

    void OnGoButtonPress() {
        Debug.Log("Microwave: Go Button pressed!");
        if (!isTimerTicking) {
            isTimerTicking = true;
            StartCoroutine(StartTickTimerDown());
        }
    }

    IEnumerator StartTickTimerDown() { // coroutine
        if (turnedOnAudio.loop == false) Debug.Log("(Warning) Microwave: Microwave turned on audio is not looped!");
        turnedOnAudio.Play();

        SetPowerLevel(1);

        while (timerTimeInSeconds > minTimeInSeconds && isTimerTicking) {
            timerTimeInSeconds--;
            UpdateTimerDisplay();
            Debug.Log("Microwave: Timer says - " + timerDisplayText.text);
            yield return new WaitForSeconds(1f); //every 1 second
        }

        turnedOnAudio.Stop();
        finishedAudio.Play(0);

        SetPowerLevel(0);

        ResetTimer();
    }

    void Start() {
        timerDisplayText.SetText("00:00");

        Button[] buttonList = GetComponents<Button>();
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

        // button event listeners
        addButton.onClick.AddListener(OnAddButtonPress);
        subtractButton.onClick.AddListener(OnSubtractButtonPress);
        goButton.onClick.AddListener(OnGoButtonPress);
    }
}
