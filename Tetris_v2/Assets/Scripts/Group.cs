using UnityEngine;
using UnityEngine.SceneManagement;

public class Group : MonoBehaviour {
    private float m_lastFall;
    private Vector3 m_vector;

    //провекра дочерних блоков(их позиции внутри сетки/вне сетки)
    private bool IsValidGridPos() {
        foreach (Transform child in transform) {
            Vector2 v = GridGame.RoundVec2(child.position); //позиция одной миношки(одного блока)

            //проверка на то что блок внутри границы
            if (!GridGame.InsideBorder(v))
                return false;

            //блок в сетке
            if (GridGame.GridForMinos[(int) v.x, (int) v.y] != null && GridGame.GridForMinos[(int) v.x, (int) v.y].parent != transform)
                return false;
        }
        return true;
    }

    //
    private void UpdateGrid() {
        //удаление старых дочерних блоков
        for (int y = 0; y < GridGame.H; ++y) {
            for (int x = 0; x < GridGame.W; ++x) {
                if (GridGame.GridForMinos[x, y] != null) {
                    if (GridGame.GridForMinos[x, y].parent == transform) {
                        GridGame.GridForMinos[x, y] = null;
                    }
                }
            }
        }
        //добавление новых
        foreach (Transform child in transform) {
            Vector2 v = GridGame.RoundVec2(child.position);
            GridGame.GridForMinos[(int) v.x, (int) v.y] = child;
        }
    }

    // Start is called before the first frame update
    private void Start() {
        if (!IsValidGridPos()) {
            Destroy(gameObject);
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }

    private void Move() {
        //изменить позицию
        transform.position += m_vector;

        //проверка
        if (IsValidGridPos()) {
            UpdateGrid();
        } else {
            //возвращает позицию если проверка не true
            transform.position -= m_vector;
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
            StartCoroutine(Sticking.StickingKey(KeyCode.LeftArrow, Move));
        }

        if (Input.GetKeyDown(KeyCode.A)) { //влево с А
            m_vector = Vector3.left;
            StartCoroutine(Sticking.StickingKey(KeyCode.A, Move));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) { //вправо со стрелки
            m_vector = Vector3.right;
            StartCoroutine(Sticking.StickingKey(KeyCode.RightArrow, Move));
        }

        if (Input.GetKeyDown(KeyCode.D)) { //вправо с D
            m_vector = Vector3.right;
            StartCoroutine(Sticking.StickingKey(KeyCode.D, Move));
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            m_vector = Vector3.down;
            StartCoroutine(Sticking.StickingKey(KeyCode.DownArrow, Move));
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            m_vector = Vector3.down;
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
                transform.position -= Vector3.down;

                //Удалить заполненные горизонтальные линии
                GridGame.DeleteFullRows();

                //заспавнить новую группу
                FindObjectOfType<Preview>().Spawn();

                //выключить скрипт
                enabled = false;
                
                foreach (Transform child in GetComponentsInChildren<Transform>()) {
                    child.transform.parent = Container.Instance.transform;
                }

                Destroy(gameObject);
            }
            m_lastFall = Time.time;
        }
    }
}