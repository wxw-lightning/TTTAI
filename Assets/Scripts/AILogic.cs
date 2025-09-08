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
    public (int row, int col) RandomEmpty(CellState[,] b)
    {
        int count = 0;
        foreach (var s in b) if (s == CellState.Empty) count++;
        if (count == 0) return (-1, -1);

        int pick = Random.Range(0, count);
        for (int r = 0; r < 3; r++)
            for (int c = 0; c < 3; c++)
            {
                if (b[r, c] == CellState.Empty)
                {
                    if (pick == 0) return (r, c);
                    pick--;
                }
            }
        return (-1, -1);
    }

    public (int row, int col) Heuristic(CellState[,] board)
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
}
