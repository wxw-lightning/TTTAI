using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class BoardLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static CellState GetWinner(CellState[,] board)
    {
        for (int row = 0; row < 3; row++)
        {
            if (board[row, 0] != CellState.Empty && board[row, 0] == board[row, 1] && board[row, 1] == board[row, 2])
            {
                return board[row, 0];
            }
        }
        for (int col = 0; col < 3; col++)
        {
            if (board[0, col] != CellState.Empty && board[0, col] == board[1, col] && board[1, col] == board[2, col])
            {
                return board[0, col];
            }
        }
        if (board[0, 0] != CellState.Empty && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
        {
            return board[0, 0];
        }
            
        if (board[0, 2] != CellState.Empty && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
        {
            return board[0, 2];
        }
           
        return CellState.Empty;
    }
    public static bool CheckGameOver(CellState[,] board)
    {
        var winner = GetWinner(board);
        if (winner != CellState.Empty)
        {
            if (winner == CellState.X)
            {
                if (GameManager.Instance.playerFirst)
                {
                    GameManager.Instance.gameResult.text = "You win!";
                }
                else
                {
                    GameManager.Instance.gameResult.text = "AI win!";
                }
            }
            else
            {
                if (GameManager.Instance.playerFirst)
                {
                    GameManager.Instance.gameResult.text = "AI win!";
                }
                else
                {
                    GameManager.Instance.gameResult.text = "You win!";
                }
            }
            GameManager.Instance.GameOverMenu.SetActive(true);
            GameManager.Instance.gameOver = true;
            return true;
        }
        if (IsFull(board))
        {
            GameManager.Instance.GameOverMenu.SetActive(true);
            GameManager.Instance.gameOver = true;
            GameManager.Instance.gameResult.text = "Game Over! Draw.";
            return true;
        }
        return false;
    }

    public static bool IsFull(CellState[,] board)
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
