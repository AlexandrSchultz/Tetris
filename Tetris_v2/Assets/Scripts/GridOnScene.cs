using UnityEngine;

public class GridOnScene : MonoBehaviour {
    [SerializeField]
    public GameObject gridOnScene;

    private static GridOnScene m_instance;

    public static GridOnScene Instance {
        get {
            return m_instance;
        }
    }

    private void Awake() {
        if (m_instance == null) {
            m_instance = this;
        }
    }

    public void Create() {
        Instantiate(gridOnScene, new Vector3((float) 8.301548, (float) 9.585592, (float) -0.2059703), Quaternion.identity);
    }

}