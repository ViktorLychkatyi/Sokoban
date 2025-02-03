using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sokoban.Models;
using Prism.Mvvm;
using Prism.Commands;
using System.Windows.Input;

namespace Sokoban.ViewModels
{
    class MainVM : BindableBase
    {
        private int column_count = 25;
        private int row_count = 10;
        private int row_player;
        private int column_player;
        private Random random = new Random(28);
        public List<List<CellVM>> AllCells { get; } = new List<List<CellVM>>();
        public DelegateCommand<Player?> MoveCommand { get; }

        public MainVM()
        {
            MoveCommand = new DelegateCommand<Player?>(Move);
            InitializeCells();
            PlacePlayer();
            PlaceBoxsGoals(4);
        }

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

        private bool CheckBoxPlace(int row, int column)
        {
            int[] row_box = { -1, 1, 0, 0 };
            int[] column_box = { 0, 0, -1, 1 };

            for (int i = 0; i < 4; i++)
            {
                int new_row_box = row + row_box[i];
                int new_column_box = column + column_box[i];

                if (new_row_box >= 0 && new_row_box < row_count && new_row_box >= 0 && new_row_box < column_count)
                {
                    if (AllCells[new_row_box][new_column_box].Cell == Cell.Box)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

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

        private void Move(Player? direction)
        {
            if (direction == null) return;

            switch (direction)
            {
                case Player.Up:
                case Player.W:
                    MovePlayer(-1, 0);
                    break;
                case Player.Down:
                case Player.S:
                    MovePlayer(1, 0);
                    break;
                case Player.Left:
                case Player.A:
                    MovePlayer(0, -1);
                    break;
                case Player.Right:
                case Player.D:
                    MovePlayer(0, 1);
                    break;
            }
        }

        private void MovePlayer(int row_change, int column_change)
        {
            int new_row_player = row_player + row_change;
            int new_column_player = column_player + column_change;

            if (new_row_player < 0 || new_row_player >= row_count || new_column_player < 0 || new_column_player >= column_count)
                return;

            var next_cell = AllCells[new_row_player][new_column_player];

            if (next_cell.Cell == Cell.Wall)
                return;

            if (next_cell.Cell == Cell.Box || next_cell.Cell == Cell.BoxOnGoal)
            {
                int next_row_box = new_row_player + row_change;
                int next_column_box = new_column_player + column_change;

                if (next_row_box < 0 || next_row_box >= row_count || next_column_box < 0 || next_column_box >= column_count)
                    return;

                var next_cell_box = AllCells[next_row_box][next_column_box];
                if (next_cell_box.Cell != Cell.None && next_cell_box.Cell != Cell.Goal)
                    return;

                if (next_cell_box.Cell == Cell.Goal)
                {
                    next_cell_box.Cell = Cell.BoxOnGoal;
                }
                else
                {
                    next_cell_box.Cell = Cell.Box;
                }

                if (next_cell.Cell == Cell.BoxOnGoal || next_cell.Cell == Cell.PlayerOnGoal)
                {
                    next_cell.Cell = Cell.Goal;
                }
                else
                {
                    next_cell.Cell = Cell.None;
                }
            }

            if (AllCells[row_player][column_player].Cell == Cell.PlayerOnGoal)
            {
                AllCells[row_player][column_player].Cell = Cell.Goal;
            }
            else
            {
                AllCells[row_player][column_player].Cell = Cell.None;
            }

            row_player = new_row_player;
            column_player = new_column_player;

            if (next_cell.Cell == Cell.Goal)
            {
                next_cell.Cell = Cell.PlayerOnGoal;
            }
            else
            {
                next_cell.Cell = Cell.Player;
            }

            CheckWin();
        }

        private void CheckWin()
        {
            foreach (var row in AllCells)
            {
                foreach (var cell in row)
                {
                    if (cell.Cell == Cell.Goal)
                    {
                        return;
                    }
                }
            }
            WinMessage();
        }

        private void WinMessage()
        {
            System.Windows.MessageBox.Show("Поздравляем! Вы выиграли!");
        }

        private void PlaceBoxsGoals(int count)
        {
            for (int i = 0; i < count; i++)
            {
                PlaceRandom(Cell.Box);
                PlaceRandom(Cell.Goal);
            }
        }

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

