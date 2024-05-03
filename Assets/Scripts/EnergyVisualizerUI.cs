using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyVisualizerUI : MonoBehaviour {

        [SerializeField] private EnergyTracker energyTracker;
    // private Slider slider;
    private TextMeshProUGUI wattageDisplayText, kwhDisplayText;

    private void Awake() {
        TextMeshProUGUI[] textObjList = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI obj in textObjList) {
            switch (obj.name) {
                case "WattageDisplay":
                    wattageDisplayText = obj;
                    break;
                case "KilowattHourDisplay":
                    kwhDisplayText = obj;
                    break;
            }
        }
    }

    /*public void OnSliderChanged(float value) {
        valueText.text = value.ToString() + "kWh";
    }*/
    static public string FormatWattageDisplayTextString(float val) {
        return $"{val} W";
    }

    static public string FormatKWHDisplayTextString(float val) {
        return $"{val} kWh";
    }

    void FixedUpdate() {
        if (energyTracker.HasWattagePerFrameUpdated) 
            wattageDisplayText.text = FormatWattageDisplayTextString(energyTracker.WattagePerFrame);
        if (energyTracker.HasTotalConsumedKWHUpdated)
            kwhDisplayText.text = FormatKWHDisplayTextString(energyTracker.TotalConsumedKWH);
    }
}
