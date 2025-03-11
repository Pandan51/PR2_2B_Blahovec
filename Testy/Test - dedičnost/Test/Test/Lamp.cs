using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Lamp : ElectricDevice, ILightSource
    {
        private double _cislo;
        public double EnlightedDistance
        {
            get
            {
                if (State == true)
                {
                    return _cislo ;
                }
                else
                {
                    return 0;
                }
            }
            private set { }
        }
        public Lamp(int voltage, double _enlightedDistance) : base(voltage, "Lamp")
        {
            _cislo = _enlightedDistance;
            
        }

        public override string TurnOn()
        {
            State = true;
            return "Light is on";
        }


        public override string TurnOff()
        {
            State = false;
            return "Light is off";
        }
    }
}
