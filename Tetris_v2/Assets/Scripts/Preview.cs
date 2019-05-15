using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour {

    private Score m_score;
    private GridGame m_gridGame;

    public int minoNext;
    public Group[] groups;
    public GameObject[] mino;
    [SerializeField]
    private Vector3 groupPosition = new Vector3(0, 0, 0);
    private List<Dictionary<ControlType, KeyCode>> m_controlKey;

    private void Start() {
        minoNext = Random.Range(0, 7);
        Spawn();
    }

    public void Initialize(Score score, GridGame gridGame, List<Dictionary<ControlType, KeyCode>> controlKey) {
        m_score = score;
        m_gridGame = gridGame;
        m_controlKey = controlKey;
    }

    public void Spawn() {
        Group group = Instantiate(groups[minoNext], m_gridGame.Container);
        group.transform.localPosition = groupPosition;
        group.Initialize(m_score, this, m_gridGame, m_controlKey);

        minoNext = Random.Range(0, 7);

        for (int i = 0; i < mino.Length; i++) {
            mino[i].SetActive(false);
        }

        mino[minoNext].SetActive(true);
    }
}