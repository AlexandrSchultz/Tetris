using System.Collections.Generic;
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

    private Dictionary<ControlType, KeyCode> m_keysWASD = new Dictionary<ControlType, KeyCode> {
        {ControlType.Left, KeyCode.A},
        {ControlType.Right, KeyCode.D},
        {ControlType.Down, KeyCode.S},
        {ControlType.Rotate, KeyCode.Space}
    };

    private Dictionary<ControlType, KeyCode> m_keysArrow = new Dictionary<ControlType, KeyCode> {
        {ControlType.Left, KeyCode.LeftArrow},
        {ControlType.Right, KeyCode.RightArrow},
        {ControlType.Down, KeyCode.DownArrow},
        {ControlType.Rotate, KeyCode.UpArrow}
    };

    private Dictionary<ControlType, KeyCode> m_keysKeypad = new Dictionary<ControlType, KeyCode> {
        {ControlType.Left, KeyCode.Keypad4},
        {ControlType.Right, KeyCode.Keypad6},
        {ControlType.Down, KeyCode.Keypad2},
        {ControlType.Rotate, KeyCode.Keypad8}
    };

    public void ClickOnSingle() {
        gameObject.SetActive(false);
        Instantiate(manager).Initialize(centralPosition, new List<Dictionary<ControlType, KeyCode>> {
            m_keysWASD, m_keysArrow, m_keysKeypad
        });
    }

    public void ClickOnGameForTwo() {
        gameObject.SetActive(false);
        Instantiate(manager).Initialize(leftPosition, new List<Dictionary<ControlType, KeyCode>> {
            m_keysWASD
        });
        Instantiate(manager).Initialize(rightPosition, new List<Dictionary<ControlType, KeyCode>> {
            m_keysArrow
        });
    }

    public void ClickOnMulti() {
        gameObject.SetActive(false);
        Instantiate(manager).Initialize(leftPosition, new List<Dictionary<ControlType, KeyCode>> {
            m_keysWASD
        });
        Instantiate(manager).Initialize(centralPosition, new List<Dictionary<ControlType, KeyCode>> {
            m_keysArrow
        });
        Instantiate(manager).Initialize(rightPosition, new List<Dictionary<ControlType, KeyCode>> {
            m_keysKeypad
        });
    }
}