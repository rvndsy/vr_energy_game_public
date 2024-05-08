using UnityEngine;

public class EnergyConsumer : MonoBehaviour {

    [Header("Required energy consumption values")]
    [SerializeField] protected float maxWattage = 0;
    [SerializeField] protected float passiveWattage = 0; // used when isTurnedOn == true && powerLevel == 0
    [Header("Optional")]
    [SerializeField] protected float powerLevel = 0;     // keep this value between 0 and 1. value of 1 indicates that maxPowerConsumption == powerConsumption;
    [SerializeField] protected bool isTurnedOn = false;
    [SerializeField] TimeManager timeManager;

    protected float lastPowerLevel = -1;

    protected float wattagePerFixedUpdate = 0;           // this is the actual consumption in Watts (W)
    protected float totalConsumedEnergyInJoules = 0;

    public float WattagePerFixedUpdate {  get { return wattagePerFixedUpdate; } }
    public float TotalConsumedEnergyInJoules { get { return totalConsumedEnergyInJoules; } }
    public float PowerLevel { get {  return powerLevel; } set { powerLevel = value; } }

    protected virtual void TurnOn(float pwrLvl = 1) {
        isTurnedOn = true;
        powerLevel = pwrLvl;
        wattagePerFixedUpdate = maxWattage * powerLevel;
    }

    protected virtual void TurnOff() {
        isTurnedOn = false;
        powerLevel = 0;
        wattagePerFixedUpdate = 0;
    }

    protected virtual void TurnToIdle() {
        isTurnedOn = true;
        powerLevel = 0;
        wattagePerFixedUpdate = passiveWattage;
    }

    protected virtual void TurnToPower(float lvl) {
        TurnOn(lvl);
    }

    protected virtual void TurnToMax() {
        isTurnedOn = true;
        powerLevel = 1;
        wattagePerFixedUpdate = maxWattage;
    }

    void Start() {
        TimeManager.onTimerTick.AddListener(UpdateCycle);
    }

    private void UpdateCycle() {
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

        totalConsumedEnergyInJoules += (wattagePerFixedUpdate);

        lastPowerLevel = powerLevel;
    }

    void FixedUpdate() {
        // Debug.Log($"{gameObject.name} - EnergyConsumer: Total Energy Consumed = {totalConsumedEnergyInJoules} ");
        // Debug.Log($"{gameObject.name} - EnergyConsumer: Power Consumption =  {wattage}");
    }
}
