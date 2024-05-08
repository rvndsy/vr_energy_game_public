using UltimateXR.Avatar;
using UltimateXR.Locomotion;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    UxrAvatar avatar;

    public static bool ReadLocomotionSettingIsTeleport() {
        if (PlayerPrefs.GetInt("Locomotion") == 1) {
            return true;
        }
        return false;
    }

    public static void SetLocomotionSettingIsTeleport(bool isTeleport) {
        int val = isTeleport ? 1 : 0;
        PlayerPrefs.SetInt("Locomotion", val);
    }

    public void LoadSettings() {
        SetLocomotionSetting(PlayerPrefs.GetInt("Locomotion"));
    }

    public void SetLocomotionSetting(int val) {
        bool state = val == 1 ? true : false;

        UxrSmoothLocomotion smoothLoco = avatar.GetComponent<UxrSmoothLocomotion>();
        if (smoothLoco != null) {
            smoothLoco.enabled = !state;
        } else {
            Debug.LogWarning($"{gameObject.name} - SettingsManager: No UxrTeleportLocomotion assigned in children!");
        }

        UxrTeleportLocomotion[] teleportLocoList = avatar.GetComponentsInChildren<UxrTeleportLocomotion>();
        foreach (UxrTeleportLocomotion teleportLoco in teleportLocoList) {
            teleportLoco.enabled = state;
        }
        if (teleportLocoList == null) Debug.LogWarning($"{gameObject.name} - SettingsManager: No UxrTeleportLocomotion assigned in children!");

    }

    void Start() {
        avatar = UxrAvatar.LocalAvatar;
        LoadSettings();
    }
}
