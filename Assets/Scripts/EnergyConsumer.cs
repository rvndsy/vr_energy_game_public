using System.Net;
using UnityEngine;

public class EnergyConsumer : MonoBehaviour {

    [Header("Required energy consumption values")]
    [SerializeField] protected float maxWattage = 0;
    [SerializeField] protected float passiveWattage = 0; // used when isTurnedOn == true && powerLevel == 0
    [Header("Optional")]
    [SerializeField] protected float powerLevel = 0;     // keep this value between 0 and 1. value of 1 indicates that maxPowerConsumption == powerConsumption;
    [SerializeField] protected bool isTurnedOn = true;

    protected float lastPowerLevel = -1;

    protected float wattagePerFixedUpdate = 0;           // this is the actual consumption in Watts (W)
    protected float totalConsumedEnergyInJoules = 0;

    // public bool HasWattagePerFixedUpdateChanged { get { return wattagePerFixedUpdate != lastWattagePerFixedUpdate; } }
    // public bool HasPowerLevelChanged { get { return powerLevel != lastPowerLevel; } }

    public float WattagePerFixedUpdate {  get { return wattagePerFixedUpdate; } }
    public float TotalConsumedEnergyInJoules { get { return totalConsumedEnergyInJoules; } }
    public float PowerLevel { get {  return powerLevel; } set { powerLevel = value; } }

    protected void TurnOn(float lvl = 0) {
        isTurnedOn = true;
        powerLevel = lvl;
        wattagePerFixedUpdate = passiveWattage;
    }

    protected void TurnOff() {
        isTurnedOn = false;
        powerLevel = 0;
        wattagePerFixedUpdate = 0;
    }

    protected void TurnToIdle() {
        isTurnedOn = true;
        powerLevel = 0;
        wattagePerFixedUpdate = passiveWattage;
    }

    protected void TurnToPower(float lvl) {
        TurnOn(lvl);
    }

    protected void TurnToMax() {
        isTurnedOn = true;
        powerLevel = 1;
        wattagePerFixedUpdate = maxWattage;
    }

    void FixedUpdate() {
        if (!isTurnedOn) {
            return;
        }

        if (lastPowerLevel != powerLevel) {
            if (powerLevel > 0) {
                wattagePerFixedUpdate = maxWattage * powerLevel;
            }
            else {
                wattagePerFixedUpdate = passiveWattage;
            }
        }

        totalConsumedEnergyInJoules += (wattagePerFixedUpdate / 50);

        lastPowerLevel = powerLevel;

        // Debug.Log($"{gameObject.name} - EnergyConsumer: Total Energy Consumed = {totalConsumedEnergyInJoules} ");
        // Debug.Log($"{gameObject.name} - EnergyConsumer: Power Consumption =  {wattage}");
    }
}
