using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sokoban.Models;
using Prism.Mvvm;
using Prism.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Diagnostics;

namespace Sokoban.view_models
{
    // игровая доска для взаймодействия с вью игрой
    class MainVM : BindableBase
    {
        private int column_count = 25;
        private int row_count = 10;
        private int row_player;
        private int column_player;
        private int move_count = 0;
        private Random random = new Random(28);

        // проверка для отмены и вернуть действия
        private Stack<(int, int, Cell[][])> undo_stack = new Stack<(int, int, Cell[][])>();
        private Stack<(int, int, Cell[][])> redo_stack = new Stack<(int, int, Cell[][])>();

        // список клеток и команды для движения
        public List<List<CellVM>> AllCells { get; } = new List<List<CellVM>>();
        public DelegateCommand<Player?> MoveCommand { get; }

        public MainVM()
        {
            MoveCommand = new DelegateCommand<Player?>(Move);
            InitializeCells();
            PlacePlayer();
            PlaceBoxsGoals(4);
            MoveCount = 0;
        }

        // инициализация клеток
        private void InitializeCells()
        {
            for (int row = 0; row < row_count; row++)
            {
                var row_list = new List<CellVM>();
                for (int column = 0; column < column_count; column++)
                {
                    var cell = new CellVM(row, column, Cell.None);
                    row_list.Add(cell);
                }
                AllCells.Add(row_list);
            }

            for (int i = 0; i < column_count; i++)
            {
                AllCells[0][i].Cell = Cell.Wall;
                AllCells[row_count - 1][i].Cell = Cell.Wall;
            }

            for (int i = 0; i < row_count; i++)
            {
                AllCells[i][0].Cell = Cell.Wall;
                AllCells[i][column_count - 1].Cell = Cell.Wall;
            }

            for (int i = 1; i < row_count - 1; i++)
            {
                for (int j = 1; j < column_count - 1; j++)
                {
                    if (random.Next(0, 10) == 0)
                    {
                        if (CheckBoxPlace(i, j))
                        {
                            AllCells[i][j].Cell = Cell.Wall;
                        }
                    }
                }
            }
        }

        // размещение коробок
        private bool CheckBoxPlace(int row, int column)
        {
            int[] row_box = { -1, 1, 0, 0 };
            int[] column_box = { 0, 0, -1, 1 };

            for (int i = 0; i < 4; i++)
            {
                int new_row_box = row + row_box[i];
                int new_column_box = column + column_box[i];

                if (new_row_box >= 0 && new_row_box < row_count && new_column_box >= 0 && new_column_box < column_count)
                {
                    if (AllCells[new_row_box][new_column_box].Cell == Cell.Box)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // копирование клеток
        private Cell[][] CopyCells()
        {
            var cells_copy = new Cell[row_count][];
            for (int row = 0; row < row_count; row++)
            {
                cells_copy[row] = new Cell[column_count];
                for (int column = 0; column < column_count; column++)
                {
                    cells_copy[row][column] = AllCells[row][column].Cell;
                }
            }
            return cells_copy;
        }

        // размещение игрока
        private void PlacePlayer()
        {
            while (true)
            {
                int row_index = random.Next(1, row_count - 1);
                int col_index = random.Next(1, column_count - 1);

                if (AllCells[row_index][col_index].Cell == Cell.None)
                {
                    AllCells[row_index][col_index].Cell = Cell.Player;
                    row_player = row_index;
                    column_player = col_index;
                    break;
                }
            }
        }

        // количество ходов
        public int MoveCount
        {
            get
            {
                return move_count;
            }
            set
            {
                if (move_count != value)
                {
                    move_count = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(MoveCount)));
                }
            }
        }

        // обновление состояния игрового поля
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // движение игрока
        private void Move(Player? direction)
        {
            if (direction == null) return;

            undo_stack.Push((row_player, column_player, CopyCells()));
            redo_stack.Clear();

            bool moved = false;
            switch (direction)
            {
                case Player.Up:
                case Player.W:
                    moved = MovePlayer(-1, 0);
                    break;
                case Player.Down:
                case Player.S:
                    moved = MovePlayer(1, 0);
                    break;
                case Player.Left:
                case Player.A:
                    moved = MovePlayer(0, -1);
                    break;
                case Player.Right:
                case Player.D:
                    moved = MovePlayer(0, 1);
                    break;
            }
            if (moved)
            {
                MoveCount++;
            }
        }

        // система движении игрока
        private bool MovePlayer(int row_change, int column_change)
        {
            int new_row_player = row_player + row_change;
            int new_column_player = column_player + column_change;

            if (new_row_player < 0 || new_row_player >= row_count ||
                new_column_player < 0 || new_column_player >= column_count)
                return false;

            var next_cell = AllCells[new_row_player][new_column_player];

            if (next_cell.Cell == Cell.Wall)
                return false;

            if (next_cell.Cell == Cell.Box || next_cell.Cell == Cell.BoxOnGoal)
            {
                int next_row_box = new_row_player + row_change;
                int next_column_box = new_column_player + column_change;

                if (next_row_box < 0 || next_row_box >= row_count ||
                    next_column_box < 0 || next_column_box >= column_count)
                    return false;

                var next_cell_box = AllCells[next_row_box][next_column_box];
                if (next_cell_box.Cell != Cell.None && next_cell_box.Cell != Cell.Goal)
                    return false;

                next_cell_box.Cell = (next_cell_box.Cell == Cell.Goal)? Cell.BoxOnGoal: Cell.Box;
                next_cell.Cell = (next_cell.Cell == Cell.BoxOnGoal)? Cell.Goal: Cell.None;
            }

            AllCells[row_player][column_player].Cell = (AllCells[row_player][column_player].Cell == Cell.PlayerOnGoal)? Cell.Goal: Cell.None;

            row_player = new_row_player;
            column_player = new_column_player;
            next_cell.Cell = (next_cell.Cell == Cell.Goal)? Cell.PlayerOnGoal: Cell.Player;

            MoveCount++;
            CheckWin();
            return true;
        }

        // система действии отмены
        public void Undo()
        {
            if (undo_stack.Count > 0)
            {
                redo_stack.Push((row_player, column_player, CopyCells()));

                var (prev_row, prev_column, prev_cell) = undo_stack.Pop();
                row_player = prev_row;
                column_player = prev_column;

                for (int row = 0; row < row_count; row++)
                {
                    for (int column = 0; column < column_count; column++)
                    {
                        AllCells[row][column].Cell = prev_cell[row][column];
                    }
                }
                MoveCount--;
            }
        }

        // система действии возврата
        public void Redo()
        {
            if (redo_stack.Count > 0)
            {
                undo_stack.Push((row_player, column_player, CopyCells()));

                var (next_row, next_col, next_board) = redo_stack.Pop();
                row_player = next_row;
                column_player = next_col;

                for (int row = 0; row < row_count; row++)
                {
                    for (int column = 0; column < column_count; column++)
                    {
                        AllCells[row][column].Cell = next_board[row][column];
                    }
                }
                MoveCount++;
            }
        }
        public bool HasRedo()
        {
            return redo_stack.Count > 0;
        }

        // событие победы
        public event Action? GameWon;

        // проверка на победу
        private void CheckWin()
        {
            if (AllCells.SelectMany(row => row).All(cell => cell.Cell != Cell.Goal))
            {
                if (AllCells.SelectMany(row => row).Any(cell => cell.Cell == Cell.PlayerOnGoal))
                {
                    return;
                }
                GameWon?.Invoke();
            }
        }

        // рандом размещение коробок и целей
        private void PlaceBoxsGoals(int count)
        {
            for (int i = 0; i < count; i++)
            {
                PlaceRandom(Cell.Box);
                PlaceRandom(Cell.Goal);
            }
        }

        // рандомное размещение
        private void PlaceRandom(Cell cell)
        {
            while (true)
            {
                int row_place = random.Next(1, row_count - 1);
                int column_place = random.Next(1, column_count - 1);

                if (AllCells[row_place][column_place].Cell == Cell.None && CheckPlaceBox(row_place, column_place))
                {
                    AllCells[row_place][column_place].Cell = cell;
                    break;
                }
            }
        }

        // проверка на размещение коробок
        private bool CheckPlaceBox(int row, int column)
        {
            int[] row_box = { -1, 1, 0, 0 };
            int[] column_box = { 0, 0, -1, 1 };

            int wall_count = 0;
            for (int i = 0; i < 4; i++)
            {
                int new_row_box = row + row_box[i];
                int new_column_box = column + column_box[i];

                if (new_row_box >= 0 && new_row_box < row_count && new_column_box >= 0 && new_column_box < column_count)
                {
                    if (AllCells[new_row_box][new_column_box].Cell == Cell.Wall)
                    {
                        wall_count++;
                    }
                }
            }
            return wall_count < 2;
        }
    }
}

