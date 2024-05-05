using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Button buttonToMainMenu;
    [Header("Settings")]
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Button buttonToSettings;
    [Header("Start demo scene")]
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
        SceneLoader.Load(SceneLoader.Scene.DemoScene);
    }

    void Start() {
        buttonToMainMenu.onClick.AddListener(OnButtonToMainMenuClicked);
        buttonToSettings.onClick.AddListener(OnButtonToSettingsClicked);
        startDemoButton.onClick.AddListener(OnStartDemoButtonClicked);
    }
}
