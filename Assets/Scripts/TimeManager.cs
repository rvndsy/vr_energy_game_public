using System;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    [SerializeField] private int timeMultiplier = 1;
    public int TimeMultiplier { get { return timeMultiplier;  } }

    private int secondsPassed = 0;
    private float fixedUpdateTimer = 0;
    private float fixedDeltaTimeMultiplier = 1;

    public void ResetTimeMultiplier() {
        timeMultiplier = 1;
    }

    public void SetTimeMultiplier(int multiplier) {
        if (multiplier > 1000) {
            Debug.LogWarning($"{gameObject.name} - TimeManager: Something tried to change timeMultiplier to greater than 1000!");
            return;
        }
        timeMultiplier = multiplier;
    }

    public string FormatSecondsToDayTimeString(int seconds) {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            return time.ToString(@"Day dd hh\:mm\:ss");
    }

    public string GetTimeString() {
        return FormatSecondsToDayTimeString(secondsPassed);
    }

    private void UpdateSecondsPassed() {
        secondsPassed += timeMultiplier;
    }

    void FixedUpdate() {
        fixedUpdateTimer += Time.fixedDeltaTime * fixedDeltaTimeMultiplier;
        if (timeMultiplier > 50) fixedDeltaTimeMultiplier = 50;
        else fixedDeltaTimeMultiplier = timeMultiplier;

        if (fixedUpdateTimer >= 1) {
            UpdateSecondsPassed();
            fixedUpdateTimer = 0;
        }

        Debug.Log($"{gameObject.name} - TimeManager: SECONDSPASSED = {secondsPassed}");
        Debug.Log($"{gameObject.name} - TimeManager: fixedUpdateTimer = {fixedUpdateTimer}; timeMultiplier = {timeMultiplier}");
    }
}