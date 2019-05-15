using System.Collections.Generic;
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

    private List<Dictionary<ControlType, KeyCode>> m_controlKey;

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

    public void Initialize(Score score, Preview preview, GridGame gridGame, List<Dictionary<ControlType, KeyCode>> controlKey) {
        m_score = score;
        m_preview = preview;
        m_gridGame = gridGame;
        m_controlKey = controlKey;
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
        foreach (var key in m_controlKey) {
            if (Input.GetKeyDown(key[ControlType.Left])) { //влево с залипанием стрелки
                m_vector = Vector3.left;
                StartCoroutine(Sticking.StickingKey(key[ControlType.Left], Move));
            }

            if (Input.GetKeyDown(key[ControlType.Right])) { //вправо со стрелки
                m_vector = Vector3.right;
                StartCoroutine(Sticking.StickingKey(key[ControlType.Right], Move));
            }

            if (Input.GetKeyDown(key[ControlType.Down])) {
                m_vector = Vector3.down;
                StartCoroutine(Sticking.StickingKey(key[ControlType.Down], Move));
            }

            if (Input.GetKeyDown(key[ControlType.Rotate])) {
                if (rotate) {
                    Rotate();
                }
            }
        }

        //падение
        if (Time.time - m_lastFall >= 1.0f - (m_score.CurrentLevel * 0.17f)) {
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