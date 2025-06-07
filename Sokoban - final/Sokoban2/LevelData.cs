using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban2
{
    public class LevelData
    {
        public int level { get; set; }
        public int[][] grid { get; set; }  // 2D array as nested lists (JSON-friendly)

        //1- green/easy 2- yellow/medium 3-orange/hard 4-very hard/red
        public int color { get; set; }
    }
}
