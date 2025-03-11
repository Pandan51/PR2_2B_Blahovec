using System.ComponentModel;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Test();
        }
        public static void Test()
        {
            VacuumCleaner lux = new VacuumCleaner();
            Console.WriteLine(lux.TurnOn()); //vypíše Connected to 230 V and cleaning
            Console.WriteLine(lux.TurnOff()); //vypíše Cleaning finished

            CircularSaw sawPowerful = new CircularSaw(400);
            CircularSaw sawWeak = new CircularSaw(230);

            Lamp lamp = new Lamp(230, 7); //lampa na 230 V, dosvítí 7 m
            Console.WriteLine($"Lampa {lamp.EnlightedDistance}");
            Console.WriteLine(lamp.TurnOn()); //vypíše "Light is on"
            Console.WriteLine(lamp.TurnOff()); //vypíše "Light is off"

            ElectricDevice[] tools = { lux, sawPowerful, sawWeak, lamp };
            Gadgets gadgetSet = new Gadgets(tools);
            Console.WriteLine(gadgetSet.MaxVoltage); //vypíše 400 - nejsilnější je pila
            gadgetSet.TurnAllOn(); //vypíše postupně zapínací zvuk všech čtyř spotřebičů
            gadgetSet.TurnAllOff(); //zase všechny povypíná


            Torch torch = new Torch(3);
            WillOWisp fairy = new WillOWisp();

            ILightSource[] lights = { torch, fairy, lamp };

            lamp.TurnOn();
            Console.WriteLine(EnlightedPath(lights)); //vypíše 21
            lamp.TurnOff();
            Console.WriteLine(EnlightedPath(lights)); //vypíše 7

        }

        public static double EnlightedPath(ILightSource[] poleZdroju)
        {
            // Vrátí, jak dlouhou cestu by polem zdrojů šlo osvítit
            // takže pokud mám dva zdroje - 7m a 1 m, pak osvítím až 16 m cesty
            // (zdroj svítí na obě strany)
            double dosvit = 0;

            foreach(ILightSource x in poleZdroju)
            {
                Console.WriteLine(x.EnlightedDistance);
                dosvit += x.EnlightedDistance;
            }
            dosvit *= 2;
            return dosvit;
        }
    }

    //zde vložte vaše třídy a interface
    //nevkládejte kód s namespace


}


