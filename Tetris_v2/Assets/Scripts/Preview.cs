using UnityEngine;

public class Preview : MonoBehaviour {

    private Score m_score;

    public int minoNext;
    public Group[] groups;
    public GameObject[] mino;

    private void Start() {
        minoNext = Random.Range(0, 7);
        Spawn();
    }
    
    public void Initialize(Score score) {
        m_score = score;
    }
    
    public void Spawn() {
        Group group = Instantiate(groups[minoNext], new Vector3(4, 15, 0), Quaternion.identity);
        group.Initialize(m_score, this);

        minoNext = Random.Range(0, 7);

        for (int i = 0; i < mino.Length; i++) {
            mino[i].SetActive(false);
        }

        mino[minoNext].SetActive(true);
    }
}