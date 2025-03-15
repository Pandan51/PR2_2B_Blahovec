using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    internal static class FileManagement
    {
        //Název seznamu
        //Statická vlastnost názvu současně vybraného souboru
        public static string currentListName { get; set; }
        //Statická vlastnost, určuje, zda je načten seznam.
        public static bool isListLoaded { get; private set; } = false;

        public static bool changed { get; set; } = false;

        public static void ListNotLoadedWarning()
        {
            Display.ForegroundColor("red");
            Console.WriteLine("Není načten seznam. Nelze vykonat akci!");
            Display.ForegroundColor("reset");
            Console.ReadKey();
        }

        public static void ListIsLoaded()
        {
            isListLoaded = true;
        }
        public static void ListNotLoaded()
        {
            isListLoaded = false;
        }
    }
}
