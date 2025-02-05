using Sokoban.Models;
using Sokoban.view_models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
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
using System.Windows.Threading;

namespace Sokoban.View
{
    // взаймодействия с игрой
    public partial class Game : Page
    {
        private int move_count;
        private DispatcherTimer dispatcher_timer;
        private TimeSpan time_span;
        private Stopwatch stopwatch = new Stopwatch();

        public Game()
        {
            InitializeComponent();
            InitializeTimer();
            var view_model = new MainVM();
            view_model.GameWon += OnGameWon;
            DataContext = view_model;
            UpdateMoveLabel();
        }

        // инициализация таймера
        private void InitializeTimer()
        {
            time_span = TimeSpan.Zero;
            dispatcher_timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            dispatcher_timer.Tick += Timer;
            dispatcher_timer.Start();
        }

        private void Timer(object sender, EventArgs e)
        {
            time_span = time_span.Add(TimeSpan.FromSeconds(1));
            UpdateTimerLabel();
        }

        // обновление данных текста на экране, таймер, количество ходов
        private void UpdateTimerLabel()
        {
            TimerLabel.Content = $"TIME: {time_span:hh\\:mm\\:ss}";
        }

        private void UpdateMoveLabel()
        {
            MoveLabel.Content = $"MOVE: {move_count}";
        }

        // проверка на завершение игры, если коробки на конечных точках
        private void ReturnNavigation()
        {
            NavigationService?.Navigate(new LevelCompleted());
        }

        private void OnGameWon()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                SaveGameStats(time_span);
                ReturnNavigation();
            });
        }

        // взаймодейтсвие клавишами
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus();
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            var view_model = DataContext as MainVM;
            if (view_model == null) return;

            move_count++;
            UpdateMoveLabel();

            switch (e.Key)
            {
                case Key.Up:
                case Key.W:
                    view_model.MoveCommand.Execute(Player.Up);
                    break;
                case Key.Down:
                case Key.S:
                    view_model.MoveCommand.Execute(Player.Down);
                    break;
                case Key.Left:
                case Key.A:
                    view_model.MoveCommand.Execute(Player.Left);
                    break;
                case Key.Right:
                case Key.D:
                    view_model.MoveCommand.Execute(Player.Right);
                    break;
            }
        }

        // создание файла после прохождении уровня
        private void SaveGameStats(TimeSpan timeTaken)
        {
            string folder_path = @"C:\MyGameStats";
            System.IO.Directory.CreateDirectory(folder_path);

            string file_path = System.IO.Path.Combine(folder_path, "game_stats.txt");

            using (var writer = new System.IO.StreamWriter(file_path))
            {
                writer.WriteLine($"Шаги: {move_count}");
                writer.WriteLine($"Время: {timeTaken:hh\\:mm\\:ss}");
            }
        }

        // кнопки
        private void UndoClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainVM vm && move_count > 0)
            {
                vm.Undo();
                move_count--;
                UpdateMoveLabel();
            }
        }

        private void RedoClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainVM vm && vm.HasRedo())
            {
                vm.Redo();
                move_count++;
                UpdateMoveLabel();
            }
        }

        private void SelectLevel(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Menu());
        }

        private void Restart(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Game());
        }
    }
}
