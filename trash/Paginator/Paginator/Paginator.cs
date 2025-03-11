using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paginator
{
    internal class Paginator
    {
        private string[] _pole;
        private int _itemCount;
        private int _pageCount;
        private int _pageCount_items;
        public int ItemCount
        {
            get
            {
                return _itemCount;
            }
            set
            {
                _itemCount = value;
            }
        }

        public int PageCount
        {
            get
            {
                return _pageCount_items;
            }
            set
            {
                _pageCount_items = value;
            }
        }

        public Paginator(string[] array, int pocet)
        {
            _pole = array;
            ItemCount = array.Length;
            PageCount = pocet / 2;
            _pageCount_items = pocet;
        }


        public int GetPageItemCount(int n)
        {
            
            int x = _itemCount - ((n) * _pageCount_items);
            if(x > 3)
            {
                x = 3;
            }
            else if(x <0)
            {
                x = 0;
            }

            if (x > 0)
                return x;
            else
                return 0;

        }
        public string[] GetPage(int i)
        {
            if (GetPageItemCount(i) != 0)
            {
                int help = _pageCount_items * i;
                string[] array = new string[GetPageItemCount(i)];
                for (int x = 0 ; x < array.Length; x++)
                {
                    array[x] = _pole[x+help];
                }
                return array;
            }
            else
            {
                string[] array =  {"Prázdno"};
                return array;
                    }

            
        }
        public int FindPage(string s)
        {
            
            int count = 0;
            while(true)
            {
                if (count >= _itemCount)
                {
                    count = -1;
                    break;
                }
                else if (_pole[count] == s)
                {
                    count /= _pageCount_items;
                    break;
                }
                count++;

                
                count++;
            }
            return count;
             
        }
    }
}
