using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    abstract class ElectricDevice
    {
        public int Voltage { get; private set; }
        public string Name { get; private set; }
        public bool State { get;  set; }

        public ElectricDevice(int voltage, string name)
        {
            Voltage = voltage;
            Name = name;
            State = true;
        }

        public abstract string TurnOn();
        public abstract string TurnOff();
        
    }
}
