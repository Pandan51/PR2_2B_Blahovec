using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    internal class Zakaznik
    {
        public int _casNakupu { get; private set; } = 0;
        public int _stravenyCas { get; set; } = 0;
        public Vozik _vozik { get; private set; } 

        public Zakaznik(int casNakupu, Vozik vozik)
        {
            _casNakupu = casNakupu;
            _vozik = vozik;
        }
    }
}
