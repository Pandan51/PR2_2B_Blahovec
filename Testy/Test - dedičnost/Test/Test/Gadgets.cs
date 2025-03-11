using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Gadgets
    {
        ElectricDevice[] array;
        public int MaxVoltage {
            get
            {
                int y = 0;
                foreach (ElectricDevice x in array)
                {
                    if (y == 0)
                        y = x.Voltage;
                    else if (y < x.Voltage)
                        y = x.Voltage;
                }
                return y;
            }
        }

        public Gadgets(ElectricDevice[] pole)
        {
            array = pole;
        }

        public void TurnAllOn()
        {
            foreach(ElectricDevice x in array)
            {
                x.State = true;
                Console.WriteLine(x.TurnOn());
            }

        }
        public void TurnAllOff()
        {
            foreach (ElectricDevice x in array)
            {
                x.State = false;
                Console.WriteLine(x.TurnOff());
            }
        }
    }
}
