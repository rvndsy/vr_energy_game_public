using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class EnergyTracker : MonoBehaviour {

    List<EnergyConsumer> energyConsumerList = new List<EnergyConsumer>();

    // float energyConsumedPerSecond = 0; //watt
    private float wattagePerFixedUpdate = 0;
    private float totalConsumedJoules = 0;
    public float TotalConsumedJoules { get { return totalConsumedJoules; } }
    public float TotalConsumedKWH { get { return ConvertJouleToKWH(totalConsumedJoules); } }
    public float WattagePerFixedUpdate { get { return wattagePerFixedUpdate; } }

    // private float lastWattagePerFixedUpdate = -1;
    // private float lastTotalConsumedJoules = -1;

    // public bool HasWattagePerFixedUpdateChanged { get { return wattagePerFixedUpdate != lastWattagePerFixedUpdate; } }
    // public bool HasTotalConsumedJoulesChanged { get { return totalConsumedJoules != lastTotalConsumedJoules; } }

    static public float ConvertJouleToKWH(float val) { // 1 watt = 1 joule per second
        return val / 3600000;                          // 3600000 joules in 1 kWh
    }

    public void AddEnergyConsumer(EnergyConsumer obj) {
        energyConsumerList.Add(obj);
    }

    void Awake() {
        foreach (var obj in GameObject.FindGameObjectsWithTag("energyconsumer")) {
            EnergyConsumer consumerToTrack = obj.GetComponent<EnergyConsumer>();
            if (consumerToTrack != null) AddEnergyConsumer(consumerToTrack);
        }
    }

    void FixedUpdate() {
        wattagePerFixedUpdate = 0;
        foreach (var consumer in energyConsumerList) {
            // Debug.Log($"{consumer.name} - EnergyConsumer - EnergyTracker: wattagePerFixedUpdate {consumer.WattagePerFixedUpdate} -- totalConsumedJoules {consumer.WattagePerFixedUpdate}");
            wattagePerFixedUpdate += consumer.WattagePerFixedUpdate;
            totalConsumedJoules += (consumer.WattagePerFixedUpdate / 50 * TimeManager.TimeMultiplier);
        }

        // Debug.Log($"{gameObject.name} - EnergyTracker: {wattagePerFixedUpdate} wattagePerFixedUpdate");
        // Debug.Log($"{gameObject.name} - EnergyTracker: {totalConsumedJoules} totalConsumedJoules");

        // lastWattagePerFixedUpdate = wattagePerFixedUpdate;
        // lastTotalConsumedJoules = totalConsumedJoules;
    }
}