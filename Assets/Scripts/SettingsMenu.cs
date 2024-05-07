using UltimateXR.Avatar;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Toggle toggleLocomotionSetting;

    void Awake() {
        toggleLocomotionSetting.isOn = SettingsManager.ReadLocomotionSettingIsTeleport();
    }

    void Start() {
        toggleLocomotionSetting.onValueChanged.AddListener(SettingsManager.SetLocomotionSettingIsTeleport);
    }

    void FixedUpdate() {
        Debug.Log($"{gameObject.name} - SettingsMenu: PlayerPrefs 'Locomotion' set to {PlayerPrefs.GetInt("Locomotion")}");
    }
}