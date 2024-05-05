using System.Collections;
using System.Collections.Generic;
using UltimateXR.Avatar;
using UltimateXR.UI.UnityInputModule;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    // !rm If this one is not used then do not submit!!!!!!!!!!!!!!!


    [SerializeField] UxrAvatar avatar = UxrAvatar.LocalAvatar;
    [SerializeField] private Toggle changeHandLaserSetting, unusedBtn;

    private void OnChangeHandLaserSettingButtonClick(bool state) {
/*
        pointerInputModule.DoesAutoEnableLaserPointer();
            = "Laser Pointers";

            .EnableLaserPointers(state);*/
        avatar.EnableFingerTips(!state);
    }
    
    void Awake() {
        EventSystem eventSystem = EventSystem.current;
        UxrPointerInputModule pointerInputModule = eventSystem.GetComponent<UxrPointerInputModule>();
    }

    void Start() {
        GameObject eventSystemObject = GameObject.Find("EventSystem");
        changeHandLaserSetting.onValueChanged.AddListener(OnChangeHandLaserSettingButtonClick);
    }
}
