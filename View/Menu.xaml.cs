﻿using System;
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
    // главное меню
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
        }

        // кнопки
        private void Start(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Game());
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
