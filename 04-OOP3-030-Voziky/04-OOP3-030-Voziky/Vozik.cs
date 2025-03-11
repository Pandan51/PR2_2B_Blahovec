using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    internal class Vozik
    {
        public int _cas { get; set; } = 0;
        public int _zacatecniPoradi { get; private set; }

        public Vozik(int x)
        {
            _zacatecniPoradi = x;
        }
    }
}
