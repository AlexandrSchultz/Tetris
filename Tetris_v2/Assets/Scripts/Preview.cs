﻿using UnityEngine;

public class Preview : MonoBehaviour {

    private Score m_score;
    private GridGame m_gridGame;

    public int minoNext;
    public Group[] groups;
    public GameObject[] mino;

    private void Start() {
        minoNext = Random.Range(0, 7);
        Spawn();
    }

    public void Initialize(Score score, GridGame gridGame) {
        m_score = score;
        m_gridGame = gridGame;
    }

    public void Spawn() {
        Group group = Instantiate(groups[minoNext], new Vector3(4, 15, 0), Quaternion.identity);
        group.Initialize(m_score, this, m_gridGame);

        minoNext = Random.Range(0, 7);

        for (int i = 0; i < mino.Length; i++) {
            mino[i].SetActive(false);
        }

        mino[minoNext].SetActive(true);
    }
}