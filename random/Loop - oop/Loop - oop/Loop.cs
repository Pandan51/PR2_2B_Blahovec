using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loop___oop
{
    internal class Loop
    {
        private int[] _cisla;
        public int pozice = 0;
        public int[] Cisla
        {
            get
            { return _cisla; }
            set
            {
                _cisla = value;
            }
        }

        public Loop(int[] nums)
        {
            _cisla = nums;
            pozice = 0;
        }

        public int Current()
        {
            return _cisla[pozice];
        }

        public void Right(int vpravo) 
        {
            //if (vpravo + pozice > _cisla.Length - 1)
            //{
            //    pozice = vpravo % _cisla.Length;
            //}
            //else
            //{
            //    pozice += vpravo;
            //}

            pozice = (vpravo + pozice) % _cisla.Length;
            
        }

        public void Left(int vlevo)
        {
            if(vlevo == 0)
                vlevo = 1;
            else
                pozice -= vlevo;

            while(pozice <0)
            {
                pozice += _cisla.Length;

            }
        }
    }
}
