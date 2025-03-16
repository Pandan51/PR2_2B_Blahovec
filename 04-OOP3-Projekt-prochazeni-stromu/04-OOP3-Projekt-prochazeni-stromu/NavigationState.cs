using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    internal class NavigationState
    {
        // Private static instance (holds all the data)
        private static NavigationState instance;

        // Public static property to access the instance (creates it if needed)
        public static NavigationState Instance
        {
            get
            {
                if (instance == null)
                    instance = new NavigationState();
                return instance;
            }
        }

        // Variables that store the state
        public int Count { get; set; } = -2;
        public bool DisplayMode { get; set; } = true;
        public Salesman Current { get; set; }
        public Salesman Boss { get; private set; }
        public List<Salesman> Marked { get; set; } = new List<Salesman>();

        // Private constructor to prevent external instantiation
        private NavigationState() { }

        
    }
}
