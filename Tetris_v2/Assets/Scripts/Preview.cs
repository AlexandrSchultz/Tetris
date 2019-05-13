using UnityEngine;

public class Preview : MonoBehaviour {

    private Score m_score;
    private GridGame m_gridGame;
    private bool m_doubleControl;

    public int minoNext;
    public Group[] groups;
    public GameObject[] mino;

    private void Start() {
        minoNext = Random.Range(0, 7);
        Spawn();
    }

    public void Initialize(Score score, GridGame gridGame, bool doubleControl) {
        m_score = score;
        m_gridGame = gridGame;
        m_doubleControl = doubleControl;
    }

    public void Spawn() {
        Group group = Instantiate(groups[minoNext], m_gridGame.Container);
        group.transform.localPosition = new Vector3(4, 15, 0);
        group.Initialize(m_score, this, m_gridGame, m_doubleControl);

        minoNext = Random.Range(0, 7);

        for (int i = 0; i < mino.Length; i++) {
            mino[i].SetActive(false);
        }

        mino[minoNext].SetActive(true);
    }
}