using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class EnergyVisualizerUI : MonoBehaviour {

    private Slider slider;
    private TextMeshProUGUI valueText;
    private EnergyTracker energyTracker;

    private void Awake() {
        //if 
        energyTracker = GetComponent<EnergyTracker>();
    }

    public void OnSliderChanged(float value) {
        valueText.text = value.ToString() + "kWh";
    }

    public void UpdateProgress() {
    }
}
