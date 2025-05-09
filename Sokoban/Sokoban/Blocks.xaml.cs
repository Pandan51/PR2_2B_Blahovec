using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sokoban
{
    //enum GroundType { Wall, Ground, Goal}
    public enum Occupied {Empty, Player, Box}

    /// <summary>
    /// Interaction logic for Blocks.xaml
    /// </summary>
    public abstract partial class Blocks : UserControl
    {
        //private GroundType _groundType;
        //private Occupied _occupied;

        public virtual bool isWalkable => true;


        public Blocks()
        {
            InitializeComponent();
        }
    }

    public class WallBlock : Blocks
    {
        public override bool isWalkable => false;
    }

    public class GroundBlock : Blocks
    {
        private Occupied _content;
        public override bool isWalkable
        {
            get
            {
                if(content == Occupied.Empty)
                {
                    return true;
                }
                return false;
            }
            //TODO - implementace box logiky
        }
        
        // content - empty, player, box
        public virtual Occupied content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }

        
    }

    public class GoalBlock : GroundBlock
    {
        //zatím stejné
    }

}
