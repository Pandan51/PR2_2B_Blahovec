using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    static class Display
    {
        
        public static void DisplaySalesman(Salesman current, Salesman boss, int selectedIndex, List<Salesman> markedList)
        {
            #region Menu
            ForegroundColor("dblue");
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
            #endregion

            Salesman manager = Find.FindUpperSalesman(current, boss);
            string pozice;
            if (manager == null)
            {
                pozice = $" \nTohle je šéf firmy\n";
            }
            else
            {
                pozice = $" \nNadřízení:";
            }
            //Výpis s barvičkou, pokud vybrán, současná pozice
            Console.Write($"Obchodník: ");
            if (selectedIndex == -2)
            {
                HighlightedColouring();
            }
            Console.Write($"{current.Name} {current.Surname}");
            if (selectedIndex == -2)
            {
                Console.ResetColor();
            }



            DisplayPopUpMenu(Find.FindMarkedInList(markedList, current));
            

            Console.WriteLine($"Příme prodeje: {current.Sales} $");
            Console.Write($"Celkové prodeje sítě: {Find.GetTotalSalesRecursive(current)} $");
            Console.Write($"{pozice}");
            if (selectedIndex == -1)
            {
                HighlightedColouring();
            }

            if (manager != null)
                Console.WriteLine($"{manager.Name} {manager.Surname}");

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

                    HighlightedColouring();
                    Console.Write($"{subordinate.Name} {subordinate.Surname}");  // Highlighted line
                    Console.ResetColor();
                    Console.Write("");

                }
                else
                {
                    Console.Write($"{subordinate.Name} {subordinate.Surname}");  // Regular line
                }

                    Console.WriteLine();
                Console.Write("           ");


            }

        }

        public static void HighlightedColouring()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        public static void DisplaySalesmenTree(Salesman node, string indent = "")
        {
            Console.WriteLine($"{indent}{node.Name} {node.Surname} - Sales: {node.Sales}");

            foreach (var subordinate in node.Subordinates)
            {
                DisplaySalesmenTree(subordinate, indent + "    ");
            }
        }

        public static void ForegroundColor(string color)
        {
            if (color == "dblue")
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }
            if(color == "red")
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if(color == "reset")
            {
                Console.ResetColor();
            }
        }
        
        static void DisplayPopUpMenu(bool isMarked)
        {

            if (isMarked == true)
            {
                Console.Write("  ");
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("Odstranit");
            }
            else
            {
                Console.Write("  ");
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("Přidat");

            }
            Console.ResetColor();
        }
    }
}
