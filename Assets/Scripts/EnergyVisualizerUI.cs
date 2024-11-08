using TMPro;
using UnityEngine;

public class EnergyVisualizerUI : MonoBehaviour {

    [SerializeField] private EnergyTracker energyTracker;
    [Header("Wattage")]
    [SerializeField] private int wattageDigitsDisplayedAfterPeriod = 1;
    [SerializeField] private string wattageAppendedText = " W";
    [Header("Kilo-Watt Hours")]
    [SerializeField] private int kwhDigitsDisplayedAfterPeriod = 6;
    [SerializeField] private string kwhAppendedText = " kWh";
    [Header("Joules")]
    [SerializeField] private int joulesDigitsDisplayedAfterPeriod = 2;
    [SerializeField] private string joulesAppendedText = " J";

    [Header("TextMeshPro fields to display values\nAppropriately named child objects will overwrite objects set in inspector")]
    [SerializeField] private TextMeshProUGUI wattageDisplay;
    [SerializeField] private TextMeshProUGUI kilowattHourDisplay;
    [SerializeField] private TextMeshProUGUI joulesDisplay;

    private float displayedWattageValue = -1;
    private float displayedJoulesValue = -1;

    void Awake() {
        TextMeshProUGUI[] textObjList = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI obj in textObjList) {
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

    static public string FormatValueUnitDisplayString(float val, string unitStr, int digitsAfterPeriod) {
        string str = val.ToString($"F{digitsAfterPeriod}");
        return $"{str}{unitStr}";
    }

    void Start() {
        displayedWattageValue = energyTracker.WattagePerFixedUpdate;
        displayedJoulesValue = energyTracker.TotalConsumedJoules;
    }

    void FixedUpdate() {
        if (energyTracker.WattagePerFixedUpdate != displayedWattageValue) {
            displayedWattageValue = energyTracker.WattagePerFixedUpdate;
            wattageDisplay.text = FormatValueUnitDisplayString(displayedWattageValue, wattageAppendedText, wattageDigitsDisplayedAfterPeriod);
            // Debug.Log($"{gameObject.name} - EnergyVisualizerUI: Updating wattageDisplay.text to {displayedWattageValue}");
        }
        if (energyTracker.TotalConsumedJoules != displayedJoulesValue) {
            displayedJoulesValue = energyTracker.TotalConsumedJoules;
            kilowattHourDisplay.text = FormatValueUnitDisplayString(energyTracker.TotalConsumedKWH, kwhAppendedText, kwhDigitsDisplayedAfterPeriod);
            joulesDisplay.text = FormatValueUnitDisplayString(energyTracker.TotalConsumedJoules, joulesAppendedText, joulesDigitsDisplayedAfterPeriod);
            // Debug.Log($"{gameObject.name} - EnergyVisualizerUI: Updating joulesDisplay.text to {energyTracker.TotalConsumedJoules}");
            // Debug.Log($"{gameObject.name} - EnergyVisualizerUI: Updating kilowattHourDisplay.text to {energyTracker.TotalConsumedKWH}");
        }
    }
}
