using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
                    _instance = new GameObject("GameManager", typeof(GameManager)).GetComponent<GameManager>();
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
    public GameObject GameOverMenu;
    public TextMeshProUGUI gameResult;

    public bool gameOver = false;
    public Turn currentTurn;

    public Cell[,] cells = new Cell[3, 3];
    public CellState[,] board = new CellState[3, 3];

    private CellState playerMark;
    private CellState AIMark;
    public AILevel aiLevel = AILevel.Normal;
    public bool playerFirst = true;

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

    public void InitBoard()
    {
        if (boardParent.childCount != 0) return;

        for (int row = 0; row < 3; row++)
        {
            for(int col = 0; col < 3; col++)
            {
                GameObject cellObj = Instantiate(cellPrefab, boardParent);
                cellObj.name = $"Cell_{row}_{col}";
                Cell cell = cellObj.GetComponent<Cell>();
                cell.row = row;
                cell.col = col;
                cells[row, col] = cell;
                if (cell.image != null)
                {
                    cell.image.sprite = null;
                }
            }
        }
        
    }

    public void StartNewGame()
    {
        if (playerFirst)
        {
            playerMark = CellState.X;
            AIMark = CellState.O;
            currentTurn = Turn.Player;
        }
        else
        {
            playerMark = CellState.O;
            AIMark = CellState.X;
            currentTurn = Turn.AI;
        }
        gameOver = false;

        for (int row = 0; row < 3; row++)
            for (int col = 0; col < 3; col++)
            {
                board[row, col] = CellState.Empty;
                if (cells[row, col] && cells[row, col].image)
                {
                    cells[row, col].image.sprite = null;
                    cells[row, col].image.color = Color.white;
                }
            }
        GameOverMenu.SetActive(false);
        gameResult.text = "";
        if (currentTurn == Turn.AI)
            StartCoroutine(AIMoveRoutine());
    }

    public void PlayerMove(int row, int col)
    {
        if (currentTurn != Turn.Player) return;
        if (gameOver) return;
        if (board[row, col] != CellState.Empty) return;

        Place(row, col, playerMark);
        if (BoardLogic.CheckGameOver(board)) return;

        currentTurn = Turn.AI;
        StartCoroutine(AIMoveRoutine());
    }

    private void Place(int row, int col, CellState who)
    {
        board[row, col] = who;
        if (cells[row, col] && cells[row, col].image)
        {
            var i = cells[row, col].image;
            i.sprite = (who == CellState.X) ? XSprite : OSprite;
            i.color = Color.white;
        }
    }
    private IEnumerator AIMoveRoutine()
    {
        yield return new WaitForSeconds(0.5f);

        var move = GetAIMove();
        if (move.row >= 0)
        {
            Place(move.row, move.col, AIMark);
            if (BoardLogic.CheckGameOver(board)) yield break;
        }

        currentTurn = Turn.Player;
    }

    private (int row, int col) GetAIMove()
    {
        switch (aiLevel)
        {
            case AILevel.Easy:
                {
                    return AILogic.RandomEmpty(board);
                }

            case AILevel.Normal:
                {
                    return AILogic.Heuristic(board);
                }

            case AILevel.Hard:
            default:
                {
                    return (AILogic.Heuristic(board));
                }
        }
    }

}
