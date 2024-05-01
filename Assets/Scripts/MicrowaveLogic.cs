using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MicrowaveLogic : MonoBehaviour
{
    [Header("Required Microwave Attributes")]
    [SerializeField] Button addButton;
    [SerializeField] Button subtractButton;
    [SerializeField] Button goButton;
    [SerializeField] TextMeshProUGUI timerDisplayText;
    [SerializeField] int maxTimeInSeconds = 120;
    [SerializeField] int timerInterval = 10;
    [Header("Other Microwave Attributes")]
    [SerializeField] AudioSource turnedOnAudio;
    [SerializeField] AudioSource finishedAudio;
    int minTimeInSeconds = 0;
    int timerTimeInSeconds = 0;
    bool isTimerTicking = false;

    // Energy consumption specific values:
    bool isTurnedOn = true;
    float powerLevel = 0; // keep this value between 0 and 1. value of 1 indicates that maxPowerConsumption == powerConsumption;
    float maxPowerConsumption = 0;
    float passiveEnergyConsumption = 0; // used when isTurnedOn == true && powerLevel == 0
    float powerConsumption = 0; // this is the actual consumption in Watts (W)

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
        // CancelInvoke("TickTimerDown");
    }

    void turnOff() {
        isTurnedOn = false;
        timerDisplayText.text = "";
    }

    void turnOn() {
        isTurnedOn = true;
        ResetTimer();
    }

    void OnSubtractButtonPress(int value) {
        if (isTimerTicking) return;
        Debug.Log("Microwave: Subtract Button pressed!");
        int newTimerValue = timerTimeInSeconds - timerInterval;
        if (newTimerValue >= minTimeInSeconds) {
            timerTimeInSeconds = newTimerValue;
            UpdateTimerDisplay();
        }
    }

    void OnAddButtonPress(int value) {
        if (isTimerTicking) return;
        Debug.Log("Microwave: Add Button pressed!");
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
            // InvokeRepeating("TickTimerDown", 0.5f, 1.0f);
            StartCoroutine(TickTimerDown());
        }
    }

/*    void TickTimerDown() {
        if (timerValue > minTimeInSeconds && isTimerTicking) {
            timerValue--;
            UpdateTimerDisplay();
            Debug.Log("Microwave: Timer says - " + timerDisplayText.text);
        } else {
            finishedAudio.Play(0);
            ResetTimer();
            // sound effects and whatnot?
        }
    }*/

    IEnumerator TickTimerDown() { // Coroutine
        if (turnedOnAudio.loop == false) Debug.Log("(Warning) Microwave: Microwave turned on audio is not looped!");
        turnedOnAudio.Play();
        while (timerTimeInSeconds > minTimeInSeconds && isTimerTicking) {
            timerTimeInSeconds--;
            UpdateTimerDisplay();
            Debug.Log("Microwave: Timer says - " + timerDisplayText.text);
            yield return new WaitForSeconds(1f);
        }
        turnedOnAudio.Stop();
            finishedAudio.Play(0);
            ResetTimer();
        }

    void Start() {
        timerDisplayText.text = "00:00";
        // button event listeners
        addButton.onClick.AddListener(() => OnAddButtonPress(timerInterval));
        subtractButton.onClick.AddListener(() => OnSubtractButtonPress(timerInterval));
        goButton.onClick.AddListener(OnGoButtonPress);
    }

    void Update() {
    }
}
