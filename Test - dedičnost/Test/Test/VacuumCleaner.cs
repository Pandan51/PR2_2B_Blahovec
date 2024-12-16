using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class VacuumCleaner : ElectricDevice
    {
        public VacuumCleaner() : base(230, "Vacuum cleaner")
        {
            State = true;
        }

        public override string TurnOn()
        {
            State = true;
            return $"Connected to {Voltage} V and cleaning";
        }
        public override string TurnOff()
        {
            State = false;
            return "Cleaning finished";
        }



    }
}
