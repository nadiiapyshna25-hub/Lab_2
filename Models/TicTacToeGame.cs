using System;
using System.Collections.Generic;

namespace Lab_2.Models
{
    public enum CellState
    {
        Empty,
        X,
        O
    }
    public class TicTacToeGame
    {
        public const int BoardSize = 3;
        private CellState[,] board;
        private Random random;
        public TicTacToeGame()
        {
            board = new CellState[BoardSize, BoardSize];
            random = new Random();
        }
        public CellState GetCell(int row, int column)
        {
            return board[row, column];
        }
        public void NewGame()
        {
            for (int row = 0; row < BoardSize; row++)
            {
                for (int column = 0; column < BoardSize; column++)
                {
                    board[row, column] = CellState.Empty;
                }
            }
        }
        public bool MakeMove(int row, int column, CellState player)
        {
            if (board[row, column] != CellState.Empty)
            {
                return false;
            }
            board[row, column] = player;
            return true;
        }
        public List<(int Row, int Column)> GetAvailableMoves()
        {
            List<(int Row, int Column)> moves =
                new List<(int Row, int Column)>();

            for (int row = 0; row < BoardSize; row++)
            {
                for (int column = 0; column < BoardSize; column++)
                {
                    if (board[row, column] == CellState.Empty)
                    {
                        moves.Add((row, column));
                    }
                }
            }
            return moves;
        }
        public CellState CheckWinner()
        {
            for (int row = 0; row < BoardSize; row++)
            {
                CellState first = board[row, 0];
                if (first == CellState.Empty)
                {
                    continue;
                }
                bool win = true;
                for (int column = 1; column < BoardSize; column++)
                {
                    if (board[row, column] != first)
                    {
                        win = false;
                        break;
                    }
                }
                if (win)
                {
                    return first;
                }
            }
            for (int column = 0; column < BoardSize; column++)
            {
                CellState first = board[0, column];
                if (first == CellState.Empty)
                {
                    continue;
                }
                bool win = true;
                for (int row = 1; row < BoardSize; row++)
                {
                    if (board[row, column] != first)
                    {
                        win = false;
                        break;
                    }
                }
                if (win)
                {
                    return first;
                }
            }
            CellState diagonal = board[0, 0];
            if (diagonal != CellState.Empty)
            {
                bool win = true;
                for (int i = 1; i < BoardSize; i++)
                {
                    if (board[i, i] != diagonal)
                    {
                        win = false;
                        break;
                    }
                }
                if (win)
                {
                    return diagonal;
                }
            }
            diagonal = board[0, BoardSize - 1];
            if (diagonal != CellState.Empty)
            {
                bool win = true;
                for (int i = 1; i < BoardSize; i++)
                {
                    if (board[i, BoardSize - 1 - i] != diagonal)
                    {
                        win = false;
                        break;
                    }
                }
                if (win)
                {
                    return diagonal;
                }
            }
            return CellState.Empty;
        }
        public bool IsDraw()
        {
            if (CheckWinner() != CellState.Empty)
            {
                return false;
            }
            return GetAvailableMoves().Count == 0;
        }
        public (int Row, int Column) ChooseComputerMove(int level)
        {
            List<(int Row, int Column)> moves = GetAvailableMoves();
            if (moves.Count == 0)
            {
                return (-1, -1);
            }
            if (level >= 3)
            {
                foreach (var move in moves)
                {
                    board[move.Row, move.Column] = CellState.O;
                    bool computerWins =
                        CheckWinner() == CellState.O;
                    board[move.Row, move.Column] = CellState.Empty;
                    if (computerWins)
                    {
                        return move;
                    }
                }
            }

            if (level >= 2)
            {
                foreach (var move in moves)
                {
                    board[move.Row, move.Column] = CellState.X;
                    bool humanWins =
                        CheckWinner() == CellState.X;
                    board[move.Row, move.Column] = CellState.Empty;
                    if (humanWins)
                    {
                        return move;
                    }
                }
            }
            int index = random.Next(moves.Count);
            return moves[index];
        }
    }
}