using System;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour {
   
    [SerializeField] private bool lockTimeUpdateRefreshRate = false;
    private static int timeMultiplier = 1; //how many seconds pass in a real second

    private static float secondsPassed = 0;
    private static float fixedUpdateTimer = 0;
    private static float timeUpdateRefreshRatePeriod = 1;

    public static int TimeMultiplier { get { return timeMultiplier; } }
    public static float TimeUpdateRefreshRatePeriod { get { return timeUpdateRefreshRatePeriod; } }
    public static TimeManager Instance { get; private set; } //one TimeManager instance per scene
    public static float FixedUpdateTimer { get { return fixedUpdateTimer; } }

    public static UnityEvent onTimerTick;

    public void ResetTimeMultiplier() {
        timeMultiplier = 1;
    }

    public void SetTimeMultiplier(int multiplier) {
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
        secondsPassed += timeMultiplier * timeUpdateRefreshRatePeriod;
    }

    void Awake() {
        onTimerTick = new UnityEvent();
        if (TimeManager.Instance == null) {
            Instance = this;
        }
    }

    void FixedUpdate() {
        if (!lockTimeUpdateRefreshRate) timeUpdateRefreshRatePeriod = 1f / timeMultiplier; // updating refresh rate

        fixedUpdateTimer += Time.fixedDeltaTime;

        if (fixedUpdateTimer >= timeUpdateRefreshRatePeriod) {
            onTimerTick.Invoke();
            UpdateSecondsPassed();
            fixedUpdateTimer = 0;
        }
        // Debug.Log($"{gameObject.name} - TimeManager: secondsPassed = {secondsPassed}, timeMultiplier = {timeMultiplier}, timeUpdateRefreshRatePeriod = {1 / timeMultiplier}");
        //Debug.Log($"{gameObject.name} - TimeManager: fixedUpdateTimer = {fixedUpdateTimer}; timeMultiplier = {timeMultiplier}");
    }
}