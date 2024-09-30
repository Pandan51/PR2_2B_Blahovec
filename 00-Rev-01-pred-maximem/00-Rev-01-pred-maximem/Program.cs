using System.ComponentModel.Design;

namespace _00_Rev_01_pred_maximem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string x = "";
            int biggest = 0;
            int before = -999;
            bool first = true;
            bool end = false;
            
            int last_num = 0;

            

            while (x != "dost")
            {
                Console.WriteLine("Piš celá čísla");
                x = Console.ReadLine();

                if(x == "dost" && first == true)
                {
                    end = true;
                }
                else if(x == "dost")
                {
                    break;
                }

                if (first)
                {
                    try
                    {
                        biggest = int.Parse(x);
                        first = false;
                        last_num = int.Parse(x);
                        


                    }
                    catch
                    {

                    }
                }
                else
                {
                    try
                    {
                        if (biggest < int.Parse(x))
                        {
                            before = last_num;
                            biggest = int.Parse(x);
                            
                        }
                        last_num = int.Parse(x);



                    }
                    catch
                    {

                    }
                }
                
               
            }
            if(end == true)
            {
                Console.WriteLine("Nebylo vloženo žádné číslo");
            }
            else if(before == -999)
            {
                Console.WriteLine(last_num);
            }
            else 
            {
            Console.WriteLine(before);
            }
        }

    }
}
    
