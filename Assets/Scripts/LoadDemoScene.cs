using System.Collections;
using System.Collections.Generic;
using UltimateXR.Avatar;
using UnityEngine;

public class LoadDemoScene : MonoBehaviour
{
    GameObject levelSelection = GameObject.Find("LevelSelection");
    UxrAvatar myAvatar = UxrAvatar.LocalAvatar;

    void Awake()
    {
        levelSelection.SetActive(false);
        myAvatar.EnableLaserPointers(false);
    }

    void Update()
    {
        
    }
}
