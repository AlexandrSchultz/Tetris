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

    public void Initialize(Vector3 position, bool oneControl, bool twoControl, bool threeControl) {
        GridGame gridGame = Instantiate(gridGamePrefab, position + gridPosition, Quaternion.identity, transform);
        Score score = Instantiate(scorePrefab, 22 * position + scorePosition, Quaternion.identity, canvas);
        score.Initialize(gridGame);
        Preview preview = Instantiate(previewPrefab, position + previewPosition, Quaternion.identity, transform);
        preview.Initialize(score, gridGame, oneControl, twoControl, threeControl);
    }
}