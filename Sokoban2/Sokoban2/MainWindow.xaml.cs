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
    internal enum BlockStyles { Wall, Ground }
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
        private char[,] _playGrid;
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
                    if (_playGrid[i, j] == 'P')
                    {
                        _player = block;
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
                Blocks temp = _blocksGrid[_characterColumn, _characterRow - 1];

                //Blocks pPos = new Blocks();
                //Blocks newPos = new Blocks();
                //Blocks changePos = new Blocks();
                
                Grid.SetRow(temp, _characterRow);
                //change
                _blocksGrid[_characterColumn, _characterRow] = temp;

                _characterRow--;
                //change
                _blocksGrid[_characterColumn, _characterRow] = _player;
                Grid.SetRow(_player, _characterRow);

                temp.Content = "up";
                _player.Content = "up";
                

            }

        }

        private void Button_MoveLeft(object sender, RoutedEventArgs e)
        {
            if (_characterColumn > 0)
            {
                Blocks temp = _blocksGrid[_characterColumn - 1, _characterRow];

                
                Grid.SetColumn(temp, _characterColumn);

                //change
                _blocksGrid[_characterColumn, _characterRow] = temp;

                _characterColumn--;
                //change
                _blocksGrid[_characterColumn, _characterRow] = _player;

                Grid.SetColumn(_player, _characterColumn);
                temp.Content = "left";
                _player.Content = "left";
            }
        }

        private void Button_MoveDown(object sender, RoutedEventArgs e)
        {
            if (_characterRow < arrayYSize - 1)
            {
                Blocks temp = _blocksGrid[_characterColumn, _characterRow + 1];

                Grid.SetRow(temp, _characterRow);
                //Change
                _blocksGrid[_characterColumn, _characterRow] = temp;

                _characterRow++;
                //change
                _blocksGrid[_characterColumn, _characterRow] = _player;
                Grid.SetRow(_player, _characterRow);
                temp.Content = "down";
                _player.Content = "down";

            }
        }

        private void Button_MoveRight(object sender, RoutedEventArgs e)
        {
            if (_characterColumn < arrayXSize - 1)
            {
                Blocks temp = _blocksGrid[_characterColumn + 1, _characterRow];

                
                Grid.SetColumn(temp, _characterColumn);
                //change
                _blocksGrid[_characterColumn, _characterRow] = temp;

                _characterColumn++;
                //change
                _blocksGrid[_characterColumn, _characterRow] = _player;

                Grid.SetColumn(_player, _characterColumn);
                temp.Content = "right";
                _player.Content = "right";

            }
        }


        private string GetStyles(char style)
        {
            switch (style)
            {
                case 'W':
                    return "WallBlockStyle";
                case 'G':
                    return "GroundBlockStyle";
                case 'P':
                    return "PlayerBlockStyle";
                default:
                    return "G";

            }
        }

        //TODO - implement scaling
        private char[,] Create2DArray()
        {
            char[,] grid = new char[arrayXSize, arrayYSize];
            Random rand = new Random();

            // First fill the grid with random "W" or "G"
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    grid[row, col] = rand.NextDouble() < 0.3 ? 'W' : 'G'; // 30% chance wall, 70% ground
                }
            }

            //Odstranit později
            grid = Custom2DArray();
            int pRow = 2;
            int pCol = 2;
            grid[pRow, pCol] = 'P';
            _characterColumn = pCol;
            _characterRow = pRow;

            //Změna velikosti gridu na reference bloků
            Blocks[,] temp = new Blocks[arrayXSize, arrayYSize];
            _blocksGrid = temp;

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

        private char[,] Custom2DArray()
        {
            char[,] grid = new char[5, 5]
            {
                { 'W', 'W', 'W', 'W', 'W' },
                { 'W', 'G', 'G', 'G', 'W' },
                { 'W', 'G', 'P', 'G', 'W' },
                { 'W', 'G', 'G', 'G', 'W' },
                { 'W', 'W', 'W', 'W', 'W' }
            };

            return grid;
        }


    }
}
