using UnityEngine;

public class Container : MonoBehaviour {
    private static Container m_instance;

    public static Container Instance {
        get {
            return m_instance;
        }
    }

    private void Awake() {
        if (m_instance == null) {
            m_instance = this;
        }
    }

}