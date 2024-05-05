using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyVisualizerUI : MonoBehaviour {

    [SerializeField] private EnergyTracker energyTracker;
    [Header("Wattage")]
    [SerializeField] private int wattageDigitsDisplayedAfterPeriod = 0;
    [SerializeField] private string wattageAppendedText = " W";
    [Header("Kilo-Watt Hours")]
    [SerializeField] private int kwhDigitsDisplayedAfterPeriod = 6;
    [SerializeField] private string kwhAppendedText = " kWh";
    [Header("Joules")]
    [SerializeField] private int joulesDigitsDisplayedAfterPeriod = 2;
    [SerializeField] private string joulesAppendedText = " J";

    // private Slider slider;
    [Header("TextMeshPro fields to display values\nAppropriately named child objects will overwrite objects set in inspector")]
    [SerializeField] private TextMeshProUGUI wattageDisplay;
    [SerializeField] private TextMeshProUGUI kilowattHourDisplay;
    [SerializeField] private TextMeshProUGUI joulesDisplay;


    private void Awake() {
        TextMeshProUGUI[] textObjList = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI obj in textObjList) {
            if (obj != null) continue;
            switch (obj.name) {
                case "WattageDisplay":
                    wattageDisplay = obj;
                    break;
                case "KilowattHourDisplay":
                    kilowattHourDisplay = obj;
                    break;
                case "JoulesDisplay":
                    joulesDisplay = obj;
                    break;
            }
        }
    }

    static public string FormatUnitDisplayTextString(float val, string unit, int digitsAfterPeriod) {
        string str = val.ToString($"F{digitsAfterPeriod}");
        return $"{str}{unit}";
    }

    void FixedUpdate() {
        if (energyTracker.HasWattagePerFrameUpdated)
            wattageDisplay.text = FormatUnitDisplayTextString(energyTracker.WattagePerFrame, wattageAppendedText, wattageDigitsDisplayedAfterPeriod);
        if (energyTracker.HasTotalConsumedJoulesUpdated) {
            kilowattHourDisplay.text = FormatUnitDisplayTextString(energyTracker.TotalConsumedKWH, kwhAppendedText, kwhDigitsDisplayedAfterPeriod);
            joulesDisplay.text = FormatUnitDisplayTextString(energyTracker.TotalConsumedJoules, joulesAppendedText, joulesDigitsDisplayedAfterPeriod);
        }
    }
}
