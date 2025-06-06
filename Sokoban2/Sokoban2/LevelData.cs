using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban2
{
    public class LevelData
    {
        public int LevelNumber { get; set; }
        public int[][] Grid { get; set; }  // 2D array as nested lists (JSON-friendly)
    }
}
