namespace Lanovka___new_solution
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Clovek clovek = new Clovek("Petr", 70);
            Sedacka sedacka = new Sedacka();
            sedacka.Pasazer = clovek;
            Lanovka lanovka = new Lanovka(5, 500, sedacka);

            lanovka.Nastup(clovek);
            lanovka.Jed();
            lanovka.Nastup(clovek);
            lanovka.Jed();
            lanovka.Nastup(clovek);
            lanovka.Jed();
            lanovka.Nastup(clovek);
            lanovka.Jed();
            lanovka.Nastup(clovek);
            lanovka.Jed();
            lanovka.Nastup(clovek); 
            lanovka.Jed();
            lanovka.Nastup(clovek);

            lanovka.Vypis();




        }
        
        
    }

    
}

