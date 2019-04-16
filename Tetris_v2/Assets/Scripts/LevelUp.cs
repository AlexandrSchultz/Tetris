using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    public static float fallSpeed = 1.0f;
    public static int currentLevel;
    Grid gridObj = new Grid();

    public Text lvl;
    //public Text lines;

    public delegate void Click();
    //события для уменьшения/увелечения уровня 
    public event Click Plus = delegate { };
    public event Click Minus = delegate { };


    void Start()
    {
        Plus += Up;
        Minus += Down;
    }

    void UpdateLevel()
    {
        currentLevel = gridObj.LineCount / 5;
    }

    public void Up()
    {
        currentLevel++;
    }

    public void Down()
    {
        currentLevel--;
    }


    void UpdateSpeed()
    {
        fallSpeed = 1.0f - ((float)currentLevel * 0.099f);
    }

    private void UpdateUI()
    {
        lvl.text = currentLevel.ToString();
    }


    private void Update()
    {
        UpdateLevel();
        UpdateSpeed();
        UpdateUI();

        if (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Plus();
        }

        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Minus();
        }
    }
}
