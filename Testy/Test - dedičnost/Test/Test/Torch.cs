using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Torch : ILightSource
    {
        public double EnlightedDistance { get; private set; }
        public Torch(double x)
        {
            EnlightedDistance = x;
        }
    }
}
