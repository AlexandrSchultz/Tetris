using UnityEngine;

public class Group : MonoBehaviour
{
    public float lastFall = 0;
    private float a = 1;
    
    //провекра дочерних блоков
    private bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);//позиция одной миношки(одного блока)

            //проверка на то что блок внутри границы
            if (!Grid.insideBorder(v))
                return false;

            //блок в сетке
            if (Grid.grid[(int)v.x, (int)v.y] != null && Grid.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }

    //
    private void updateGrid()
    {
        //удаление старых дочерних блоков
        for (int y = 0; y < Grid.h; ++y)
            for (int x = 0; x < Grid.w; ++x)
                if (Grid.grid[x, y] != null)
                    if (Grid.grid[x, y].parent == transform)
                        Grid.grid[x, y] = null;

        //добавление новых
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = child;
        }
    }

    //


    // Start is called before the first frame update
    void Start()
    {
        Speed();

        if (!isValidGridPos())
        {
            Debug.Log("GAME OVER");
            Destroy(gameObject);
            Application.LoadLevel("GameOver");
        }
    }

    private void Left()
    {
        //обновление позиции
        transform.position += new Vector3(-1, 0, 0);

        //проверка
        if (isValidGridPos())
        {
            //обновление сетки 
            updateGrid();
        }

        else
        {
            //если false вернуться 
            transform.position += new Vector3(1, 0, 0);
        }

    }

    private void Right()
    {
        transform.position += new Vector3(1, 0, 0);

        if (isValidGridPos())
        {
            updateGrid();
        }

        else
        {
            transform.position += new Vector3(-1, 0, 0);
        }
    }

    private void Down()
    {
        //изменить позицию
            transform.position += new Vector3(0, -1, 0);            
          

            //проверка
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
            {
                //возвращает позицию если проверка не true
                transform.position += new Vector3(0, 1, 0);
            }
    }

    private void Rotate()
    {
        transform.Rotate(0, 0, -90);
        if (isValidGridPos())
        {
            updateGrid();
        }

        else
        {
            transform.Rotate(0, 0, 90);
        }
    }

    
    private void Speed()
    {
        a = a*LevelUp.fallSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))//влево с залипанием стрелки
        {
            StartCoroutine(Sticking.StickingKey(KeyCode.LeftArrow, Left));
        }

        if (Input.GetKeyDown(KeyCode.A))//влево с А
        {
            StartCoroutine(Sticking.StickingKey(KeyCode.A, Left));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))//вправо со стрелки
        {
            StartCoroutine(Sticking.StickingKey(KeyCode.RightArrow, Right));
        }

        if (Input.GetKeyDown(KeyCode.D))//вправо с D
        {
            StartCoroutine(Sticking.StickingKey(KeyCode.D, Right));
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(Sticking.StickingKey(KeyCode.DownArrow, Down));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(Sticking.StickingKey(KeyCode.S, Down));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rotate();
        }
            

        //падение
        if (Time.time - lastFall >= 1*a)
        {
            //изменить позицию
            transform.position += new Vector3(0, -1, 0);

            //проверка
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
            {
                //возвращает позицию если проверка не true
                transform.position += new Vector3(0, 1, 0);

                //Удалить заполненные горизонтальные линии
                Grid.deleteFullRows();

                //заспавнить новую группу
                FindObjectOfType<Preview>().Spawn();

                //выключить скрипт
                enabled = false;
            }
            lastFall = Time.time;
        }
    }
}
