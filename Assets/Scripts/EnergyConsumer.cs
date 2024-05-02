using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class EnergyConsumer : MonoBehaviour {

    [Header("Energy consumption values")]
    [SerializeField] protected float maxPowerConsumption = 0;
    [SerializeField] protected float passiveEnergyConsumption = 0; // used when isTurnedOn == true && powerLevel == 0
    [Header("Optional")]
    [SerializeField] protected float powerLevel = 0;       // keep this value between 0 and 1. value of 1 indicates that maxPowerConsumption == powerConsumption;
    [SerializeField] protected bool isTurnedOn = true;
    protected float lastPowerLevel;                        // don't need to calculate powerConsumption every Update

    public float powerConsumption = 0;           // this is the actual consumption in Watts (W)
    public float totalConsumedEnergyInKilowattHours = 0;

    protected void setPowerLevel(float val) {
        powerLevel = val;
    }

    protected void turnOn() {}
    protected void turnOff() {}

    float convertJouleToKWH(float energy) { // 1 watt = 1 joule per second
        return energy / 3600000;
    }

    void Start() {
        hideFlags = HideFlags.HideInHierarchy;
        hideFlags = HideFlags.HideInInspector;
    }

    void FixedUpdate() {
        if (lastPowerLevel != powerLevel && isTurnedOn) {
            lastPowerLevel = powerLevel;
            powerConsumption = powerLevel * maxPowerConsumption;
            totalConsumedEnergyInKilowattHours += convertJouleToKWH(powerConsumption / 50);
        } else if (!isTurnedOn) {
            powerLevel = 0;
            powerConsumption = 0;
        }
        Debug.Log("Consumer: Total Energy Consumed = " + powerConsumption);
        Debug.Log("Consumer: Power Consumption = " + powerConsumption);
    }
}
