using UltimateXR.Avatar;
using UltimateXR.Core;
using UltimateXR.Locomotion;
using UnityEngine;

public class LoadMenuScene : MonoBehaviour {

    [Header("Assign player avatar")]
    [SerializeField] UxrAvatar myAvatar = UxrAvatar.LocalAvatar;
    [SerializeField] GameObject teleportLeft, teleportRight;

    [Header("Player avatar settings")]
    [SerializeField] bool hasTeleportEnabled;
    [SerializeField] bool hasLasterPointersEnabled = true;
    [SerializeField] Vector3 startingPosition = new Vector3(0, 0, 0);
    [SerializeField] Quaternion startingRotation = Quaternion.Euler(0, 0, 0);

    void Start() {
        UxrManager.Instance.TeleportLocalAvatar(Vector3.zero, Quaternion.identity, UxrTranslationType.Fade);
        myAvatar.EnableLaserPointers(hasLasterPointersEnabled);
    }

    void Update() {

    }
}
