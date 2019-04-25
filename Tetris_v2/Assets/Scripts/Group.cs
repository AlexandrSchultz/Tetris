using UnityEngine;
using UnityEngine.SceneManagement;

public class Group : MonoBehaviour {
    private float m_lastFall;
    private Vector3 m_vector;
    private Vector3 m_reverseVector;

    //провекра дочерних блоков(их позиции внутри сетки/вне сетки)
    private bool IsValidGridPos() {
        foreach (Transform child in transform) {
            Vector2 v = Grid.RoundVec2(child.position); //позиция одной миношки(одного блока)

            //проверка на то что блок внутри границы
            if (!Grid.InsideBorder(v))
                return false;

            //блок в сетке
            if (Grid.GridForMinos[(int) v.x, (int) v.y] != null && Grid.GridForMinos[(int) v.x, (int) v.y].parent != transform)
                return false;
        }
        return true;
    }

    //
    private void UpdateGrid() {
        //удаление старых дочерних блоков
        for (int y = 0; y < Grid.H; ++y) {
            for (int x = 0; x < Grid.W; ++x) {
                if (Grid.GridForMinos[x, y] != null) {
                    if (Grid.GridForMinos[x, y].parent == transform) {
                        Grid.GridForMinos[x, y] = null;
                    }
                }
            }
        }
        //добавление новых
        foreach (Transform child in transform) {
            Vector2 v = Grid.RoundVec2(child.position);
            Grid.GridForMinos[(int) v.x, (int) v.y] = child;
        }
    }

    // Start is called before the first frame update
    private void Start() {
        if (!IsValidGridPos()) {
            Destroy(gameObject);
            SceneManager.LoadScene(sceneBuildIndex: 1, LoadSceneMode.Single);
        }
    }

/*
    private void Left() {
        //обновление позиции
        transform.position += Vector3.left;

        //проверка
        if (IsValidGridPos()) {
            //обновление сетки 
            UpdateGrid();
        } else {
            //если false вернуться 
            transform.position += Vector3.right;
        }
    }

    private void Right() {
        transform.position += Vector3.right;

        if (IsValidGridPos()) {
            UpdateGrid();
        } else {
            transform.position += Vector3.left;
        }
    }

    private void Down() {
        //изменить позицию
        transform.position += Vector3.down;

        //проверка
        if (IsValidGridPos()) {
            UpdateGrid();
        } else {
            //возвращает позицию если проверка не true
            transform.position += Vector3.up;
        }
    }
*/
    private void Move() {
        //изменить позицию
        transform.position += m_vector;

        //проверка
        if (IsValidGridPos()) {
            UpdateGrid();
        } else {
            //возвращает позицию если проверка не true
            transform.position += m_reverseVector;
        }
    }

    private void Rotate() {
        transform.Rotate(0, 0, -90);
        if (IsValidGridPos()) {
            UpdateGrid();
        } else {
            transform.Rotate(0, 0, 90);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { //влево с залипанием стрелки
            m_vector = Vector3.left;
            m_reverseVector = Vector3.right;
            StartCoroutine(Sticking.StickingKey(KeyCode.LeftArrow, Move));
        }

        if (Input.GetKeyDown(KeyCode.A)) { //влево с А
            m_vector = Vector3.left;
            m_reverseVector = Vector3.right;
            StartCoroutine(Sticking.StickingKey(KeyCode.A, Move));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) { //вправо со стрелки
            m_vector = Vector3.right;
            m_reverseVector = Vector3.left;
            StartCoroutine(Sticking.StickingKey(KeyCode.RightArrow, Move));
        }

        if (Input.GetKeyDown(KeyCode.D)) { //вправо с D
            m_vector = Vector3.right;
            m_reverseVector = Vector3.left;
            StartCoroutine(Sticking.StickingKey(KeyCode.D, Move));
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            m_vector = Vector3.down;
            m_reverseVector = Vector3.up;
            StartCoroutine(Sticking.StickingKey(KeyCode.DownArrow, Move));
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            m_vector = Vector3.down;
            m_reverseVector = Vector3.up;
            StartCoroutine(Sticking.StickingKey(KeyCode.S, Move));
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            Rotate();
        }

        //падение
        if (Time.time - m_lastFall >= 1.0f - (Score.CurrentLevel * 0.17f)) {
            //изменить позицию
            transform.position += Vector3.down;

            //проверка
            if (IsValidGridPos()) {
                UpdateGrid();
            } else {
                //возвращает позицию если проверка не true
                transform.position += Vector3.up;

                //Удалить заполненные горизонтальные линии
                Grid.DeleteFullRows();

                //заспавнить новую группу
                FindObjectOfType<Preview>().Spawn();

                //выключить скрипт
                enabled = false;
            }
            m_lastFall = Time.time;
        }
    }
}