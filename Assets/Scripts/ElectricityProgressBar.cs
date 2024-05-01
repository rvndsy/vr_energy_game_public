using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ElectricityProgressBar : MonoBehaviour {



    public Slider slider;
    public TextMeshProUGUI valueText;
    float energyConsumed = 0;

    public void OnSliderChanged(float value) {
        valueText.text = value.ToString() + "kWh";
    }

    public void UpdateProgress() {
        energyConsumed++;
        slider.value = energyConsumed;
    }
}
