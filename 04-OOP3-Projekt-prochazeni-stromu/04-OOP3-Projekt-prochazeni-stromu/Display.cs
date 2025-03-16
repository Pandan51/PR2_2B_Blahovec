using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    static class Display
    {
        
        public static void DisplayBrowser(Salesman current, Salesman boss, int count, List<Salesman> marked)
        {
            #region Menu
            
            if (count == -3)
            {
                HighlightedColouring();
            }
            else
            {
                ForegroundColor("dblue");
            }
            Console.Write("Přejít nahoru");
            Console.ResetColor();
            Console.Write("  |  ");
            if (count == -4)
            {
                HighlightedColouring();
            }
            else
            {
                ForegroundColor("dblue");
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
            if (count == -2)
            {
                HighlightedColouring();
            }
            Console.Write($"{current.Name} {current.Surname}");
            if (count == -2)
            {
                Console.ResetColor();
            }



            DisplayPopUpMenu(Find.FindMarkedInList(marked, current));
            

            Console.WriteLine($"Příme prodeje: {current.Sales} $");
            Console.Write($"Celkové prodeje sítě: {Find.GetTotalSalesRecursive(current)} $");
            Console.Write($"{pozice}");
            if (count == -1)
            {
                HighlightedColouring();
            }

            if (manager != null)
                Console.WriteLine($"{manager.Name} {manager.Surname}");

            if (count == -1)
            {
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.Write($"Podřízení: ");

            for (int i = 0; i < current.Subordinates.Count; i++)
            {
                var subordinate = current.Subordinates[i];


                // Highlight the selected subordinate
                if (i == count)
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
        public static void DisplayList(Salesman current, Salesman boss, int count, List<Salesman> marked)
        {
            Console.Clear();
            #region Seznam
            if (count == -5)
                Display.HighlightedColouring();
            else
                Display.ForegroundColor("dblue");
            Console.Write("Založit");
            Display.ForegroundColor("reset");
            Console.Write(" | ");
            if (count == -6)
                Display.HighlightedColouring();
            else
                Display.ForegroundColor("dblue");
            Console.Write("Načíst");
            Display.ForegroundColor("reset");
            Console.Write(" | ");
            if (count == -7)
                Display.HighlightedColouring();
            else
                Display.ForegroundColor("dblue");
            Console.Write("Uložit");
            Display.ForegroundColor("reset");
            Console.Write(" | ");
            if (count == -8)
                Display.HighlightedColouring();
            else
                Display.ForegroundColor("dblue");
            Console.Write("Přejít na prohlížeč\n");
            Display.ForegroundColor("reset");

            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - -");
            Console.WriteLine($"Seznam: {FileManagement.currentListName}");
            Console.WriteLine("- - - - - - - - -");

            //foreach (Salesman x in marked)
            //{

            //    Console.WriteLine(x.Name+" "+x.Surname);
            //}

            for (int i = 0; i < marked.Count; i++)
            {
                if (count == i)
                {
                    Display.HighlightedColouring();
                    Console.WriteLine($"{marked[i].Name} {marked[i].Surname}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{marked[i].Name} {marked[i].Surname}");
                }
            }
            #endregion
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
