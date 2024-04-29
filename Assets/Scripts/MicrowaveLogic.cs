using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MicrowaveLogic : MonoBehaviour
{
    [Header("Microwave Attributes")]
    [SerializeField] Button addButton;
    [SerializeField] Button subtractButton;
    [SerializeField] Button goButton;
    [SerializeField] TextMeshProUGUI timerDisplayText;
    [SerializeField] int maxTimeInSeconds = 120;
    [SerializeField] int timerInterval = 10;
    int minTimeInSeconds = 0;
    int timerValue = 0;
    bool isTimerTicking = false;

    // TODO:
    // Cannot execute Go whilst the door is open
    // (optional) Light inside the microwave
    // Sound effect
    // Tactile feedback?
    // Door locks and snaps into place?
    // 


    public void UpdateTimerDisplay() {
        int minutes = timerValue / 60;
        int seconds = timerValue % 60;
        timerDisplayText.text = $"{minutes:00}:{seconds:00}";
    }

    public void ResetTimer() {
        isTimerTicking = false;
        timerValue = 0;
        timerDisplayText.text = "00:00";
        CancelInvoke("TickTimerDown");
    }

    public void OnSubtractButtonPress(int value) {
        if (isTimerTicking) return;
        Debug.Log("Microwave: Subtract Button pressed!");
        int newTimerValue = timerValue - timerInterval;
        if (newTimerValue > minTimeInSeconds) {
            timerValue = newTimerValue;
            UpdateTimerDisplay();
        }
    }

    public void OnAddButtonPress(int value) {
        if (isTimerTicking) return;
        Debug.Log("Microwave: Add Button pressed!");
        int newTimerValue = timerValue + timerInterval;
        if (newTimerValue < maxTimeInSeconds) {
            timerValue = newTimerValue;
            UpdateTimerDisplay();
        }
    }

    public void OnGoButtonPress() {
        Debug.Log("Microwave: Go Button pressed!");
        if (!isTimerTicking) {
            isTimerTicking = true;
            InvokeRepeating("TickTimerDown", 0.5f, 1.0f);
        }
    }

    void Start() {
        timerDisplayText.text = "00:00";
        // button event listeners
        addButton.onClick.AddListener(() => OnAddButtonPress(timerInterval));
        subtractButton.onClick.AddListener(() => OnSubtractButtonPress(timerInterval));
        goButton.onClick.AddListener(OnGoButtonPress);
    }

    void TickTimerDown() {
        if (timerValue >= minTimeInSeconds && isTimerTicking) {
            timerValue--;
            UpdateTimerDisplay();
            Debug.Log("Microwave: Timer says - " + timerDisplayText.text);
        } else {
            ResetTimer();
            // sound effects and whatnot?
        }
    }
}
