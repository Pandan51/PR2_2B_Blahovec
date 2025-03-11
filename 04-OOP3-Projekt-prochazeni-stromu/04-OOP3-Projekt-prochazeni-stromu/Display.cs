using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    static class Display
    {
        
        public static void DisplaySalesman(Salesman current, Salesman boss, int selectedIndex)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            if (selectedIndex == -3)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
            }
            Console.Write("Přejít nahoru");
            if (selectedIndex == -3)
            {
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.Write("  |  ");
            if (selectedIndex == -4)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
            }
            Console.WriteLine("Přejít na seznam");
            Console.ResetColor();
            Console.WriteLine("- - - - - - - - - - - - - - - - - -");
            Salesman manager = Find.FindUpperSalesman(current, boss);
            string pozice;
            if (manager == null)
            {
                pozice = $" \nTohle je šéf firmy";
            }
            else
            {
                pozice = $" \nNadřízení:";
            }

            Console.Write($"Obchodník: ");
            if (selectedIndex == -2)
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            Console.WriteLine($"{current.Name} {current.Surname}");
            if (selectedIndex == -2)
            {
                Console.ResetColor();
            }
            Console.Write($"Příme prodeje: {current.Sales} $");
            Console.Write($"{pozice} ");
            if (selectedIndex == -1)
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }

            if (manager != null)
                Console.Write($"{manager.Name} {manager.Surname}");

            if (selectedIndex == -1)
            {
                Console.ResetColor();
            }
            Console.WriteLine();
            Console.Write($"Podřízení: ");

            for (int i = 0; i < current.Subordinates.Count; i++)
            {
                var subordinate = current.Subordinates[i];


                // Highlight the selected subordinate
                if (i == selectedIndex)
                {

                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write($"{subordinate.Name} {subordinate.Surname}");  // Highlighted line
                    Console.ResetColor();
                    Console.WriteLine();



                }
                else
                {
                    Console.WriteLine($"{subordinate.Name} {subordinate.Surname}");  // Regular line
                }
                Console.Write("           ");


            }

        }

        public static void DisplaySalesmenTree(Salesman node, string indent = "")
        {
            Console.WriteLine($"{indent}{node.Name} {node.Surname} - Sales: {node.Sales}");

            foreach (var subordinate in node.Subordinates)
            {
                DisplaySalesmenTree(subordinate, indent + "    ");
            }
        }
    }
}
