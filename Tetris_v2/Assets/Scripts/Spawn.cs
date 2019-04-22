using UnityEngine;

public class Spawn : MonoBehaviour {
    public GameObject[] groups;

    private void SpawnNext() {
        int i = Random.Range(0, groups.Length);
        Instantiate(groups[i], transform.position, Quaternion.identity);
    }

    // Start is called before the first frame update
    private void Start() {
        SpawnNext();
    }
}