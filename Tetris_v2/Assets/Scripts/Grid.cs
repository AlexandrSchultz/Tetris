using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{

    public static int w = 10;
    public static int h = 20;
    public static Transform[,] grid = new Transform[w,h];

    public delegate void LineOnField(int lineCount);
    public static event LineOnField LineFull;

    //переменная для подсчёта количества удаляемых линий
    //public static int numberOfRowThisTurn = 0;
    public static int lineCount = 0;

    //функция округления координат
    public static Vector2 roundVec2 (Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }
    
    //функция для проверки находится ли координаты между границами или за её пределами
    public static bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < w && (int)pos.y >= 0);
    }
    
    //удаление заполненной линии
    public static void deleteRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    //падение вышестоящих фигур
    public static void decreaseRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != null)
            {
                //перемещение вниз
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                //обновляет позицию блоков
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    //предыдущую функцию на все линии 
    public static void decreaseRowsAbove(int y)
    {
        for (int i = y; i < h; ++i)
            decreaseRow(i);
    }

    //функция проверки заполнения строки
    public static bool isRowFull(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] == null)
                return false;
        }

        return true;
    }

    //функция удаления всех заполненых линий
    public static void deleteFullRows()
    {
        for (int y = 0; y < h; ++y)
        {
            if (isRowFull(y))
            {
                deleteRow(y);
                decreaseRowsAbove(y + 1);
                --y;
                lineCount++;
            }
        }
        LineFull(lineCount);
    }
}
