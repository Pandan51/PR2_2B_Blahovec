using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class CircularSaw : ElectricDevice
    {
        public CircularSaw(int voltage) : base(voltage, "Circular saw")
        {
            State = true;
        }

        public override string TurnOn()
        {
            State = true;
            return $"Connected to {Voltage} V and cutting";
        }
        public override string TurnOff()
        {
           State = false;
           return "Ohhh, that silence";
        }
    }
}
