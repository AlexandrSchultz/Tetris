using UnityEngine;

public class Manager : MonoBehaviour {
    [SerializeField]
    private GameObject gridGamePrefab;
    [SerializeField]
    private Score scorePrefab;
    [SerializeField]
    private Preview previewPrefab;
    [SerializeField]
    private GameObject containerPrefab;
    [SerializeField]
    private Transform canvas;

    public void Initialize() {
        Instantiate(gridGamePrefab, new Vector3((float) 8.301548, (float) 9.585592, (float) -0.2059703), Quaternion.identity, transform);
        Score score = Instantiate(scorePrefab, new Vector3((float) 748.1743, (float) 191.9733, 0), Quaternion.identity, canvas);
        Preview preview = Instantiate(previewPrefab, new Vector3(0, 1, 0), Quaternion.identity, transform);
        preview.Initialize(score);
        Instantiate(containerPrefab, new Vector3(0, 0, 0), Quaternion.identity, transform);
    }

    private void Start() {
        Initialize();
    }
}