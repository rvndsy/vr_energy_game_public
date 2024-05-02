using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyTracker : MonoBehaviour {

    [SerializeField] GameObject[] energyConsumerGameObjects;
    List<EnergyConsumer> energyConsumerList = new List<EnergyConsumer>();

    // float energyConsumedPerSecond = 0; //watt
    float energyConsumedPerFrame = 0;
    float totalConsumedEnergyInKilowattHours = 0;

    float convertJouleToKWH(float energy) { // 1 watt = 1 joule per second
        return energy / 3600000;
    }

    void Start() {
        foreach (var obj in energyConsumerGameObjects) {
            energyConsumerList.Add(obj.GetComponent<EnergyConsumer>());
        }
    }

    void FixedUpdate() {
        energyConsumedPerFrame = 0;
        foreach (var consumer in energyConsumerList) {
            energyConsumedPerFrame += consumer.powerConsumption;
            totalConsumedEnergyInKilowattHours += convertJouleToKWH(consumer.powerConsumption / 50);
        }
        Debug.Log("Total power draw per second = " + energyConsumedPerFrame);
        Debug.Log("Total power consumed = " + totalConsumedEnergyInKilowattHours);
    }
}
