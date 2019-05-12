using UnityEngine;

public class Starter : MonoBehaviour {

    [SerializeField]
    private Manager manager;

    [SerializeField]
    private Vector3 centralPosition = new Vector3(0, 0, 0);
    [SerializeField]
    private Vector3 leftPosition = new Vector3(0, 0, 0);
    [SerializeField]
    private Vector3 rightPosition = new Vector3(0, 0, 0);

    public void ClickOnSingle() {
        gameObject.SetActive(false);
        Instantiate(manager).Initialize(centralPosition);
    }

    public void ClickOnMulti() {
        gameObject.SetActive(false);
        Instantiate(manager).Initialize(leftPosition);
        Instantiate(manager).Initialize(rightPosition);
    }
}