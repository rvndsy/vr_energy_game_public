using System;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour {

    [SerializeField] private static int timeMultiplier = 1; //how many seconds pass in a real second
    [SerializeField] private static bool lockTimeUpdateRefreshRate = false;

    private static float secondsPassed = 0;
    private static float fixedUpdateTimer = 0;
    private static float fixedDeltaTimeMultiplier = 1;
    private static float timeUpdateRefreshRateDecimal = 1;

    public static int TimeMultiplier { get { return timeMultiplier; } }
    public static float TimeUpdateRefreshRateDecimal { get { return timeUpdateRefreshRateDecimal; } }
    public static TimeManager Instance { get; private set; } //one TimeManager instance per scene
    public static float FixedUpdateTimer { get { return fixedUpdateTimer; } }

    public static UnityEvent onTimerTick;

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

    public string FormatSecondsToDayTimeString(float seconds) {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            return time.ToString(@"d\d\ hh\:mm\:ss");
    }

    public string GetTimeString() {
        return FormatSecondsToDayTimeString(secondsPassed);
    }

    private void UpdateSecondsPassed() {
        secondsPassed += timeMultiplier * timeUpdateRefreshRateDecimal;
    }

    void Awake() {
        onTimerTick = new UnityEvent();
        if (TimeManager.Instance == null) {
            Instance = this;
        }
    }

    void FixedUpdate() {
        if (timeMultiplier > 50) fixedDeltaTimeMultiplier = 50;             
        else                     fixedDeltaTimeMultiplier = timeMultiplier;

        if (!lockTimeUpdateRefreshRate) timeUpdateRefreshRateDecimal = 1 / fixedDeltaTimeMultiplier; // updating refresh rate

        fixedUpdateTimer += Time.fixedDeltaTime;

        if (fixedUpdateTimer >= timeUpdateRefreshRateDecimal) {
            onTimerTick.Invoke();
            UpdateSecondsPassed();
            fixedUpdateTimer = 0;
        }

        //Debug.Log($"{gameObject.name} - TimeManager: SECONDSPASSED = {secondsPassed}");
        //Debug.Log($"{gameObject.name} - TimeManager: fixedUpdateTimer = {fixedUpdateTimer}; timeMultiplier = {timeMultiplier}");
    }
}