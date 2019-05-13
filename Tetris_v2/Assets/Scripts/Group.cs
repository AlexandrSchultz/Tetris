using UnityEngine;
using UnityEngine.SceneManagement;

public class Group : MonoBehaviour {
    private float m_lastFall;
    private Vector3 m_vector;
    private const int MaxOffset = 2;
    private int m_offset;
    private Vector3 m_startPosition;
    private Transform m_transform;
    [SerializeField]
    public bool rotate;

    private Preview m_preview;
    private Score m_score;
    private GridGame m_gridGame;
    private bool m_doubleControl;

    //провекра дочерних блоков(их позиции внутри сетки/вне сетки)
    private bool IsValidGridPos() {
        foreach (Transform child in transform) {
            Vector2 v = m_gridGame.RoundVec2(child.position - m_gridGame.Container.position); //позиция одной миношки(одного блока)

            //проверка на то что блок внутри границы
            if (!m_gridGame.InsideBorder(v))
                return false;

            //блок в сетке
            if (m_gridGame.GridForMinos[(int) v.x, (int) v.y] != null && m_gridGame.GridForMinos[(int) v.x, (int) v.y].parent != transform)
                return false;
        }
        return true;
    }

    public void Initialize(Score score, Preview preview, GridGame gridGame, bool doubleControl) {
        m_score = score;
        m_preview = preview;
        m_gridGame = gridGame;
        m_doubleControl = doubleControl;
    }

    //
    private void UpdateGrid() {
        //удаление старых дочерних блоков
        for (int y = 0; y < GridGame.H; ++y) {
            for (int x = 0; x < GridGame.W; ++x) {
                if (m_gridGame.GridForMinos[x, y] != null) {
                    if (m_gridGame.GridForMinos[x, y].parent == transform) {
                        m_gridGame.GridForMinos[x, y] = null;
                    }
                }
            }
        }
        //добавление новых
        foreach (Transform child in transform) {
            Vector2 v = m_gridGame.RoundVec2(child.position - m_gridGame.Container.position);
            m_gridGame.GridForMinos[(int) v.x, (int) v.y] = child;
        }
    }

    // Start is called before the first frame update
    private void Start() {
        if (!IsValidGridPos()) {
            Destroy(gameObject);
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        Initialize(m_score, m_preview, m_gridGame, m_doubleControl);
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
        (m_transform = transform).Rotate(0, 0, -90);
        m_startPosition = m_transform.position;
        if (IsValidGridPos()) {
            UpdateGrid();
        } else if (!IsValidGridPos()) {
            //Пробуем сдвинуть вправо или влево поочерёдно
            for (m_offset = 1; m_offset <= MaxOffset; m_offset++) {
                transform.position = m_startPosition - new Vector3(m_offset, 0, 0);
                if (IsValidGridPos())
                    return;
                transform.position = m_startPosition + new Vector3(m_offset, 0, 0);
                if (IsValidGridPos())
                    return;
            }
        } else {
            transform.position = m_startPosition;
            transform.Rotate(0, 0, 90);
        }
    }

    private void Update() {
        if (!m_doubleControl) {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) { //влево с залипанием стрелки
                m_vector = Vector3.left;
                StartCoroutine(Sticking.StickingKey(KeyCode.LeftArrow, Move));
            }

            if (Input.GetKeyDown(KeyCode.RightArrow)) { //вправо со стрелки
                m_vector = Vector3.right;
                StartCoroutine(Sticking.StickingKey(KeyCode.RightArrow, Move));
            }

            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                m_vector = Vector3.down;
                StartCoroutine(Sticking.StickingKey(KeyCode.DownArrow, Move));
            }

            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                if (rotate) {
                    Rotate();
                }
            }
        }

        if (m_doubleControl) {
            if (Input.GetKeyDown(KeyCode.A)) { //влево с А
                m_vector = Vector3.left;
                StartCoroutine(Sticking.StickingKey(KeyCode.A, Move));
            }

            if (Input.GetKeyDown(KeyCode.D)) { //вправо с D
                m_vector = Vector3.right;
                StartCoroutine(Sticking.StickingKey(KeyCode.D, Move));
            }

            if (Input.GetKeyDown(KeyCode.S)) {
                m_vector = Vector3.down;
                StartCoroutine(Sticking.StickingKey(KeyCode.S, Move));
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                if (rotate) {
                    Rotate();
                }
            }
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
                m_gridGame.DeleteFullRows();

                //заспавнить новую группу
                m_preview.Spawn();

                //выключить скрипт
                enabled = false;

                foreach (Transform child in GetComponentsInChildren<Transform>()) {
                    child.transform.parent = m_gridGame.Container;
                }

                Destroy(gameObject);
            }
            m_lastFall = Time.time;
        }
    }
}