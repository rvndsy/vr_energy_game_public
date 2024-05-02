using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class EnergyConsumer : MonoBehaviour
{
    [Header("GameObject to track")]
    [SerializeField] MicrowaveLogic consumerObject;
    [Header("Energy consumption values")]
    [SerializeField] float maxPowerConsumption = 0;
    // [SerializeField] float passiveEnergyConsumption = 0; // used when isTurnedOn == true && powerLevel == 0
    [Header("Optional")]
    [SerializeField] float powerLevel = 0;      // keep this value between 0 and 1. value of 1 indicates that maxPowerConsumption == powerConsumption;
    [SerializeField] bool isTurnedOn = true;
    float lastPowerLevel;                       // don't need to calculate powerConsumption every Update
    bool overrideConsumerObjectSettings = false;

    public float powerConsumption = 0;                 // this is the actual consumption in Watts (W)

    void Start() {
        if (powerLevel > 0) {
            overrideConsumerObjectSettings = true;
        }
        powerLevel = consumerObject.powerLevel;
    }

    void FixedUpdate() {
        if (lastPowerLevel != powerLevel && isTurnedOn) {
            lastPowerLevel = powerLevel;
            powerConsumption = powerLevel * maxPowerConsumption;
        } else if (!isTurnedOn) {
            powerLevel = 0;
            powerConsumption = 0;
        }
    }
}
