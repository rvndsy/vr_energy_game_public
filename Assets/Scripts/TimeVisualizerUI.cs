using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeVisualizerUI : MonoBehaviour
{
    [SerializeField] private List<int> timeMultiplierList = new List<int>();
    [Header("")]
    [SerializeField] private Slider changeTimeMultiplierSlider;
    [SerializeField] private TextMeshProUGUI multiplierDisplay;
    [SerializeField] private TextMeshProUGUI timeDisplay;
    [SerializeField] private TimeManager timeManager; //finds it automatically

    public void ResetSliderValue() {
        changeTimeMultiplierSlider.value = 1;
        timeManager.SetTimeMultiplier(1);
    }

    private void OnSliderValueChanged(float value) {
        timeManager.SetTimeMultiplier((int)changeTimeMultiplierSlider.value);
        multiplierDisplay.text = $"{changeTimeMultiplierSlider.value}x";
    }

    void Start() {
        timeManager = FindObjectOfType<TimeManager>();
        changeTimeMultiplierSlider.value = 1;
        changeTimeMultiplierSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void FixedUpdate() {
        timeDisplay.text = timeManager.GetTimeString(); 
        Debug.LogWarning($"{gameObject.name} - TimeInteractiveUI: {changeTimeMultiplierSlider.value}");
    }
}
