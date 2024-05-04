using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Button buttonToMainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Button buttonToSettings;
    [SerializeField] private Button startDemoButton;

    public void OnButtonToSettingsClicked() {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OnButtonToMainMenuClicked() {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnStartDemoButtonClicked() {
        // TODO !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    }

    void Start() {
        buttonToMainMenu.onClick.AddListener(OnButtonToMainMenuClicked);
        buttonToSettings.onClick.AddListener(OnButtonToSettingsClicked);
        startDemoButton.onClick.AddListener(OnStartDemoButtonClicked);
    }
}
