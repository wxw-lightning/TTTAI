using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class AILogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static (int row, int col) RandomEmpty(CellState[,] board)
    {
        int count = 0;
        foreach (var s in board) if (s == CellState.Empty) count++;
        if (count == 0) return (-1, -1);

        int pick = Random.Range(0, count);
        for (int row = 0; row < 3; row++)
            for (int col = 0; col < 3; col++)
            {
                if (board[row, col] == CellState.Empty)
                {
                    if (pick == 0) return (row, col);
                    pick--;
                }
            }
        return (-1, -1);
    }

    public static (int row, int col) Heuristic(CellState[,] board)
    {
        //center
        if (board[1, 1] == CellState.Empty) return (1, 1);

        //corner
        if (board[0, 0] == CellState.Empty)
        {
            return (0, 0);
        }
        else if(board[0, 2] == CellState.Empty)
        {
            return (0, 2);
        }
        else if (board[2, 0] == CellState.Empty)
        {
            return (2, 0);
        }
        else if (board[2, 2] == CellState.Empty)
        {
            return (2, 2);
        }

        return RandomEmpty(board);
    }

    public static (int row, int col) MinimaxMove(CellState[,] board, CellState ai, CellState player)
    {
        int bestValue = int.MinValue;
        int bestRow = -1;
        int bestCol = -1;
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (board[row, col] == CellState.Empty)
                {
                    board[row, col] = ai;
                    int value = MinimaxAB(board, false, 0 , ai, player, int.MinValue, int.MaxValue);
                    board[row, col] = CellState.Empty;
                    if(value > bestValue)
                    {
                        bestValue = value;
                        bestRow = row;
                        bestCol = col;
                    }
                }
            }
        }
        return (bestRow, bestCol);
    }
    public static int MinimaxAB(CellState[,] board, bool isMaximizing,int depth, CellState ai, CellState player, int alpha, int beta)
    {
        var win = BoardLogic.GetWinner(board);
        if (win == ai) return 10 - depth; 
        if (win == player) return depth - 10; 
        if (BoardLogic.IsFull(board)) return 0; 

        if (isMaximizing)
        {
            int maxValue = int.MinValue;
            for (int row = 0; row < 3; row++)
            {
                for(int col = 0; col < 3; col++)
                {
                    if (board[row, col] == CellState.Empty)
                    {
                        board[row, col] = ai;
                        int value = MinimaxAB(board,false, depth+1, ai, player, alpha, beta);
                        board[row, col] = CellState.Empty;

                        maxValue = Mathf.Max(maxValue, value);
                        alpha = Mathf.Max(alpha, maxValue);

                        if (beta <= alpha) break;
                    }
                }
            }
            return maxValue;
        }
        else
        {
            int minValue = int.MaxValue;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if(board[row, col] == CellState.Empty)
                    {
                        board[row, col] = player;
                        int value = MinimaxAB(board, true, depth+1, ai, player, alpha, beta);
                        board[row, col] = CellState.Empty;

                        minValue = Mathf.Min(minValue, value);
                        beta = Mathf.Min(beta, minValue);

                        if (beta <= alpha) break;
                    }
                }
            }
            return minValue;
        }
    }
}
