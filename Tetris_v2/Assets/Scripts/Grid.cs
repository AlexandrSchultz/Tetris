using UnityEngine;

public class Grid : MonoBehaviour {

    public const int W = 10;
    public const int H = 20;
    public static readonly Transform[,] Ggrid = new Transform[W, H];

    public delegate void LineOnField(int lineCount);

    public static event LineOnField LineFull = delegate { };

    //переменная для подсчёта количества удаляемых линий
    private static int m_lineCount;

    //функция округления координат
    public static Vector2 roundVec2(Vector2 v) {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    //функция для проверки находится ли координаты между границами или за её пределами
    public static bool insideBorder(Vector2 pos) {
        return ((int) pos.x >= 0 && (int) pos.x < W && (int) pos.y >= 0);
    }

    //удаление заполненной линии
    private static void deleteRow(int y) {
        for (int x = 0; x < W; ++x) {
            Destroy(Ggrid[x, y].gameObject);
            Ggrid[x, y] = null;
        }
    }

    //падение вышестоящих фигур
    private static void decreaseRow(int y) {
        for (int x = 0; x < W; ++x) {
            if (Ggrid[x, y] != null) {
                //перемещение вниз
                Ggrid[x, y - 1] = Ggrid[x, y];
                Ggrid[x, y] = null;

                //обновляет позицию блоков
                Ggrid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    //предыдущую функцию на все линии 
    private static void decreaseRowsAbove(int y) {
        for (int i = y; i < H; ++i)
            decreaseRow(i);
    }

    //функция проверки заполнения строки
    private static bool isRowFull(int y) {
        for (int x = 0; x < W; ++x) {
            if (Ggrid[x, y] == null)
                return false;
        }

        return true;
    }

    //функция удаления всех заполненых линий
    public static void deleteFullRows() {
        for (int y = 0; y < H; ++y) {
            if (isRowFull(y)) {
                deleteRow(y);
                decreaseRowsAbove(y + 1);
                --y;
                m_lineCount++;
            }
        }
        LineFull(m_lineCount);
        m_lineCount = 0;
    }
}