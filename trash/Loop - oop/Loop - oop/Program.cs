namespace Loop___oop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] nums = { 1, 4, 9, 16, 25 };
            Loop l = new Loop(nums);
            
            string cislo;
            //l.Cisla = nums;

            while (true)
            {
                int x = 0;
                Console.WriteLine(l.Current());
                Console.WriteLine("O kolik se posuneme(číslo nebo prázdné[o 1 místo]");
                cislo = Console.ReadLine();
                if (cislo != null)
                {
                    int.TryParse(cislo, out x);
                }
                else
                {
                    x = 1;
                }

                Console.WriteLine("Vlevo nebo vpravo? (napište slovně vpravo nebo vlevo");
                string s = Console.ReadLine();
                Console.WriteLine("Hodnota x "+ x);

                if(s.ToLower() == "vpravo")
                {
                    
                    l.Right(x);
                }
                else if(s.ToLower() == "vlevo")
                {
                    l.Left(x);
                }
                else
                {
                    Console.WriteLine("Nebyl vybrán směr");
                }
            }
        }
    }
}
