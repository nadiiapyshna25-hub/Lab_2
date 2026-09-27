using System.Windows;
using Lab_2.ViewModels;

namespace Lab_2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }
    }
}