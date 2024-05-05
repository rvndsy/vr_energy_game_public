using UnityEngine;

public class EnergyConsumer : MonoBehaviour {

    [Header("Energy consumption values")]
    [SerializeField] protected float maxPowerConsumption = 0;
    [SerializeField] protected float passiveEnergyConsumption = 0; // used when isTurnedOn == true && powerLevel == 0
    [Header("Optional")]
    [SerializeField] protected float powerLevel = 0;       // keep this value between 0 and 1. value of 1 indicates that maxPowerConsumption == powerConsumption;
    [SerializeField] protected bool isTurnedOn = true;
    protected float lastPowerLevel;                        // don't need to calculate powerConsumption every Update

    private float wattage = 0;           // this is the actual consumption in Watts (W)
    private float totalConsumedEnergyInJoules = 0;
    public float Wattage {  get { return wattage; } }
    public float TotalConsumedEnergyInJoules { get { return totalConsumedEnergyInJoules; } }
    public float PowerLevel { get {  return powerLevel; } set { powerLevel = value; } }

    protected void TurnOn() {
        isTurnedOn = true;
    }

    protected void TurnOff() {
        isTurnedOn = false;
    }

    protected void Activate() {
        isTurnedOn = true;
        powerLevel = 1;
    }

    protected void Deactivate() {
        isTurnedOn = false;
        powerLevel = 0;
    }

    void Start() {
/*        hideFlags = HideFlags.HideInHierarchy;
        hideFlags = HideFlags.HideInInspector;*/
    }

    void FixedUpdate() {
        if (lastPowerLevel != powerLevel && isTurnedOn) {
            lastPowerLevel = powerLevel;
            wattage = powerLevel * maxPowerConsumption;
            totalConsumedEnergyInJoules += wattage / 50;
        } else if (!isTurnedOn) {
            powerLevel = 0;
            wattage = 0;
        }
/*        Debug.Log("Consumer: Total Energy Consumed = " + powerConsumption);
        Debug.Log("Consumer: Power Consumption = " + powerConsumption);
*/    }
}
