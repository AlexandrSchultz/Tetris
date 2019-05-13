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

    private bool m_doubleControl;

    public void ClickOnSingle() {
        gameObject.SetActive(false);
        Instantiate(manager).Initialize(centralPosition, m_doubleControl);
    }

    public void ClickOnMulti() {
        gameObject.SetActive(false);
        Instantiate(manager).Initialize(leftPosition, m_doubleControl = true);
        Instantiate(manager).Initialize(rightPosition, m_doubleControl = false);
    }
}