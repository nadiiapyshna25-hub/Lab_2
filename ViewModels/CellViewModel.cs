using System.ComponentModel;
using System.Runtime.CompilerServices;
using Lab_2.Models;

namespace Lab_2.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private CellState state;
        private bool gameEnabled = true;
        public int Row { get; }
        public int Column { get; }
        public CellState State
        {
            get => state;
            set
            {
                state = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Text));
                OnPropertyChanged(nameof(IsEnabled));
            }
        }
        public string Text
        {
            get
            {
                if (State == CellState.X)
                    return "X";
                if (State == CellState.O)
                    return "O";
                return "";
            }
        }
        public bool GameEnabled
        {
            get => gameEnabled;
            set
            {
                gameEnabled = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsEnabled));
            }
        }
        public bool IsEnabled
        {
            get => State == CellState.Empty && GameEnabled;
        }
        public CellViewModel(int row, int column)
        {
            Row = row;
            Column = column;
            State = CellState.Empty;
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