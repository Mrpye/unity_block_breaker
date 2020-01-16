using UnityEngine;

public class Level : MonoBehaviour {
    [SerializeField] private int breakableBlocks;// debugging
    SceneLoader sceneloader;

    public void Start() {
        sceneloader =FindObjectOfType<SceneLoader>();
    }
    public void CountBlocks() {
        breakableBlocks++;
    }

    public void BlockBroken() {
        breakableBlocks--;
        if (breakableBlocks <= 0) {
            sceneloader.LoadNextScene();
        }
    }
}