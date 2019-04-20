using UnityEngine;

public class Spawn : MonoBehaviour {
    public GameObject[] groups;

    private void spawnNext() {
        int i = Random.Range(0, groups.Length);
        Instantiate(groups[i], transform.position, Quaternion.identity);
    }

    // Start is called before the first frame update
    private void Start() {
        spawnNext();
    }
}