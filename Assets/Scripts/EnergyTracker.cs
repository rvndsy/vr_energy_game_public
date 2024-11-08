using System.Collections.Generic;
using UnityEngine;

public class EnergyTracker : MonoBehaviour {

    List<EnergyConsumer> energyConsumerList = new List<EnergyConsumer>();

    private float wattagePerFixedUpdate = 0;
    private float totalConsumedJoules = 0;
    public float TotalConsumedJoules { get { return totalConsumedJoules; } }
    public float TotalConsumedKWH { get { return ConvertJouleToKWH(totalConsumedJoules); } }
    public float WattagePerFixedUpdate { get { return wattagePerFixedUpdate; } }

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

    void Start() {
        TimeManager.onTimerTick.AddListener(UpdateCycle);
    }

    private void UpdateCycle() {
        wattagePerFixedUpdate = 0;
        foreach (var consumer in energyConsumerList) {
            // Debug.Log($"{consumer.name} - EnergyConsumer - EnergyTracker: wattagePerFixedUpdate {consumer.WattagePerFixedUpdate} -- totalConsumedJoules {consumer.WattagePerFixedUpdate}");
            wattagePerFixedUpdate += consumer.WattagePerFixedUpdate;
            totalConsumedJoules += consumer.WattagePerFixedUpdate;
        }
    }
}