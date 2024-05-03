using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyTracker : MonoBehaviour {

    List<EnergyConsumer> energyConsumerList = new List<EnergyConsumer>();

    // float energyConsumedPerSecond = 0; //watt
    private float wattagePerFrame = 0;
    private float totalConsumedKWH = 0;
    public float TotalConsumedKWH { get { return totalConsumedKWH; } }
    public float WattagePerFrame { get { return wattagePerFrame; } }

    private float lastWattagePerFrame, lastTotalConsumedKWH;

    public bool HasWattagePerFrameUpdated { get { return wattagePerFrame == lastWattagePerFrame; } }
    public bool HasTotalConsumedKWHUpdated { get { return totalConsumedKWH == lastTotalConsumedKWH; } }

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
            wattagePerFrame += consumer.PowerConsumption;
            totalConsumedKWH += ConvertJouleToKWH(consumer.PowerConsumption / 50);
        }
        lastWattagePerFrame = wattagePerFrame;
        lastTotalConsumedKWH = totalConsumedKWH;
        // Debug.Log("Total power draw per second = " + energyConsumedPerFrame);
        // Debug.Log("Total power consumed = " + totalConsumedEnergyInKilowattHours);
    }
}