using System.Collections;
using System.Collections.Generic;
using UltimateXR.Avatar;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] UxrAvatar avatar = UxrAvatar.LocalAvatar;
    [SerializeField] private Toggle changeHandLaserSetting, unusedBtn;

    private void OnChangeHandLaserSettingButtonClick(bool state) {
        avatar.EnableLaserPointers(state);
        avatar.EnableFingerTips(!state);
    }

    void Start() {
        changeHandLaserSetting.onValueChanged.AddListener(OnChangeHandLaserSettingButtonClick);
    }
}
