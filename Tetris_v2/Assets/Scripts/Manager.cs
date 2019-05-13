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

    [SerializeField]
    private Vector3 gridPosition;
    [SerializeField]
    private Vector3 scorePosition;
    [SerializeField]
    private Vector3 previewPosition;

    public void Initialize(Vector3 position, bool doubleControl) {
        GridGame gridGame = Instantiate(gridGamePrefab, position + gridPosition, Quaternion.identity, transform);
        Score score = Instantiate(scorePrefab, 37 * position + scorePosition, Quaternion.identity, canvas);
        score.Initialize(gridGame);
        Preview preview = Instantiate(previewPrefab, position + previewPosition, Quaternion.identity, transform);
        preview.Initialize(score, gridGame, doubleControl);
    }

//new Vector3((float) 8.301548, (float) 9.585592, (float) -0.2059703)
//new Vector3((float) 748.1743, (float) 191.9733, 0)
//new Vector3(0, 1, 0)
}