using UnityEngine;

public class Preview : MonoBehaviour {

    private Score m_score;
    private GridGame m_gridGame;
    private bool m_oneControl;
    private bool m_twoControl;
    private bool m_threeControl;

    public int minoNext;
    public Group[] groups;
    public GameObject[] mino;
    [SerializeField]
    private Vector3 groupPosition = new Vector3(0, 0, 0);

    private void Start() {
        minoNext = Random.Range(0, 7);
        Spawn();
    }

    public void Initialize(Score score, GridGame gridGame, bool oneControl, bool twoControl, bool threeControl) {
        m_score = score;
        m_gridGame = gridGame;
        m_oneControl = oneControl;
        m_twoControl = twoControl;
        m_threeControl = threeControl;
    }

    public void Spawn() {
        Group group = Instantiate(groups[minoNext], m_gridGame.Container);
        group.transform.localPosition = groupPosition;
        group.Initialize(m_score, this, m_gridGame, m_oneControl, m_twoControl, m_threeControl);

        minoNext = Random.Range(0, 7);

        for (int i = 0; i < mino.Length; i++) {
            mino[i].SetActive(false);
        }

        mino[minoNext].SetActive(true);
    }
}