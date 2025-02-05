using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Models
{
    // за что отвечаект клетка
    enum Cell
    {
        None, Wall, Box, Goal, Player, PlayerOnGoal, BoxOnGoal
    }
}
