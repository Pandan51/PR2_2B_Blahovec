using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pod_limitem
{
    internal class Prumer
    {
        public double[] Nums;
        public int Limit;

        public Prumer(double[] cisla)
        {
            Nums = cisla;
        }

        public double PrumerPodLimitem(double[] array,double lim)
        {
            double x = 0;
            int count = 0;

            foreach(double y in array)
            {
                if(y < lim)
                {
                    x += y;
                    count++;
                }
            }

            return x / count;
        }
    }
}
