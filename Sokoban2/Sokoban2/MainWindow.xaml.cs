﻿using System.Text;
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

        private int arrayXSize = 5;
        private int arrayYSize = 5;
        private Blocks _player;
        private char[,] _playGrid;
        public int _characterX
        {
            get { return (int)GetValue(_characterXProperty); }
            set { SetValue(_characterXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for _characterX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty _characterXProperty =
            DependencyProperty.Register("_characterX", typeof(int), typeof(MainWindow), new PropertyMetadata(2));



        public int _characterY
        {
            get { return (int)GetValue(_characterYProperty); }
            set { SetValue(_characterYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for _characterY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty _characterYProperty =
            DependencyProperty.Register("_characterY", typeof(int), typeof(MainWindow), new PropertyMetadata(2));


        private void Start()
        {
            _playGrid = Create2DArray();

            arrayXSize = _playGrid.GetLength(1);
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
            if (_characterY > 0)
            {

                _characterY--;
                Grid.SetRow(_player, _characterY);
                Grid.SetColumn(_player, _characterX);
            }

        }

        private void Button_MoveLeft(object sender, RoutedEventArgs e)
        {
            if (_characterX > 0)
            {
                _characterX--;
                Grid.SetRow(_player, _characterY);
                Grid.SetColumn(_player, _characterX);
            }
        }

        private void Button_MoveDown(object sender, RoutedEventArgs e)
        {
            if (_characterY < arrayYSize)
            {
                _characterY++;
                Grid.SetRow(_player, _characterY);
                Grid.SetColumn(_player, _characterX);
            }
        }

        private void Button_MoveRight(object sender, RoutedEventArgs e)
        {
            if (_characterX < arrayXSize)
            {
                _characterX++;
                Grid.SetRow(_player, _characterY);
                Grid.SetColumn(_player, _characterX);
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

            // Now place exactly one "P" at a random location
            int playerRow = rand.Next(5);
            int playerCol = rand.Next(5);
            grid[playerRow, playerCol] = 'P';
            _characterX = playerCol;
            _characterY = playerRow;

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
    }
}
