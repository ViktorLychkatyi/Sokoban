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
    public partial class LevelCompleted : Page
    {
        public LevelCompleted()
        {
            InitializeComponent();
        }

        // кнопки
        private void Next(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SelectLevel(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Restart(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Game());
        }
    }
}
