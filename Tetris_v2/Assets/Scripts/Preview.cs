using UnityEngine;

public class Preview : MonoBehaviour {

    public int minoNext;
    public Transform[] groups;
    public GameObject[] mino;

    public Transform currentTetramino;
    private Transform m_minos;

    private void Start() {
        GridOnScene.Instance.Create();
        minoNext = Random.Range(0, 7);
        Spawn();
    }

    public void Spawn() {
        m_minos = Instantiate(groups[minoNext], transform.position, Quaternion.identity);

        minoNext = Random.Range(0, 7);

        for (int i = 0; i < mino.Length; i++) {
            mino[i].SetActive(false);
        }

        mino[minoNext].SetActive(true);

        m_minos.transform.SetParent(currentTetramino);
    }
}