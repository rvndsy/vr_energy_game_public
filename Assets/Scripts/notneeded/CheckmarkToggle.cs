using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCheckmark : MonoBehaviour {
    [SerializeField] private Image checkmarkImage;
    [SerializeField] private Toggle toggle;
    [SerializeField] private bool isOn;

    void Awake() {
        toggle = gameObject.GetComponent<Toggle>();
        if (toggle == null)         Debug.LogWarning($"{gameObject.name} - ToggleCheckmark: Script not assigned to a toggle!");

        if (checkmarkImage == null) Debug.LogWarning($"{gameObject.name} - ToggleCheckmark: Checkmark Image not assigned!");
    }

    private void OnToggleValueChanged(bool isOn) {
        Debug.Log($"{gameObject.name} - ToggleCheckmark: Checkmark ticked!");
        checkmarkImage.gameObject.SetActive(!isOn);
    }

    void Start() {
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
        //checkmarkImage.gameObject.SetActive(toggle.isOn);
    }
}