using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class EnergyTracker : MonoBehaviour {

    List<EnergyConsumer> energyConsumerList = new List<EnergyConsumer>();

    // float energyConsumedPerSecond = 0; //watt
    private float wattagePerFrame = 0;
    private float totalConsumedJoules = 0;
    public float TotalConsumedJoules { get { return totalConsumedJoules; } }
    public float TotalConsumedKWH { get { return ConvertJouleToKWH(lastTotalConsumedJoules); } }
    public float WattagePerFrame { get { return wattagePerFrame; } }

    private float lastWattagePerFrame, lastTotalConsumedJoules;

    public bool HasWattagePerFrameUpdated { get { return wattagePerFrame == lastWattagePerFrame; } }
    public bool HasTotalConsumedJoulesUpdated { get { return totalConsumedJoules == lastTotalConsumedJoules; } }
    public bool HasTotalConsumedKWHUpdated { get { return HasTotalConsumedJoulesUpdated; } }


    static public float ConvertJouleToKWH(float val) { // 1 watt = 1 joule per second
        return val / 3600000;                          // 3600000 joules in 1 kWh
    }

    public void AddEnergyConsumerToTrackingList(EnergyConsumer obj) {
        energyConsumerList.Add(obj);
    }

    void Start() {
        foreach (var obj in GameObject.FindGameObjectsWithTag("energyconsumer")) {
            AddEnergyConsumerToTrackingList(obj.GetComponent<EnergyConsumer>());
        }
    }

    void FixedUpdate() {
        wattagePerFrame = 0;
        foreach (var consumer in energyConsumerList) {
            wattagePerFrame += consumer.Wattage;
            totalConsumedJoules += consumer.Wattage / 50;
        }
        lastWattagePerFrame = wattagePerFrame;
        lastTotalConsumedJoules = totalConsumedJoules;
        // Debug.Log("Total power draw per second = " + energyConsumedPerFrame);
        // Debug.Log("Total power consumed = " + totalConsumedEnergyInKilowattHours);
    }
}