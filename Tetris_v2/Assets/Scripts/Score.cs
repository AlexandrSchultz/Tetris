using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    
    public static int numLinesCleared = 0;

    //Score
    public int scoreOniLine = 100;//счёт за 1 линию
    public int scoreTwoLine = 300;//счёт за 2линии
    public int scoreThreeLine = 700;//счёт за 3 линии
    public int scoreFourLine = 1500;//счёт за 4 линии

    public Text scoreText;

    public static int currentScore;

    //обновление счёта
    public void UpdateScore()
    {
        if (Grid.numberOfRowThisTurn > 0)
        {
            //если переменная отвечающая за число линий равна 1, то вызывается функция которая к текущему счёту прибавить счёт за 1 линию(далее по анологии)
            if (Grid.numberOfRowThisTurn == 1)
            {
                ClearedOneLine();
            }
            else if (Grid.numberOfRowThisTurn == 2)
            {
                ClearedTwoLone();
            }
            else if (Grid.numberOfRowThisTurn == 3)
            {
                ClearedThreeLine();
            }
            else if (Grid.numberOfRowThisTurn == 4)
            {
                ClearedFourLine();
            }
            Grid.numberOfRowThisTurn = 0;
        }
    }

    //функция срабатывающая при удалении одной линии
    public void ClearedOneLine()
    {
        //при удалении одной линии к текущему счёту прибавляется счёт за одну линию(далее аналогично)
        currentScore += scoreOniLine;
        numLinesCleared++;
    }

    public void ClearedTwoLone()
    {
        currentScore += scoreTwoLine;
        numLinesCleared += 2;
    }

    public void ClearedThreeLine()
    {
        currentScore += scoreThreeLine;
        numLinesCleared += 3;
    }

    public void ClearedFourLine()
    {
        currentScore += scoreFourLine;
        numLinesCleared += 4;
    }

    //отоброжение счёта 
    public void UpdateUI()
    {
        scoreText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        UpdateUI();
        
    }
}
