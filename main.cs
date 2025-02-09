using main;
using System;
using System.Collections.Generic;

class Program
{
  static Random random = new Random(123456);
    public static void Main(string[] args)
    {
        Random random = new Random(123456);

      int pocetVoziku = 50;
      int casStart = 0;
      int casKonec = 12 * 60;
      int maxZakaznikuZaMinutu = 3;
      int minNakup = 5;
      int maxNakup = 45;

      Stack<Vozik> voziky = new Stack<Vozik>();
      List<Zakaznik> nakupujici = new List<Zakaznik>();

        //Vytvoøení vozíkù
        for (int i = 1; i <= pocetVoziku; i++)
        {
            Vozik vozik = new Vozik(i);
            voziky.Push(vozik);
        }

        //Vozik vozik1 = new Vozik(1);
        //voziky.Push(vozik1);
        //Vozik vozik2 = new Vozik(2);
        //voziky.Push(vozik2);
        //Vozik vozik3 = new Vozik(3);
        //voziky.Push(vozik3);

        //Test èasu
        //foreach (Vozik x in voziky)
        //{
        //    Console.WriteLine(x._cas + " "+x._zacatecniPoradi);
        //}

        //Loop dne
        for (int i = casStart; i < casKonec;i++)
        {
            //Zákazníci za min
            int zakaznici = random.Next(1, maxZakaznikuZaMinutu + 1);

            //Zakazníci berou vozíky, pokud dostupné. Nelze vzít vozík pokud je po zavírací dobì
            
                for(int j = 0; j < zakaznici;j++)
                {
                    if (zakaznici > 0 && pocetVoziku > nakupujici.Count && voziky.Count != 0 && casKonec > i)
                    {
                    Zakaznik navstevnik = new Zakaznik(random.Next(minNakup, maxNakup++),voziky.Pop());
                    nakupujici.Add(navstevnik);
                    }
                }


            //Pøiètení èasu
            foreach(Zakaznik x in nakupujici)
            {
                x._vozik._cas++;
                x._stravenyCas++;

                //if(x._stravenyCas == x._casNakupu)
                //{
                //    voziky.Push(x._vozik);
                //    nakupujici.Remove(x);
                //}
            }
            //Vrácení vozíkù
            int p = 0;
            for (int j = 0; j < nakupujici.Count; j++)
            {
                
                if (nakupujici[j]._stravenyCas == nakupujici[j]._casNakupu)
                {
                    
                    voziky.Push(nakupujici[p]._vozik);
                    nakupujici.Remove(nakupujici[p]);
                    p--;
                }
                p++;
            }





        }

        Console.WriteLine(" \n Finální hodnocení");

        foreach(Zakaznik x in nakupujici)
        {
            voziky.Push(x._vozik);
        }

        foreach(Vozik x in voziky)
        {
            Console.WriteLine($"Vozík s èíslem {x._zacatecniPoradi} má opotøebení {x._cas}");
        }
        //for(int i = 0; i < pocetVoziku; i++)
        //{
        //    Vozik vozik = voziky.Pop();
        //    Console.WriteLine($"Vozík {vozik._zacatecniPoradi} má {vozik._cas} èíslo");
        //}

    }
}
