using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    static class Find
    {
        static void FindSalesmanRecursive(Salesman parentNode, string surname)
        {
            if (parentNode.Surname == surname)
                Console.WriteLine($"{parentNode.Name} {parentNode.Surname} - Sales:{parentNode.Sales}");

            foreach (var subordinate in parentNode.Subordinates)
            {
                FindSalesmanRecursive(subordinate, surname);
            }
        }
        public static bool FindMarkedInList(List<Salesman> markedList, Salesman target)
        {
            foreach(Salesman x in markedList)
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

        static void FindSalesmanStack(Salesman parentNode, string surname)
        {
            Stack<Salesman> toBeVisited = new Stack<Salesman>();
            toBeVisited.Push(parentNode);

            while (toBeVisited.Count > 0)
            {
                Salesman current = toBeVisited.Pop();
                if (current.Surname == surname)
                    Console.WriteLine($"{current.Name} {current.Surname} - Sales:{current.Sales}");

                foreach (var sub in current.Subordinates)
                {
                    toBeVisited.Push(sub);
                }
            }
        }
        #region ui


        #endregion
        static void FindSalesmanQueue(Salesman parentNode, string surname)
        {
            Queue<Salesman> toBeVisited = new Queue<Salesman>();
            toBeVisited.Enqueue(parentNode);

            while (toBeVisited.Count > 0)
            {
                Salesman current = toBeVisited.Dequeue();
                if (current.Surname == surname)
                    Console.WriteLine($"{current.Name} {current.Surname} - Sales:{current.Sales}");

                foreach (var sub in current.Subordinates)
                {
                    toBeVisited.Enqueue(sub);
                }
            }
        }

        static int GetTotalSalesQueue(Salesman parentNode, string surname)
        {
            int total = 0;
            Queue<Salesman> toBeVisited = new Queue<Salesman>();
            toBeVisited.Enqueue(parentNode);

            while (toBeVisited.Count > 0)
            {
                Salesman current = toBeVisited.Dequeue();
                total += current.Sales;
                //if (current.Surname == surname)
                //    Console.WriteLine($"{current.Name} {current.Surname} - Sales:{current.Sales}");

                foreach (Salesman sub in current.Subordinates)
                {
                    toBeVisited.Enqueue(sub);
                }
            }
            return total;
        }

        static int GetTotalSalesRecursive(Salesman parentNode)
        {
            int sum = 0;
            sum += parentNode.Sales;

            foreach (var subordinate in parentNode.Subordinates)
            {
                sum += GetTotalSalesRecursive(subordinate);
            }

            return sum;
        }

        static Salesman[] GetSalesmanStack(Salesman parentNode, string surname)
        {
            List<Salesman> found = new List<Salesman>();
            Stack<Salesman> toBeVisited = new Stack<Salesman>();
            toBeVisited.Push(parentNode);

            while (toBeVisited.Count > 0)
            {
                Salesman current = toBeVisited.Pop();
                if (current.Surname == surname)
                    found.Add(current);

                foreach (var sub in current.Subordinates)
                {
                    toBeVisited.Push(sub);
                }
            }
            return found.ToArray();
        }
    }
}
