using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Lab_2.Models;

namespace Lab_2.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly TicTacToeGame game;
        private string statusMessage = "";
        private int difficulty = 1;
        private bool gameOver;
        public ObservableCollection<CellViewModel> Cells { get; }
        public ICommand MoveCommand { get; }
        public ICommand NewGameCommand { get; }
        public string StatusMessage
        {
            get => statusMessage;
            set
            {
                statusMessage = value;
                OnPropertyChanged();
            }
        }
        public int Difficulty
        {
            get => difficulty;
            set
            {
                difficulty = value;
                OnPropertyChanged();
                NewGame();
            }
        }
        public MainViewModel()
        {
            game = new TicTacToeGame();
            Cells = new ObservableCollection<CellViewModel>();
            for (int row = 0; row < TicTacToeGame.BoardSize; row++)
            {
                for (int column = 0; column < TicTacToeGame.BoardSize; column++)
                {
                    Cells.Add(new CellViewModel(row, column));
                }
            }
            MoveCommand = new RelayCommand(MakeHumanMove);
            NewGameCommand = new RelayCommand(parameter => NewGame());
            NewGame();
        }
        private void MakeHumanMove(object? parameter)
        {
            if (gameOver)
                return;
            if (parameter is not CellViewModel cell)
                return;
            if (!game.MakeMove(cell.Row, cell.Column, CellState.X))
                return;
            UpdateCells();
            if (CheckGameEnd())
                return;
            MakeComputerMove();
        }
        private void MakeComputerMove()
        {
            var move = game.ChooseComputerMove(Difficulty);
            if (move.Row == -1)
                return;
            game.MakeMove(move.Row, move.Column, CellState.O);
            UpdateCells();
            CheckGameEnd();
        }
        private bool CheckGameEnd()
        {
            CellState winner = game.CheckWinner();
            if (winner == CellState.X)
            {
                StatusMessage = "Ви перемогли!";
                gameOver = true;
                UpdateCells();
                return true;
            }
            if (winner == CellState.O)
            {
                StatusMessage = "Комп'ютер переміг!";
                gameOver = true;
                UpdateCells();
                return true;
            }
            if (game.IsDraw())
            {
                StatusMessage = "Нічия!";
                gameOver = true;
                UpdateCells();
                return true;
            }
            StatusMessage = "Ваш хід";
            return false;
        }
        private void NewGame()
        {
            game.NewGame();
            gameOver = false;

            UpdateCells();

            StatusMessage = "Ваш хід";
        }
        private void UpdateCells()
        {
            foreach (CellViewModel cell in Cells)
            {
                cell.State = game.GetCell(cell.Row, cell.Column);
                cell.GameEnabled = !gameOver;
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}