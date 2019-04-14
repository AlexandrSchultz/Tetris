using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{

    public int minoNext;
    public Transform[] groups;
    public GameObject[] mino;

    // Start is called before the first frame update
    void Start()
    {
        minoNext = Random.Range(0, 7);
        Spawn();
    }

    public void Spawn()
    {
       
        Instantiate(groups[minoNext], transform.position, Quaternion.identity);

        minoNext = Random.Range(0, 7);

        for (int i = 0; i < mino.Length; i++)
        {
            mino[i].SetActive(false);
        }

        mino[minoNext].SetActive(true);

    }
}
