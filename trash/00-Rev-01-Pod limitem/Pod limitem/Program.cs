namespace Pod_limitem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[] cisla = { 1.3, 2.2, -1.5, 1.4, -2.7 };
            Prumer prumer = new Prumer(cisla);

            Console.WriteLine(prumer.PrumerPodLimitem(cisla,1.1));
            Console.WriteLine(prumer.PrumerPodLimitem(cisla, -2));
        }
    }
}
