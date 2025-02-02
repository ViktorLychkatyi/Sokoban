using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Sokoban.Models;

namespace Sokoban.ViewModels
{
    class CellVM : BindableBase
    {
        public int Row { get; }
        public int Column { get; }

        private Cell cell;

        public Cell Cell
        {
            get 
            {
                return cell; 
            }
            set 
            { 
                cell = value;
                RaisePropertyChanged(nameof(Cell));
            }
        }

        public CellVM(int row, int column, Cell cell)
        {
            Row = row;
            Column = column;
            Cell = cell;
        }

        public CellVM(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
