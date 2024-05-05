using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public enum Scene {
        MenuScene,
        DemoScene
    }

    public static void Load(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }
}