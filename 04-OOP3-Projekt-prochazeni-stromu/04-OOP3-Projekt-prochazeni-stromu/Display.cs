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
        
        public static void DisplaySalesman(Salesman current, Salesman boss, int selectedIndex/*, bool popUpMenu*/, List<Salesman> markedList)
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
            //Menu


            //if (popUpMenu == true && selectedIndex == -1 && manager != null)
            //{
            //    //DisplayPopUpMenu(Find.FindMarkedInList(markedList, manager));
            //}
            //else
            //{
            //    Console.WriteLine();
            //}

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

                //if (popUpMenu == true && selectedIndex == i)
                //{
                //    //DisplayPopUpMenu(Find.FindMarkedInList(markedList, subordinate));
                //}
                //else
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
        //public static void LoopCase(Salesman current, Salesman boss, int count, bool menuBool, List<Salesman> marked, bool displayMode)
        //{
        //    if (displayMode == true)
        //    {
        //        #region Prohlizec
        //        Console.Clear();
        //        Console.WriteLine();
        //        //Console.Clear();
        //        Display.DisplaySalesman(current, boss, count, menuBool, marked);


        //        //Console.WriteLine("U for up, D for down");
        //        //Console.WriteLine("Up arrow to move up, down arrow to move down");
        //        var key = Console.ReadKey(true).Key;
        //        //char key = Char.ToUpper(Console.ReadKey().KeyChar);
        //        Console.WriteLine();

        //        switch (key)
        //        {

        //            case ConsoleKey.UpArrow:
        //                // Move to the superior (previous subordinate)
        //                if (count >= -3)
        //                {
        //                    count--; // Go up (previous subordinate)
        //                }
        //                else
        //                {
        //                    // Optionally handle wraparound or show a message if already at the top
        //                    Console.WriteLine("You are already at the top!");
        //                }
        //                break;
        //            case ConsoleKey.LeftArrow:
        //                if (count == -4)
        //                {
        //                    count++;
        //                }
        //                else if (menuBool == true)
        //                {
        //                    menuBool = false;
        //                }
        //                break;
        //            case ConsoleKey.RightArrow:
        //                //Posouvání v menu
        //                if (count == -3)
        //                {
        //                    count--;
        //                }
        //                else if (count > -3)
        //                {
        //                    menuBool = true;
        //                }
        //                break;
        //            case ConsoleKey.DownArrow:
        //                // Move to the next subordinate
        //                if (count == -4)
        //                {
        //                    count += 2;
        //                }
        //                else if (count < current.Subordinates.Count - 1)
        //                {
        //                    count++; // Go down (next subordinate)
        //                }

        //                else
        //                {
        //                    // Optionally handle wraparound or show a message if already at the bottom
        //                    Console.WriteLine("No more subordinates.");
        //                }
        //                break;

        //            case ConsoleKey.Enter:
        //                //

        //                // If Enter is pressed, select the current subordinate
        //                if (count > -1 && current.Subordinates.Count > 0)
        //                {
        //                    Console.WriteLine($"You selected: {current.Subordinates[count].Name}");

        //                    current = current.Subordinates[count];  // Change to the selected subordinate
        //                }
        //                else if (count == -2)
        //                {
        //                    //Potencionální místo pro označování a přidání/odstraňování
        //                    Console.WriteLine("Už jsme na pozici");
        //                    Console.ReadKey();
        //                }
        //                //Přejít na šéfa
        //                else if (count == -3)
        //                {
        //                    current = boss;
        //                }
        //                //Přepnutí na mód seznam
        //                else if (count == -4)
        //                {
        //                    count--;
        //                    Console.WriteLine("Přepnout na seznam");
        //                    Console.ReadKey();
        //                    displayMode = false;
        //                }
        //                else if (count == -5)
        //                {
        //                    count++;
        //                    Console.WriteLine("Přepnout na prohlížeč");
        //                    Console.ReadKey();
        //                }
        //                else
        //                {
        //                    //Případ kde není žádný subordinate
        //                    current = Find.FindUpperSalesman(current, boss);
        //                    //count = -1;
        //                }
        //                break;
        //            case ConsoleKey.Spacebar:
        //                marked = MarkSalesman(marked, current);

        //                break;

        //        }
        //        #endregion
        //    }
        //    else
        //    {

        //    }
        //}

        //static List<Salesman> MarkSalesman(List<Salesman> markedList, Salesman target)
        //{
        //    bool found = false;
        //    foreach (Salesman x in markedList)
        //    {
        //        if (x == target)
        //        {
        //            found = true;
        //        }
        //    }

        //    if (found == true)
        //    {
        //        markedList.Remove(target);
        //    }
        //    else
        //    {
        //        markedList.Add(target);
        //    }
        //    return markedList;
        //}

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
