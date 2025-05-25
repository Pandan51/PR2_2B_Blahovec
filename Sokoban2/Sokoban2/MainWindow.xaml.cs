using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sokoban2
{
    internal enum BlockStyles { Wall, Ground, Target, Box, BoxTarget, Player, PlayerTarget }
    internal enum Direction { Up, Left, Down, Right}
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Velikosti 2d polí
        private int arrayXSize = 5;
        private int arrayYSize = 5;
        //Hráč
        private Blocks _player;
        //
        private BlockStyles[,] _playGrid;
        private Blocks[,] _blocksGrid;

        //Sloupec (_characterX)

        private int _characterColumn = 2;
        private int _characterRow = 2;
        



        //public int _characterColumn
        //{
        //    get { return (int)GetValue(_characterColumnProperty); }
        //    set { SetValue(_characterColumnProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for _characterColumn.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty _characterColumnProperty =
        //    DependencyProperty.Register("_characterColumn", typeof(int), typeof(MainWindow), new PropertyMetadata(2));



        //Řádek (_characterY)



        //public int _characterRow
        //{
        //    get { return (int)GetValue(_characterRowProperty); }
        //    set { SetValue(_characterRowProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for _characterRow.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty _characterRowProperty =
        //    DependencyProperty.Register("_characterRow", typeof(int), typeof(MainWindow), new PropertyMetadata(2));




        private void Start()
        {
            _playGrid = Create2DArray();

            

            //sloupec
            arrayXSize = _playGrid.GetLength(1);
            //řádek
            arrayYSize = _playGrid.GetLength(0);

            //Reference na bloky
            _blocksGrid = new Blocks[arrayYSize, arrayXSize];
            //promažu, kdyby tam něco bylo

            PlayingFieldGrid.ColumnDefinitions.Clear();
            PlayingFieldGrid.RowDefinitions.Clear();

            //připravím řádky, sloupce
            //Zatím stále,
            //TODO později implementovat velikost
            for (int i = 0; i < arrayXSize; i++)
            {
                PlayingFieldGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < arrayYSize; i++)
            {
                PlayingFieldGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < arrayYSize; i++)
            {
                for (int j = 0; j < arrayXSize; j++)
                {
                    //if (i != arrayXSize && j != arrayYSize)
                    //{
                    Blocks block = new Blocks();
                    _blocksGrid[j, i] = block;

                    block.Style = (Style)FindResource(GetStyles(_playGrid[i, j]));
                    Grid.SetRow(block, i);
                    Grid.SetColumn(block, j);
                    //Test
                    if (_playGrid[i, j] == BlockStyles.Player)
                    {
                        _player = block;
                        _player.Content = "player";
                    }
                    PlayingFieldGrid.Children.Add(block);
                    //}
                }
            }


        }
        public MainWindow()
        {
            InitializeComponent();
            Start();
        }

        private void Button_MoveUp(object sender, RoutedEventArgs e)
        {
            if (_characterRow > 0)
            {
                Move(Direction.Up, 0,-1);
            }

        }

        private void Button_MoveLeft(object sender, RoutedEventArgs e)
        {
            if (_characterColumn > 0)
            {
                Move(Direction.Left, -1,0);
            }
        }

        private void Button_MoveDown(object sender, RoutedEventArgs e)
        {
            if (_characterRow < arrayYSize - 1)
            {
                Move(Direction.Down,0,1);
            }
        }

        private void Button_MoveRight(object sender, RoutedEventArgs e)
        {
            if (_characterColumn < arrayXSize - 1)
            {
                Move(Direction.Right,1,0);
            }
        }


        private string GetStyles(BlockStyles style)
        {
            switch (style)
            {
                case BlockStyles.Wall:
                    return "WallBlockStyle";
                case BlockStyles.Ground:
                    return "GroundBlockStyle";
                case BlockStyles.Player:
                    return "PlayerBlockStyle";
                case BlockStyles.Box:
                    return "BoxBlockStyle";
                case BlockStyles.Target:
                    return "GoalBlockStyle";
                case BlockStyles.BoxTarget:
                    return "BoxGoalBlockStyle";
                case BlockStyles.PlayerTarget:
                    return "PlayerGoalBlockStyle";
                default:
                    return "GroundBlockStyle";

            }
        }

        //TODO - implement scaling
        private BlockStyles[,] Create2DArray()
        {
            BlockStyles[,] grid = new BlockStyles[arrayXSize, arrayYSize];
            Random rand = new Random();

            //// First fill the grid with random "W" or "G"
            //for (int row = 0; row < grid.GetLength(0); row++)
            //{
            //    for (int col = 0; col < grid.GetLength(1); col++)
            //    {
            //        grid[row, col] = rand.NextDouble() < 0.3 ? 'W' : 'G'; // 30% chance wall, 70% ground
            //    }
            //}

            //Odstranit později
            grid = Custom2DArray(10);

            for(int i = 0; i < grid.GetLength(0);i++)
            {
                for(int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i,j]==BlockStyles.Player)
                    {
                        _characterColumn = j;
                        _characterRow = i;
                    }
                }
            }
            

            //Změna velikosti gridu na reference bloků
            

            // Now place exactly one "P" at a random location

            //Random player

            //int pRow = rand.Next(5);
            //int pCol = rand.Next(5);
            //grid[pRow, pCol] = 'P';
            //_characterColumn = pCol;
            //_characterRow = pRow;

            //Print the grid
            //for (int row = 0; row < 5; row++)
            //{
            //    for (int col = 0; col < 5; col++)
            //    {
            //        Console.Write(grid[row, col] + " ");
            //    }
            //    Console.WriteLine();
            //}

            return grid;
        }

        private BlockStyles[,] Custom2DArray(int size)
        {
            
            if (size == 5)
            {
                BlockStyles[,] grid = new BlockStyles[5, 5]
                {
                { BlockStyles.Wall,  BlockStyles.Wall,  BlockStyles.Wall,  BlockStyles.Wall,  BlockStyles.Wall },
            { BlockStyles.Wall,  BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall },
            { BlockStyles.Wall,  BlockStyles.Ground, BlockStyles.Player, BlockStyles.Ground, BlockStyles.Wall },
            { BlockStyles.Wall,  BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall },
            { BlockStyles.Wall,  BlockStyles.Wall,  BlockStyles.Wall,  BlockStyles.Wall,  BlockStyles.Wall }
        };
                return grid;
            }
            else if (size == 10)
            {
                BlockStyles[,] grid = new BlockStyles[10,10]
               {
                { BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall },
            { BlockStyles.Wall, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall },
            { BlockStyles.Wall, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall },
            { BlockStyles.Wall, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall },
            { BlockStyles.Wall, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall, BlockStyles.Target, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall },
            { BlockStyles.Wall, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall, BlockStyles.Box, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall },
            { BlockStyles.Wall, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Player, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall },
            { BlockStyles.Wall, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall },
            { BlockStyles.Wall, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall },
            { BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall }
        };
                return grid;
            }
            else
            {
                return null;
            }
            

            
        }

        private void Move(Direction direction, int columnDir, int rowDir)
        {
            columnDir = columnDir * 1;
            rowDir = rowDir * 1;

                //Blok ve směru
                Blocks nextBlock = _blocksGrid[_characterColumn + (columnDir), _characterRow + (rowDir)];

                //Kontrola, jestli blok je zeď
                if (nextBlock.Style != (Style)FindResource(GetStyles(BlockStyles.Wall)))
                {


                    Grid.SetRow(nextBlock, _characterRow);
                    Grid.SetColumn(nextBlock, _characterColumn);
                    //change
                    _blocksGrid[_characterColumn, _characterRow] = nextBlock;


                    _characterRow += rowDir;
                    _characterColumn += columnDir;
                    //change
                    _blocksGrid[_characterColumn, _characterRow] = _player;
                    Grid.SetRow(_player, _characterRow);
                    Grid.SetColumn(_player, _characterColumn);

            }

            //if(direction == Direction.Up)
            //{
            //    //Blok ve směru
            //    Blocks nextBlock = _blocksGrid[_characterColumn, _characterRow - 1];

            //    //Kontrola, jestli blok je zeď
            //    if (nextBlock.Style != (Style)FindResource(GetStyles(BlockStyles.Wall)))
            //    {


            //        Grid.SetRow(nextBlock, _characterRow);
            //        //change
            //        _blocksGrid[_characterColumn, _characterRow] = nextBlock;

            //        _characterRow--;
            //        //change
            //        _blocksGrid[_characterColumn, _characterRow] = _player;
            //        Grid.SetRow(_player, _characterRow);

            //    }
            //}
            //else if (direction == Direction.Left)
            //{
            //    Blocks nextBlock = _blocksGrid[_characterColumn - 1, _characterRow];

            //    //Kontrola, jestli blok je zeď
            //    if (nextBlock.Style != (Style)FindResource(GetStyles(BlockStyles.Wall)))
            //    {

            //        Grid.SetColumn(nextBlock, _characterColumn);

            //        //change
            //        _blocksGrid[_characterColumn, _characterRow] = nextBlock;

            //        _characterColumn--;
            //        //change
            //        _blocksGrid[_characterColumn, _characterRow] = _player;

            //        Grid.SetColumn(_player, _characterColumn);

            //    }
            //}
            //else if (direction == Direction.Down)
            //{
            //    Blocks nextBlock = _blocksGrid[_characterColumn, _characterRow + 1];

            //    //Kontrola, jestli blok je zeď
            //    if (nextBlock.Style != (Style)FindResource(GetStyles(BlockStyles.Wall)))
            //    {
            //        Grid.SetRow(nextBlock, _characterRow);
            //        //Change
            //        _blocksGrid[_characterColumn, _characterRow] = nextBlock;

            //        _characterRow++;
            //        //change
            //        _blocksGrid[_characterColumn, _characterRow] = _player;
            //        Grid.SetRow(_player, _characterRow);

            //    }
            //}
            //else if (direction == Direction.Right)
            //{
            //    Blocks nextBlock = _blocksGrid[_characterColumn + 1, _characterRow];
            //    //Kontrola, jestli blok je zeď
            //    if (nextBlock.Style != (Style)FindResource(GetStyles(BlockStyles.Wall)))
            //    {

            //        Grid.SetColumn(nextBlock, _characterColumn);
            //        //change
            //        _blocksGrid[_characterColumn, _characterRow] = nextBlock;

            //        _characterColumn++;
            //        //change
            //        _blocksGrid[_characterColumn, _characterRow] = _player;

            //        Grid.SetColumn(_player, _characterColumn);

            //    }
            //}
        }


    }
}
