using NGS.ExtendableSaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Table : MonoBehaviour
{
    static public Table instance;
    [SerializeField] private int column = 4;
    [SerializeField] private int row = 4;
    [SerializeField] private int[][] numberTable;
    [SerializeField] private List<BoxSlot> boxes;
    public int score = 0;
    public int bestScore = 0;
    public int Column { get => column; set => column = value; }
    public int Row { get => row; set => row = value; }
    public int[][] NumberTable { get => numberTable; set => numberTable = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        initDataTable();
    }
    public void initDataTable()
    {
        numberTable = new int[row][];
        for (int i = 0; i < row; ++i)
        {
            numberTable[i] = new int[column]; // Khởi tạo từng hàng trong mảng mảng
        }
        score = 0;
    }
    public void InitTable()
    {
        initDataTable();
        for (int i = 0; i < Row; ++i)
        {
            for (int j = 0;j < Column; ++j)
            {
                numberTable[i][j] = 0;
            }
        }
        UIManager.instance.UpdateTableUI();
        AppearNumber();
    }

    public void AppearNumber()
    {
        if (checkEndGame())
        {
            GameManager.instance.GameOver();
            return;
        }
        int number;
        int randomRate = 8 ;

        if (Random.Range(0, 10) < randomRate)
            number = 2;
        else
            number = 4;


        bool check = false;
        do
        {
            int n_column = Random.Range(0, Column);
            int n_row = Random.Range(0, Row);
            if (numberTable[n_row][n_column] == 0)
            {
                numberTable[n_row][n_column] = number;
                check = true;
                boxes[n_row * column  + n_column].UpdateBox(numberTable[n_row][n_column].ToString());
            }
        }
        while (!check);
    }
    public bool checkEndGame()
    {
        for (int i = 0; i < Row; ++i)
        {
            for (int j = 0; j < Column; ++j)
            {
                if (numberTable[i][j] == 0)
                    return false ;
            }
        }

        return true;
    }
    public void CheckBestScore()
    {
        if (score > bestScore)
            bestScore = score;
    }
    public void Swap(int row1, int col1, int row2, int col2)
    {
        int temp = numberTable[row1][col1];
        numberTable[row1][col1] = numberTable[row2][col2];
        numberTable[row2][col2] = temp;
    }
    void SortAfterRightDrag()
    {
        for (int i = 0; i < Row; ++i)
        {
            for (int j = Column - 2; j >= 0; --j)
            {
                if (numberTable[i][j] != 0)
                {
                    for (int k = Column - 1; k > j; --k)
                    {
                        if (numberTable[i][k] == 0)
                        {
                            Swap(i, j, i, k);
                            boxes[i*column + j].MoveEffect(boxes[i*column + k].transform.position, 0.5f);
                            break;
                        }
                    }
                }
            }
        }
    }
    void SortAfterLeftDrag()
    {
        for (int i = 0; i < Row; ++i)
        {
            for (int j = 1; j < Column; ++j)
            {
                if (numberTable[i][j] != 0)
                {
                    for (int k = 0; k < j; ++k)
                    {
                        if (numberTable[i][k] == 0)
                        {
                            Swap(i, j, i, k);
                            boxes[i * column + j].MoveEffect(boxes[i * column + k].transform.position, 0.5f);
                            break;
                        }
                    }
                }
            }
        }
    }
    void SortAfterDownDrag()
    {
        for (int i = 0; i < Column; ++i)
        {
            for (int j = Row - 2; j >= 0; --j)
            {
                if (numberTable[j][i] != 0)
                {
                    for (int k = Row - 1; k > j; --k)
                    {
                        if (numberTable[k][i] == 0)
                        {
                            Swap(j, i, k, i);
                            boxes[j * column + i].MoveEffect(boxes[k * column + i].transform.position, 0.5f);
                            break;
                        }
                    }
                }
            }
        }
    }
    void SortAfterUPDrag()
    {
        for (int i = 0; i < Column; ++i)
        {
            for (int j = 1; j < Row; ++j)
            {
                if (numberTable[j][i] != 0)
                {
                    for (int k = 0; k < j; ++k)
                    {
                        if (numberTable[k][i] == 0)
                        {
                            Swap(j, i, k, i);
                            boxes[j * column + i].MoveEffect(boxes[k * column + i].transform.position, 0.5f);
                            break;
                        }
                    }
                }
            }
        }
    }
    public void RightDrag()
    {
        for (int i = 0; i < Row; ++i)
        {
            for (int j = Column-1; j>= 0; --j)
            {
                if (numberTable[i][j] == 0)
                    continue;
                else 
                {
                    for (int k = j-1; k >= 0; --k)
                    {
                        if (numberTable[i][k] == 0) // neu o ben canh trong thi bo qa
                            continue;
                        else if (numberTable[i][k] == numberTable[i][j]) // neu co o cung hang co cung gia tri -> cong vao va dua o kia ve 0
                        {
                            numberTable[i][j] += numberTable[i][k];
                            if (numberTable[i][j] > score)
                            {
                                score = numberTable[i][j];
                            }
                            boxes[i * column + k].MoveEffect(boxes[i * column + j].transform.position, 0.5f);
                            numberTable[i][k] = 0;
                            boxes[i * column + k].EmptyBox();
                            boxes[i * column + j].UpdateBox(numberTable[i][j].ToString());
                            j = k+1;
                        }
                        else
                        {
                            j = k+1;
                            break;
                        }
                    }
                }
            }
        }
        SortAfterRightDrag();
       
    }
    public void LeftDrag()
    {
        for (int i = 0; i < Row; ++i)
        {
            for (int j = 0; j < Column; ++j)
            {
                if (numberTable[i][j] == 0)
                    continue;
                else
                {
                    for (int k = j+1; k < Column; ++k)
                    {
                        if (numberTable[i][k] == 0)
                            continue;
                        else if (numberTable[i][k] == numberTable[i][j])
                        {
                            numberTable[i][j] += numberTable[i][k];
                            if (numberTable[i][j] > score)
                            {
                                score = numberTable[i][j];
                            }
                            boxes[i * column + k].MoveEffect(boxes[i * column + j].transform.position, 0.5f);
                            numberTable[i][k] = 0;
                            boxes[i * column + k].EmptyBox();
                            boxes[i * column + j].UpdateBox(numberTable[i][j].ToString());
                            j = k-1;
                        }
                        else
                        {
                            j = k - 1;
                            break;
                        }
                    }
                }
            }
        }
        SortAfterLeftDrag();
    }
    public void UpDrag()
    {
        for (int i = 0; i < Column; ++i)
        {
            for (int j = 0; j<Row; ++j)
            {
                if (numberTable[j][i] == 0)
                    continue;
                else
                {
                    for (int k = j+1; k < Row; ++k)
                    {
                        if (numberTable[k][i] == 0)
                            continue;
                        else if (numberTable[k][i] == numberTable[j][i])
                        {
                            numberTable[j][i] += numberTable[k][i];
                            if (numberTable[j][i] > score)
                            {
                                score = numberTable[j][i];
                            }
                            boxes[j * column + i].MoveEffect(boxes[j * column + i].transform.position, 0.5f);
                            numberTable[k][i] = 0;
                            boxes[k * column + i].EmptyBox();
                            boxes[j * column + i].UpdateBox(numberTable[j][i].ToString());
                            j = k - 1;
                        }
                        else
                        {
                            j = k - 1;
                            break;
                        }
                    }
                }
            }
        }
        SortAfterUPDrag();
    }
    public void DownDrag()
    {
        for (int i = 0; i < Column; ++i)
        {
            for (int j = Row - 1; j >= 0; --j)
            {
                if (numberTable[j][i] == 0)
                    continue;
                else
                {
                    for (int k = j - 1; k >= 0; --k)
                    {
                        if (numberTable[k][i] == 0)
                            continue;
                        else if (numberTable[k][i] == numberTable[j][i])
                        {
                            numberTable[j][i] += numberTable[k][i];
                            if (numberTable[j][i] > score)
                            {
                                score = numberTable[j][i];
                            }
                            boxes[j * column + i].MoveEffect(boxes[j * column + i].transform.position, 0.5f);
                            numberTable[k][i] = 0;
                            boxes[k * column + i].EmptyBox();
                            boxes[j * column + i].UpdateBox(numberTable[j][i].ToString());
                            j = k + 1;
                        }
                        else
                        {
                            j = k + 1;
                            break;
                        }
                    }
                }
            }
        }
        SortAfterDownDrag();
    }

}
