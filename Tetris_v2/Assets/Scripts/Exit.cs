using UnityEditor;
using UnityEngine;

public class Exit : MonoBehaviour {
    public void QuitGame() {
        if (EditorApplication.isPlaying) {
            UnityEditor.EditorApplication.isPlaying = false;
        } else {
            Application.Quit();
        }
    }
}