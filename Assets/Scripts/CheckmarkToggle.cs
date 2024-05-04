using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCheckmark : MonoBehaviour {
    [SerializeField] private Image checkmarkImage;
    [SerializeField] private Toggle toggle;

    void Awake() {
        toggle = gameObject.GetComponent<Toggle>();
        if (toggle == null) {
            Debug.LogWarning($"{gameObject.name} - ToggleCheckmark: Script not assigned to a toggle!");
        }
    }

    void OnToggleValueChanged(bool state) {
        Debug.Log($"{gameObject.name} - ToggleCheckmark: Checkmark ticked!");
        checkmarkImage.gameObject.SetActive(state);
    }

    void Start() {
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
        checkmarkImage.gameObject.SetActive(toggle.isOn);
    }

}
