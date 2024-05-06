using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HingedSwitch : MonoBehaviour {

    [SerializeField] private float angleOn = 7f;
    [SerializeField] private float angleOff = -7f;
    [SerializeField] private GameObject hingedSwitch;

    private AudioSource sound;

    public UnityEvent onPress;

    private bool isFlipped;
    private Quaternion defaultRotation;

    public void OnTriggerEnter() {
        Debug.Log($"{gameObject.name} - HingedSwitch: OnTriggerEnter executed");
        SetState(!isFlipped);
        onPress.Invoke();
        if (sound != null) sound.Play();
    }

    public void SetState(bool state) {
        isFlipped = state;
        RotateHingedSwitchToState(state);
    }

    private void RotateHingedSwitchToState(bool state) {
        if (state) hingedSwitch.transform.rotation = Quaternion.Euler(angleOn, defaultRotation.eulerAngles.y, defaultRotation.eulerAngles.z);
        else       hingedSwitch.transform.rotation = Quaternion.Euler(angleOff, defaultRotation.eulerAngles.y, defaultRotation.eulerAngles.z);
    }

    void Awake() {
        defaultRotation = gameObject.transform.rotation;
        sound = GetComponent<AudioSource>();
        if (gameObject.GetComponent<Rigidbody>() == null) {
            Debug.LogWarning($"{gameObject.name} - HingedSwitch: No Rigidbody component found!");
        }
    }
}