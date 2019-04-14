using UnityEngine;
using UnityEngine.UI;

public class LinesClear : MonoBehaviour
{
    public Text lines;

    private void UpdateUI()
    {
        lines.text = Score.numLinesCleared.ToString();
    }

   
    void Update()
    {
        UpdateUI();
    }
}
