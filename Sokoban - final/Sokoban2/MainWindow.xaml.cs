using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Input.Manipulations;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;
using System.Text.Json;
using System.IO;
using System.Windows.Controls.Primitives;
using System.Diagnostics;


namespace Sokoban2
{
    internal enum BlockStyles { 
        Wall=0,
        Ground=1,
        Target=2,
        Box=3,
        BoxTarget=4,
        Player=5,
        PlayerTarget=6 }
    

    

    internal enum GameStage {Victory, PlayStage, LevelSelect}
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Velikosti 2d polí
        private int arrayXSize_column = 5;
        private int arrayYSize_row = 5;
        //Hráč
        private Blocks _player;

        //
        //private BlockStyles[,] _startLayout;
        private BlockStyles[,] _playGrid;
        private Blocks[,] _blocksGrid;

        //Sloupec (_characterX)

        private int _characterColumn = 2;
        private int _characterRow = 2;
        private GameStage _gameStage = GameStage.PlayStage;

        private int _targetBlockCount = 0;
        private int _activeTargetBlocks = 0;

        private BlockStyles[,] _startPos;
        private Stack<BlockStyles[,]> _undoArray = new Stack<BlockStyles[,]>();
        //Levels list
        List<LevelData> levels;


        public MainWindow()
        {
            InitializeComponent();
            LoadLevels();
            //_playGrid = ParseLevel(levels[0].grid);
            ////_startLayout = _playGrid;
            //CreatePlayfield(_playGrid);


        }


        #region Gameplay
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (_gameStage == GameStage.PlayStage)
            {
                base.OnKeyDown(e);

                switch (e.Key)
                {
                    case Key.W: // Up
                        if (_characterRow > 0)
                        {
                            Move(0, -1);
                        }
                        break;

                    case Key.S: // Down
                        if (_characterRow < arrayYSize_row - 1)
                        {
                            Move(0, 1);
                        }
                        break;
                    case Key.A: // Left
                        if (_characterColumn > 0)
                        {
                            Move(-1, 0);
                        }
                        break;
                    case Key.D: // Right
                        if (_characterColumn < arrayXSize_column - 1)
                        {
                            Move(1, 0);
                        }
                        break;
                }
            }
        }
        private void Move(int columnDir, int rowDir)
        {


            //Bloky ve směru
            //nextBlock je vedle hráče
            Blocks nextBlock = _blocksGrid[_characterRow + (rowDir), _characterColumn + columnDir];
            Blocks afterBlock = new();
            //afterBlock je za nextBlock
            if (nextBlock.Style != (Style)FindResource(GetStyles(BlockStyles.Wall)))
                afterBlock = _blocksGrid[_characterRow + (2 * rowDir), _characterColumn + (2 * columnDir)];







            ////Kontrola, jestli blok je zeď
            ////Swap
            //if (nextBlock.Style != (Style)FindResource(GetStyles(BlockStyles.Wall)))
            //{


            //    Grid.SetRow(nextBlock, _characterRow);
            //    Grid.SetColumn(nextBlock, _characterColumn);
            //    //change
            //    _blocksGrid[_characterColumn, _characterRow] = nextBlock;


            //    _characterRow += rowDir;
            //    _characterColumn += columnDir;
            //    //change
            //    _blocksGrid[_characterColumn, _characterRow] = _player;
            //    Grid.SetRow(_player, _characterRow);
            //    Grid.SetColumn(_player, _characterColumn);

            //}

            //Next block
            switch (_playGrid[Grid.GetRow(nextBlock), Grid.GetColumn(nextBlock)])
            {
                case BlockStyles.Wall:
                    break;



                case BlockStyles.Ground:
                    UndoSave(_playGrid);
                    int[] PPos = { Grid.GetRow(_player), Grid.GetColumn(_player) };
                    int[] NBPos = { Grid.GetRow(nextBlock), Grid.GetColumn(nextBlock) };

                    AutoStyle(nextBlock, BlockStyles.Player);
                    _playGrid[NBPos[0], NBPos[1]] = BlockStyles.Player;


                    if (_playGrid[PPos[0], PPos[1]] == BlockStyles.Player)
                    {
                        AutoStyle(_player, BlockStyles.Ground);
                        //_player.Content = "Ground";
                        _playGrid[PPos[0], PPos[1]] = BlockStyles.Ground;
                    }
                    else
                    {
                        AutoStyle(_player, BlockStyles.Target);
                        //_player.Content = "Target";
                        _playGrid[PPos[0], PPos[1]] = BlockStyles.Target;
                    }



                    _characterColumn += columnDir;
                    _characterRow += rowDir;

                    _player = nextBlock;
                    //UNDO

                    break;
                case BlockStyles.Target:
                    UndoSave(_playGrid);
                    PPos = new int[2] { Grid.GetRow(_player), Grid.GetColumn(_player) };
                    NBPos = new int[2] { Grid.GetRow(nextBlock), Grid.GetColumn(nextBlock) };

                    AutoStyle(nextBlock, BlockStyles.PlayerTarget);
                    _playGrid[NBPos[0], NBPos[1]] = BlockStyles.PlayerTarget;

                    if (_playGrid[PPos[0], PPos[1]] == BlockStyles.Player)
                    {
                        AutoStyle(_player, BlockStyles.Ground);
                        //_player.Content = "Ground";
                        _playGrid[PPos[0], PPos[1]] = BlockStyles.Ground;
                    }
                    else
                    {
                        AutoStyle(_player, BlockStyles.Target);
                        //_player.Content = "Target";
                        _playGrid[PPos[0], PPos[1]] = BlockStyles.Target;
                    }



                    _characterColumn += columnDir;
                    _characterRow += rowDir;

                    _player = nextBlock;
                    //UNDO

                    break;
                case BlockStyles.Box:

                    PPos = new int[2] { Grid.GetRow(_player), Grid.GetColumn(_player) };
                    NBPos = new int[2] { Grid.GetRow(nextBlock), Grid.GetColumn(nextBlock) };
                    int[] ABPos = new int[2] { Grid.GetRow(afterBlock), Grid.GetColumn(afterBlock) };

                    if (_playGrid[ABPos[0], ABPos[1]] == BlockStyles.Wall || _playGrid[ABPos[0], ABPos[1]] == BlockStyles.Box || _playGrid[ABPos[0], ABPos[1]] == BlockStyles.BoxTarget)
                    {
                        break;
                    }
                    UndoSave(_playGrid);

                    if (_playGrid[ABPos[0], ABPos[1]] == BlockStyles.Ground)
                    {
                        _playGrid[ABPos[0], ABPos[1]] = BlockStyles.Box;
                        AutoStyle(afterBlock, BlockStyles.Box);

                    }
                    else
                    {
                        _playGrid[ABPos[0], ABPos[1]] = BlockStyles.BoxTarget;
                        AutoStyle(afterBlock, BlockStyles.BoxTarget);
                        _activeTargetBlocks++;
                    }

                    _playGrid[NBPos[0], NBPos[1]] = BlockStyles.Player;
                    AutoStyle(nextBlock, BlockStyles.Player);


                    if (_playGrid[PPos[0], PPos[1]] == BlockStyles.Player)
                    {
                        AutoStyle(_player, BlockStyles.Ground);
                        //_player.Content = "Ground";
                        _playGrid[PPos[0], PPos[1]] = BlockStyles.Ground;
                    }
                    else
                    {
                        AutoStyle(_player, BlockStyles.Target);
                        //_player.Content = "Target";
                        _playGrid[PPos[0], PPos[1]] = BlockStyles.Target;
                    }

                    _characterColumn += columnDir;
                    _characterRow += rowDir;

                    _player = nextBlock;
                    WinCheck();
                    //UNDO


                    break;
                case BlockStyles.BoxTarget:

                    PPos = new int[2] { Grid.GetRow(_player), Grid.GetColumn(_player) };
                    NBPos = new int[2] { Grid.GetRow(nextBlock), Grid.GetColumn(nextBlock) };
                    ABPos = new int[2] { Grid.GetRow(afterBlock), Grid.GetColumn(afterBlock) };

                    if (_playGrid[ABPos[0], ABPos[1]] == BlockStyles.Wall || _playGrid[ABPos[0], ABPos[1]] == BlockStyles.Box || _playGrid[ABPos[0], ABPos[1]] == BlockStyles.BoxTarget)
                    {
                        break;
                    }
                    UndoSave(_playGrid);
                    if (_playGrid[ABPos[0], ABPos[1]] == BlockStyles.Ground)
                    {
                        _playGrid[ABPos[0], ABPos[1]] = BlockStyles.Box;
                        AutoStyle(afterBlock, BlockStyles.Box);
                        _activeTargetBlocks--;
                    }
                    else
                    {
                        _playGrid[ABPos[0], ABPos[1]] = BlockStyles.BoxTarget;
                        AutoStyle(afterBlock, BlockStyles.BoxTarget);
                    }


                    _playGrid[NBPos[0], NBPos[1]] = BlockStyles.PlayerTarget;
                    AutoStyle(nextBlock, BlockStyles.PlayerTarget);


                    if (_playGrid[PPos[0], PPos[1]] == BlockStyles.Player)
                    {
                        AutoStyle(_player, BlockStyles.Ground);
                        //_player.Content = "Ground";
                        _playGrid[PPos[0], PPos[1]] = BlockStyles.Ground;
                    }
                    else
                    {
                        AutoStyle(_player, BlockStyles.Target);
                        //_player.Content = "Target";
                        _playGrid[PPos[0], PPos[1]] = BlockStyles.Target;
                    }

                    _characterColumn += columnDir;
                    _characterRow += rowDir;

                    _player = nextBlock;
                    //UNDO


                    break;

            }




        }
        private void WinCheck()
        {
            if (_targetBlockCount == _activeTargetBlocks)
            {
                _gameStage = GameStage.Victory;
                VictoryGrid.Visibility = Visibility.Visible;
                LayoutGrid.Visibility = Visibility.Hidden;
            }
        }

        #region MoveButtons 

        private void Button_MoveUp(object sender, RoutedEventArgs e)
        {
            if (_characterRow > 0)
            {
                Move(0, -1);
            }

        }

        private void Button_MoveLeft(object sender, RoutedEventArgs e)
        {
            if (_characterColumn > 0)
            {
                Move(-1, 0);
            }
        }

        private void Button_MoveDown(object sender, RoutedEventArgs e)
        {
            if (_characterRow < arrayYSize_row - 1)
            {
                Move(0, 1);
            }
        }

        private void Button_MoveRight(object sender, RoutedEventArgs e)
        {
            if (_characterColumn < arrayXSize_column - 1)
            {
                Move(1, 0);
            }
        }
        #endregion
        private void Reset(object sender, RoutedEventArgs e)
        {

            //_playGrid = ParseLevel(levels[0].grid);
            //_playGrid = _startPos;

            Array.Copy(_startPos, _playGrid, _startPos.Length);
            _undoArray.Clear();
            CreatePlayfield(_playGrid);




        }
        private void Undo_Rollback(object sender, RoutedEventArgs e)
        {
            if (_undoArray.Count > 0)
            {

                _playGrid = _undoArray.Pop();
                CreatePlayfield(_playGrid);
            }
        }
        private void UndoSave(BlockStyles[,] newSave)
        {
            BlockStyles[,] tempPush = new BlockStyles[newSave.GetLength(0), newSave.GetLength(1)];
            Array.Copy(newSave, tempPush, newSave.Length);
            _undoArray.Push(tempPush);
        }
        #endregion


        #region UI
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
        private void AutoStyle(Blocks blockTarget, BlockStyles blockTarget_style)
        {
            blockTarget.Style = (Style)FindResource(GetStyles(blockTarget_style));
        }
        #endregion

        #region Levels
        private void ShowHide_Btn(object sender, RoutedEventArgs e)
        {
            if (_gameStage == GameStage.Victory)
            {
                _gameStage = GameStage.PlayStage;
                VictoryGrid.Visibility = Visibility.Hidden;
                LayoutGrid.Visibility = Visibility.Visible;
            }
            else if (_gameStage == GameStage.PlayStage)
            {
                _gameStage = GameStage.Victory;
                VictoryGrid.Visibility = Visibility.Visible;
                LayoutGrid.Visibility = Visibility.Hidden;


            }
        }
        private void CreatePlayfield(BlockStyles[,] sentPlayfield)
        {

            int targetCount = 0;
            int activeTarget = 0;
            //sloupec
            arrayXSize_column = _playGrid.GetLength(1);
            //řádek
            arrayYSize_row = _playGrid.GetLength(0);

            //Reference na bloky
            _blocksGrid = new Blocks[arrayYSize_row, arrayXSize_column];
            //promažu, kdyby tam něco bylo

            PlayingFieldGrid.ColumnDefinitions.Clear();
            PlayingFieldGrid.RowDefinitions.Clear();

            var cellSize = new System.Windows.GridLength(50);

            //připravím řádky, sloupce
            //Zatím stále,
            //TODO později implementovat velikost
            for (int i = 0; i < arrayXSize_column; i++)
            {
                var colDef = new ColumnDefinition();
                colDef.Width = cellSize;



                PlayingFieldGrid.ColumnDefinitions.Add(colDef);
            }

            for (int i = 0; i < arrayYSize_row; i++)
            {
                var rowDef = new RowDefinition();
                rowDef.Height = cellSize;
                PlayingFieldGrid.RowDefinitions.Add(rowDef);
            }

            for (int i = 0; i < arrayYSize_row; i++)
            {
                for (int j = 0; j < arrayXSize_column; j++)
                {
                    //if (i != arrayXSize && j != arrayYSize)
                    //{
                    Blocks block = new Blocks();
                    _blocksGrid[i, j] = block;

                    block.Style = (Style)FindResource(GetStyles(sentPlayfield[i, j]));
                    if (sentPlayfield[i, j] == BlockStyles.Player || sentPlayfield[i, j] == BlockStyles.PlayerTarget)
                    {
                        _characterColumn = j;
                        _characterRow = i;
                        _player = block;
                    }

                    if (sentPlayfield[i, j] == BlockStyles.Target || sentPlayfield[i, j] == BlockStyles.PlayerTarget)
                    {
                        targetCount++;

                    }
                    else if (sentPlayfield[i, j] == BlockStyles.BoxTarget)
                    {
                        targetCount++;
                        activeTarget++;
                    }
                    Grid.SetRow(block, i);
                    Grid.SetColumn(block, j);
                    //Test
                    if (sentPlayfield[i, j] == BlockStyles.Player)
                    {
                        _player = block;

                    }
                    PlayingFieldGrid.Children.Add(block);
                    //}
                }
            }

            _targetBlockCount = targetCount;
            _activeTargetBlocks = activeTarget;

        }
        internal void LoadLevels()
        {
            try
            {
                string projectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                string jsonPath = Path.Combine(projectRoot, "Levels.json");


                //List<LevelData> json = JsonSerializer.Deserialize<List<LevelData>>();
                //levels = json;
                string jsonText = File.ReadAllText(jsonPath);



                // Deserialize into a List<LevelData>
                levels = JsonSerializer.Deserialize<List<LevelData>>(jsonText);










                int totalLevels = levels.Count; // Your total levels
                int columns = (int)Math.Ceiling(Math.Sqrt(totalLevels)); // Columns ≈ √N (rounded up)
                int rows = (int)Math.Ceiling((double)totalLevels / columns); // Rows = Total / Columns (rounded up)

                LevelsGrid.Children.Clear();
                LevelsGrid.ColumnDefinitions.Clear();
                LevelsGrid.RowDefinitions.Clear();

                for (int i = 0; i < columns; i++)
                    LevelsGrid.ColumnDefinitions.Add(new ColumnDefinition());
                for (int i = 0; i < columns; i++)
                    LevelsGrid.RowDefinitions.Add(new RowDefinition());



                //for (int j = 1; j <= rows; j++)
                //{
                //    for (int i = 1; i <= columns; i++)
                //    {
                //        Button btn = new Button();
                //        btn.Content = $"{i + (columns * (j - 1))}";
                //        Grid.SetColumn(btn, i - 1);
                //        Grid.SetRow(btn, j - 1);
                //        //btn.Background = ;

                //        LevelsGrid.Children.Add(btn);
                //    }
                //}
                int row = 1;
                int col = 1;
                int count = 0;
                do
                {


                    Button btn = new Button();
                    btn.Content = $"{col + (columns * (row - 1))}";
                    Grid.SetColumn(btn, col - 1);
                    Grid.SetRow(btn, row - 1);

                    switch(levels[count].color)
                    {
                        case 1:
                            btn.Background = Brushes.Green;
                            break;
                        case 2:
                            btn.Background = Brushes.Yellow;
                            break;
                        case 3:
                            btn.Background = Brushes.Orange;
                            break;
                        case 4:
                            btn.Background = Brushes.Red;
                            break;
                    }
                    btn.FontSize = 48;
                    var thick = new System.Windows.Thickness(10);
                    btn.Margin = thick;

                    LevelsGrid.Children.Add(btn);
                    btn.Click += new RoutedEventHandler(LevelSelectBtn_Click);

                    LevelsGrid.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(LevelSelectBtn_Click));
                    if (col == columns)
                    {
                        row++;
                        col = 0;
                    }
                    col++;
                    count++;

                }
                while (count < totalLevels);


            }
            catch
            {

            }
        }

        private void LevelSelection(object sender, RoutedEventArgs e)
        {
            _gameStage = GameStage.LevelSelect;
            VictoryGrid.Visibility = Visibility.Hidden;
            LayoutGrid.Visibility = Visibility.Hidden;
            LevelsGrid.Visibility = Visibility.Visible;
            _undoArray.Clear();
        }

        private void LevelSelectBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                int buttonText = int.Parse(button.Content.ToString());
                //MessageBox.Show($"You clicked: {buttonText}");

                foreach (LevelData x in levels)
                {
                    if (x.level == buttonText)
                    {

                        _playGrid = ParseLevel(x.grid);
                        _startPos = new BlockStyles[_playGrid.GetLength(0), _playGrid.GetLength(1)];
                        Array.Copy(_playGrid, _startPos, _playGrid.Length);
                        CreatePlayfield(_playGrid);
                        SwitchGameStage(GameStage.PlayStage);
                    }
                }
            }
        }

        internal BlockStyles[,] ParseLevel(int[][] grid)
        {
            int rows = grid.Length;
            int cols = grid[0].Length;
            BlockStyles[,] levelGrid = new BlockStyles[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    levelGrid[i, j] = (BlockStyles)grid[i][j];
                }
            }

            return levelGrid;
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
                BlockStyles[,] grid = new BlockStyles[10, 10]
               {
                { BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall, BlockStyles.Wall },
            { BlockStyles.Wall, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall },
            { BlockStyles.Wall, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall },
            { BlockStyles.Wall, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall },
            { BlockStyles.Wall, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall, BlockStyles.Target, BlockStyles.Ground, BlockStyles.Box, BlockStyles.Ground, BlockStyles.Wall },
            { BlockStyles.Wall, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall, BlockStyles.Box, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall },
            { BlockStyles.Wall, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Player, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall },
            { BlockStyles.Wall, BlockStyles.Ground, BlockStyles.Box, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Ground, BlockStyles.Wall },
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
        private void SwitchGameStage(GameStage request)
        {
            switch (request)
            {
                case GameStage.PlayStage:
                    _gameStage = GameStage.PlayStage;
                    VictoryGrid.Visibility = Visibility.Hidden;
                    LayoutGrid.Visibility = Visibility.Visible;
                    LevelsGrid.Visibility = Visibility.Hidden;
                    break;
                case GameStage.LevelSelect:
                    _gameStage = GameStage.LevelSelect;
                    VictoryGrid.Visibility = Visibility.Hidden;
                    LayoutGrid.Visibility = Visibility.Hidden;
                    LevelsGrid.Visibility = Visibility.Visible;
                    break;
                case GameStage.Victory:
                    _gameStage = GameStage.Victory;
                    VictoryGrid.Visibility = Visibility.Visible;
                    LayoutGrid.Visibility = Visibility.Hidden;
                    LevelsGrid.Visibility = Visibility.Hidden;
                    break;
            }
        }
        #endregion







    }
}
