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

    private bool m_oneControl = true;
    private bool m_twoControl = true;
    private bool m_threeControl = true;

    public void ClickOnSingle() {
        gameObject.SetActive(false);
        Instantiate(manager).Initialize(centralPosition, m_oneControl, m_twoControl, m_threeControl);
    }

    public void ClickOnGameForTwo() {
        gameObject.SetActive(false);
        Instantiate(manager).Initialize(leftPosition, m_oneControl = false, m_twoControl = true, m_threeControl = false);
        Instantiate(manager).Initialize(rightPosition, m_oneControl = true, m_twoControl = false, m_threeControl = false);
    }

    public void ClickOnMulti() {
        gameObject.SetActive(false);
        Instantiate(manager).Initialize(leftPosition, m_oneControl = false, m_twoControl = true, m_threeControl = false);
        Instantiate(manager).Initialize(centralPosition, m_oneControl = true, m_twoControl = false, m_threeControl = false);
        Instantiate(manager).Initialize(rightPosition, m_oneControl = false, m_twoControl = false, m_threeControl = true);
    }
}