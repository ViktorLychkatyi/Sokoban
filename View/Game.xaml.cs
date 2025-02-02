using Sokoban.Models;
using Sokoban.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sokoban.View
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        public Game()
        {
            InitializeComponent();
            DataContext = new MainVM();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus();
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            var viewModel = DataContext as MainVM;
            if (viewModel == null) return;

            switch (e.Key)
            {
                case Key.Up:
                    viewModel.MoveCommand.Execute(Player.Up);
                    break;
                case Key.Down:
                    viewModel.MoveCommand.Execute(Player.Down);
                    break;
                case Key.Left:
                    viewModel.MoveCommand.Execute(Player.Left);
                    break;
                case Key.Right:
                    viewModel.MoveCommand.Execute(Player.Right);
                    break;
                case Key.W:
                    viewModel.MoveCommand.Execute(Player.W);
                    break;
                case Key.S:
                    viewModel.MoveCommand.Execute(Player.S);
                    break;
                case Key.A:
                    viewModel.MoveCommand.Execute(Player.A);
                    break;
                case Key.D:
                    viewModel.MoveCommand.Execute(Player.D);
                    break;
            }
        }
    }
}
