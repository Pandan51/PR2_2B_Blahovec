using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    static class Find
    {
        public static bool FindMarkedInList(List<Salesman> markedList, Salesman target)
        {
            foreach (Salesman x in markedList)
            {
                if (x == target)
                    return true;

            }
            return false;
        }

        public static Salesman FindUpperSalesman(Salesman target, Salesman root)
        {

            if (target == root)
            {
                //Console.WriteLine($"Našli jsme cíl na začátku : {target.Name}");
                return null;
            }
            foreach (var employee in root.Subordinates)
            {
                if (employee == target)
                {
                    //Console.WriteLine($"Našli jsme cíl {target.Name}");
                    return root;

                }

                Salesman found = FindUpperSalesman(target, employee);
                if (found != null)
                {
                    return found;  // If found in a deeper level, return it
                }

            }
            return null;
        }


        public static int GetTotalSalesRecursive(Salesman parentNode)
        {
            int sum = 0;
            sum += parentNode.Sales;

            foreach (var subordinate in parentNode.Subordinates)
            {
                sum += GetTotalSalesRecursive(subordinate);
            }

            return sum;
        }

    }
}
