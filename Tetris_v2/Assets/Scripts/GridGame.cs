using UnityEngine;

public class GridGame : MonoBehaviour {

    public const int W = 10;
    public const int H = 20;
    public readonly Transform[,] GridForMinos = new Transform[W, H];

    [SerializeField]
    private Transform container;

    public Transform Container {
        get {
            return container;
        }
    }

    public delegate void LineOnField(int lineCount);

    public event LineOnField LineFull = delegate { };

    //переменная для подсчёта количества удаляемых линий
    private int m_lineCount;

    //функция округления координат
    public Vector2 RoundVec2(Vector2 v) {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    //функция для проверки находится ли координаты между границами или за её пределами
    public bool InsideBorder(Vector2 pos) {
        return ((int) pos.x >= 0 && (int) pos.x < W && (int) pos.y >= 0);
    }

    // 1-ый вспомогательный для DeleteFullRows 
    //удаление заполненной линии
    private void DeleteRow(int y) {
        for (int x = 0; x < W; ++x) {
            Destroy(GridForMinos[x, y].gameObject);
            GridForMinos[x, y] = null;
        }
    }

    // вспомогательный для DecreaseRowsAbove 
    //падение вышестоящих фигур
    private void DecreaseRow(int y) {
        for (int x = 0; x < W; ++x) {
            if (GridForMinos[x, y] != null) {
                //перемещение вниз
                GridForMinos[x, y - 1] = GridForMinos[x, y];
                GridForMinos[x, y] = null;

                //обновляет позицию блоков
                GridForMinos[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    // 2-ый вспомогательный для DeleteFullRows
    //предыдущую функцию на все линии 
    private void DecreaseRowsAbove(int y) {
        for (int i = y; i < H; ++i)
            DecreaseRow(i);
    }

    // 1-ый вспомогательный для DeleteFullRows
    //функция проверки заполнения строки
    private bool IsRowFull(int y) {
        for (int x = 0; x < W; ++x) {
            if (GridForMinos[x, y] == null)
                return false;
        }

        return true;
    }

    //функция удаления всех заполненых линий
    public void DeleteFullRows() {
        for (int y = 0; y < H; ++y) {
            if (IsRowFull(y)) {
                DeleteRow(y);
                DecreaseRowsAbove(y + 1);
                --y;
                m_lineCount++;
            }
        }
        LineFull(m_lineCount);
        m_lineCount = 0;
    }
}