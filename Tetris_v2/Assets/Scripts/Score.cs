using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    private readonly Dictionary<int, int> m_scoreByLineCount = new Dictionary<int, int> {
        {1, 100},
        {2, 300},
        {3, 700},
        {4, 1500},
    };

    public delegate void Click();

    //события для уменьшения/увелечения уровня 
    public event Click Plus = delegate { };

    public event Click Minus = delegate { };

    private int m_numLinesCleared;
    private static int m_currentLevel;
    private static int m_currentScore;
    private int m_countLine;
    private float m_fallSpeed;

    private GridGame m_gridGame;

    public static int CurrentLevel {
        get {
            return m_currentLevel;
        }
    }

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text lineText;
    [SerializeField]
    private Text lvl;

    public void Initialize(GridGame gridGame) {
        m_gridGame = gridGame;
        //подписка на событие методом, который будет исполнятся по заполнению линии
        m_gridGame.LineFull += ClearedLine;

        Plus += Up;
        Minus += Down;
    }

    //функция срабатывающая при удалении одной линии
    private void ClearedLine(int lineCount) {
        if (!m_scoreByLineCount.ContainsKey(lineCount)) {
            return;
        }
        //при удалении одной линии к текущему счёту прибавляется счёт за одну линию(далее аналогично)
        m_currentScore += m_scoreByLineCount[lineCount];
        m_numLinesCleared += lineCount;
        m_countLine += lineCount;
        if (m_countLine >= 5) {
            Up();
            m_countLine = 0;
        }
        UpdateUI();
    }

    private void Up() {
        m_currentLevel++;
        UpdateUI();
    }

    private void Down() {
        m_currentLevel--;
        UpdateUI();
    }

    //отображение счёта 
    private void UpdateUI() {
        scoreText.text = m_currentScore.ToString();
        lineText.text = m_numLinesCleared.ToString();
        lvl.text = m_currentLevel.ToString();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadPlus)) {
            Plus();
        }

        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus)) {
            Minus();
        }
    }
}