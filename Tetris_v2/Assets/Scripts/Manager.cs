using UnityEngine;

public class Manager : MonoBehaviour {
    [SerializeField]
    private GridGame gridGamePrefab;
    [SerializeField]
    private Score scorePrefab;
    [SerializeField]
    private Preview previewPrefab;

    [SerializeField]
    private Transform canvas;

    public void Initialize() {
        GridGame gridGame = Instantiate(gridGamePrefab, new Vector3((float) 8.301548, (float) 9.585592, (float) -0.2059703), Quaternion.identity, transform);
        Score score = Instantiate(scorePrefab, new Vector3((float) 748.1743, (float) 191.9733, 0), Quaternion.identity, canvas);
        score.Initialize(gridGame);
        Preview preview = Instantiate(previewPrefab, new Vector3(0, 1, 0), Quaternion.identity, transform);
        preview.Initialize(score, gridGame);
    }

    private void Start() {
        Initialize();
    }
}