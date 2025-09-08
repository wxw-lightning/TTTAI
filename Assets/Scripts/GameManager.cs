using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("Spawned GameManager", typeof(GameManager)).GetComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    public enum CellState { Empty, X, O }
    public enum Turn { Player, AI }
    public enum AILevel { Easy, Normal, Hard }

    public GameObject cellPrefab;
    public Sprite XSprite;
    public Sprite OSprite;
    public Transform boardParent;

    public bool gameOver = false;
    public Turn currentTurn;

    public Cell[,] cells = new Cell[3, 3];
    public CellState[,] board = new CellState[3, 3];

    private CellState playerMark;
    private CellState AIMark;
    private bool playerFirst;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Init()
    {
        if (boardParent.GetChild(0) != null) return;
        for(int row = 0; row < 3; row++)
        {
            for(int col = 0; col < 3; col++)
            {
                GameObject cellObj = Instantiate(cellPrefab, boardParent);
                Cell cell = cellObj.GetComponent<Cell>();
                cell.row = row;
                cell.col = col;
                cells[row, col] = cell;
            }
        }
        if (playerFirst)
        {
            playerMark = CellState.X;
            AIMark = CellState.O;
            currentTurn = Turn.Player;
        }
        else
        {
            playerMark= CellState.O;
            AIMark = CellState.X;
            currentTurn = Turn.AI;
        }
        gameOver = false;
    }

    public void PlayerMove(int row, int col)
    {
        if (gameOver) return;
        if (board[row, col] != CellState.Empty) return;

        Place(row, col, playerMark);
        if (CheckGameOver()) return;

        currentTurn = Turn.AI;
        //StartCoroutine(AIMoveRoutine());
    }

    private void Place(int row, int col, CellState who)
    {
        board[row, col] = who;
        if (cells[row, col] && cells[row, col].sr)
        {
            var sr = cells[row, col].sr;
            sr.sprite = (who == CellState.X) ? XSprite : OSprite;
            sr.color = Color.white;
        }
    }

    private bool CheckGameOver()
    {
        var winner = GetWinner(board);
        if (winner != CellState.Empty)
        {
            gameOver = true;
            return true;
        }
        if (IsFull(board))
        {
            gameOver = true;
            Debug.Log("Game Over! Draw.");
            return true;
        }
        return false;
    }

    private CellState GetWinner(CellState[,] board)
    {
        for (int row = 0; row < 3; row++)
        {
            if (board[row, 0] != CellState.Empty && board[row, 0] == board[row, 1] && board[row, 1] == board[row, 2]) return board[row, 0];
        }
        for (int col = 0; col < 3; col++)
        {
            if (board[0, col] != CellState.Empty && board[0, col] == board[1, col] && board[1, col] == board[2, col]) return board[0, col];
        }
        if (board[0, 0] != CellState.Empty && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) return board[0, 0];
        if (board[0, 2] != CellState.Empty && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]) return board[0, 2];

        return CellState.Empty;
    }

    private bool IsFull(CellState[,] board)
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (board[row, col] == CellState.Empty) return false;
            }
        }
        return true;
    }
}
