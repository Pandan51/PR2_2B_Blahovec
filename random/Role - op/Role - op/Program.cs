using System.Data;

namespace Role___op
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //zde tvorbu role vyzkoušejte
            Role balicak = new Role("červená", 350);

            Console.WriteLine($"Role papíru, barva {balicak.Colour}, zbývá {balicak.Length}");
        }
    }
}
