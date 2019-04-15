using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Dictionary<int, int> scoreByLineCount = new Dictionary<int, int>
    {
        {1, 100},
        {2, 300},
        {3, 700},
        {4, 1500},
    };

    private int numLinesCleared = 0;

    public Text scoreText;
    public Text lineText;

    private static int currentScore;

    private void Awake()
    {
        //подписка на событие методом, который будет исполнятся по заполнению линии
        Grid.LineFull += ClearedLine;
    }

    //функция срабатывающая при удалении одной линии
    private void ClearedLine(int lineCount)
    {
        if (!scoreByLineCount.ContainsKey(lineCount))
        {
            return;
        }
        //при удалении одной линии к текущему счёту прибавляется счёт за одну линию(далее аналогично)
        currentScore += scoreByLineCount[lineCount];
        numLinesCleared = lineCount;
        Debug.Log(numLinesCleared);
    }

    //отображение счёта 
    public void UpdateUI()
    {
        scoreText.text = currentScore.ToString();
        lineText.text = numLinesCleared.ToString();
    }

    private void Update()
    {
        UpdateUI();
    }
}
