namespace Lanovka___new_solution
{
    internal class Lanovka
    {

        public int Delka { get; init; }
        public int Nosnost { get; init; }
        private Sedacka _dolniSedacka = null; //mám uložený odkaz na hodní a dolní sedačku
        private Sedacka _horniSedacka = null;

        internal int weight = 0;
        internal bool _jeVolnoNahore;
        internal bool _jeVolnoDole;
        public bool JeVolnoDole
        {
            get
            {
                return _jeVolnoDole;
            }
            set
            {
                Sedacka index = new Sedacka();
                index = _horniSedacka;
                _jeVolnoDole = false;

                for(int i = 1; i < Delka; i++)
                {
                    if (index != _dolniSedacka)
                    {
                        index = index.Predchozi;
                    }
                    else
                    {
                        _jeVolnoDole = true;
                        break;  
                    }
                    
                }
            }
        }
        public bool JeVolnoNahore
        {
            get
            {
                return _jeVolnoNahore;
            }
            set
            {
                Sedacka index = new Sedacka();
                index = _horniSedacka;
                _jeVolnoNahore = false;

                for (int i = 1; i < 5; i++)
                {
                    if (index != _dolniSedacka)
                    {
                        index = index.Predchozi;
                    }
                    else
                    {
                        _jeVolnoNahore = true;
                    }

                }

            }
        }
        public int Zatizeni
        {

            get
            {
                return weight;
            }
            set
            {
                weight = 0;
                Sedacka index = new Sedacka();
                index = _horniSedacka;

               while(index != _dolniSedacka)
               { 
                    if(index.Pasazer != null)
                    {
                        weight += index.Pasazer.Hmotnost;
                    }
                    index = index.Predchozi;
                }
            }
        }
        public Lanovka(int delka, int nosnost, Sedacka nastupujici)
        {
            Delka = delka;
            Nosnost = nosnost;

            _horniSedacka = nastupujici;
            _dolniSedacka = nastupujici;
            _jeVolnoDole = false;
            _jeVolnoNahore = true;

            //if(_horniSedacka == null)
            //{
            //    _horniSedacka = nastupujici;
            //    _dolniSedacka = nastupujici;
            //}
            //else if (_horniSedacka != null)
            //{
            //    _horniSedacka.Predchozi = nastupujici;
            //    _dolniSedacka = nastupujici;
            //}
            //else
            //{
            //    _dolniSedacka.Predchozi = nastupujici;
            //    _dolniSedacka = nastupujici;
            //}

            
            // Tady je potřeba vytvořit jednu sedačku, zapamatovat si ji třeba jako horní a pak postupně dodělávat další a propojit je mezi sebou. Poslední si pak uložíte jako dolní
        }
        public bool Nastup(Clovek clovek) //tohle je teď velmi snadné, mám uložený přímý odkaz na sedačku
        {
            
            if (!JeVolnoDole)
            {
                return false;
            }
            if (Zatizeni + clovek.Hmotnost > Nosnost)
            {
                return false;
            }

            Sedacka sedacka = new Sedacka();
            sedacka.Pasazer = clovek;
            _dolniSedacka.Predchozi = sedacka;
            _dolniSedacka = sedacka;
            _jeVolnoDole = false;

            return true;
        }
        public Clovek Vystup() //tohle je teď velmi snadné, protože mám přímý odkaz na sedačku
        {
            if (_jeVolnoNahore == false)
            {
                Clovek pasazer = _horniSedacka.Pasazer;
                _horniSedacka.Pasazer = null;
                _jeVolnoNahore = true;
                return pasazer;
            }
            else
            {
                throw new Exception("Horní místo je prázdné");
            }
        }
        public void Jed()
        {
            if (!JeVolnoNahore)
                throw new Exception("Nelze jet s clovekem nahore");
            else
            {
                _jeVolnoDole = true;
            }
            // A teď musíme horní sedačku odpojit, na její předchozí namířit ukazatel „horní sedačka“ a dospod tu původní připojit nebo vytvořit novou. A zase správně zapojit
        }

        public void Vypis()
        {
            Sedacka index = new Sedacka();
            index = _horniSedacka;

            do
            {
                Console.WriteLine(index.Pasazer.Jmeno);
                index = index.Predchozi;
                
            } while (index != null);





        }
    }
}
