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
    internal enum Direction { Up, Left, Down, Right}

    

    internal enum GameStage {Victory, PlayStage, LevelSelect}
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
        //private BlockStyles[,] _startLayout;
        private BlockStyles[,] _playGrid;
        private Blocks[,] _blocksGrid;

        //Sloupec (_characterX)

        private int _characterColumn = 2;
        private int _characterRow = 2;
        private GameStage _gameStage = GameStage.PlayStage;

        private int _targetBlockCount = 0;
        private int _activeTargetBlocks = 0;

        private Stack<BlockStyles[,]> _undoArray = new Stack<BlockStyles[,]>();
        //Levels list
        List<LevelData> levels; /*= JsonSerializer.Deserialize<List<LevelData>>(File.ReadAllText("Levels.json"));*/


        public MainWindow()
        {
            InitializeComponent();
            LoadLevels();
            _playGrid = ParseLevel(levels[0].grid);
            //_startLayout = _playGrid;
            CreatePlayfield(_playGrid);


        }


        private void Start()
        {


            ////sloupec
            //arrayXSize = _playGrid.GetLength(1);
            ////řádek
            //arrayYSize = _playGrid.GetLength(0);

            ////Reference na bloky
            //_blocksGrid = new Blocks[arrayYSize, arrayXSize];
            ////promažu, kdyby tam něco bylo

            //PlayingFieldGrid.ColumnDefinitions.Clear();
            //PlayingFieldGrid.RowDefinitions.Clear();

            //var cellSize = new System.Windows.GridLength(50);

            ////připravím řádky, sloupce
            ////Zatím stále,
            ////TODO později implementovat velikost
            //for (int i = 0; i < arrayXSize; i++)
            //{
            //    var colDef = new ColumnDefinition();
            //    colDef.Width = cellSize;



            //    PlayingFieldGrid.ColumnDefinitions.Add(colDef);
            //}

            //for (int i = 0; i < arrayYSize; i++)
            //{
            //    var rowDef = new RowDefinition();
            //    rowDef.Height = cellSize;
            //    PlayingFieldGrid.RowDefinitions.Add(rowDef);
            //}

            //for (int i = 0; i < arrayYSize; i++)
            //{
            //    for (int j = 0; j < arrayXSize; j++)
            //    {
            //        //if (i != arrayXSize && j != arrayYSize)
            //        //{
            //        Blocks block = new Blocks();
            //        _blocksGrid[j, i] = block;

            //        block.Style = (Style)FindResource(GetStyles(_playGrid[i, j]));
            //        if (_playGrid[i, j] == BlockStyles.Target)
            //        {
            //            _targetBlockCount++;
            //        }
            //        else if (_playGrid[i, j] == BlockStyles.BoxTarget)
            //        {
            //            _targetBlockCount++;
            //            _activeTargetBlocks++;
            //        }
            //        Grid.SetRow(block, i);
            //        Grid.SetColumn(block, j);
            //        //Test
            //        if (_playGrid[i, j] == BlockStyles.Player)
            //        {
            //            _player = block;

            //        }
            //        PlayingFieldGrid.Children.Add(block);
            //        //}
            //    }
            //}
            

            


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

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.Key)
            {
                case Key.W: // Up
                   if (_characterRow > 0)
                    {
                        Move(Direction.Up, 0,-1);
                    }
                    break;

                case Key.S: // Down
                    if (_characterRow < arrayYSize - 1)
                    {
                        Move(Direction.Down, 0, 1);
                    }
                    break;
                case Key.A: // Left
                    if (_characterColumn > 0)
                    {
                        Move(Direction.Left, -1, 0);
                    }
                    break;
                case Key.D: // Right
                    if (_characterColumn < arrayXSize - 1)
                    {
                        Move(Direction.Right, 1, 0);
                    }
                    break;
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

        private void Move(Direction direction, int columnDir, int rowDir)
        {


            //Bloky ve směru
            //nextBlock je vedle hráče
            Blocks nextBlock = _blocksGrid[_characterColumn + columnDir, _characterRow + (rowDir)];
            Blocks afterBlock = new();
            //afterBlock je za nextBlock
            if(nextBlock.Style != (Style)FindResource(GetStyles(BlockStyles.Wall)))
                 afterBlock = _blocksGrid[_characterColumn + (2 * columnDir), _characterRow + (2 * rowDir)];

            
            




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
                    PPos = new int [2] { Grid.GetRow(_player), Grid.GetColumn(_player) };
                    NBPos = new int [2] { Grid.GetRow(nextBlock), Grid.GetColumn(nextBlock) };

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
                    int[] ABPos = new int[2] {Grid.GetRow(afterBlock), Grid.GetColumn(afterBlock) };

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
                    UndoSave(_playGrid);
                    PPos = new int[2] { Grid.GetRow(_player), Grid.GetColumn(_player) };
                    NBPos = new int[2] { Grid.GetRow(nextBlock), Grid.GetColumn(nextBlock) };
                    ABPos = new int[2] { Grid.GetRow(afterBlock), Grid.GetColumn(afterBlock) };

                    if (_playGrid[ABPos[0], ABPos[1]] == BlockStyles.Wall || _playGrid[ABPos[0], ABPos[1]] == BlockStyles.Box)
                    {
                        break;
                    }

                    if (_playGrid[ABPos[0], ABPos[1]] == BlockStyles.Ground)
                    {
                        _playGrid[ABPos[0], ABPos[1]] = BlockStyles.Box;
                        AutoStyle(afterBlock, BlockStyles.Box);
                    }
                    else
                    {
                        _playGrid[ABPos[0], ABPos[1]] = BlockStyles.BoxTarget;
                        AutoStyle(afterBlock, BlockStyles.BoxTarget);
                    }
                    _activeTargetBlocks--;

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


        private void AutoStyle(Blocks blockTarget, BlockStyles blockTarget_style)
        {
            blockTarget.Style = (Style)FindResource(GetStyles(blockTarget_style));
        }



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

        private void SwitchGameStage(GameStage request)
        {
            switch(request)
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

        private void WinCheck()
        {
            if(_targetBlockCount == _activeTargetBlocks)
            {
                _gameStage = GameStage.Victory;
                VictoryGrid.Visibility = Visibility.Visible;
                LayoutGrid.Visibility = Visibility.Hidden;
            }
        }

        private void Reset(object sender, RoutedEventArgs e)
        {

            _playGrid = ParseLevel(levels[0].grid);
            CreatePlayfield(_playGrid);

        }

        private void UndoSave(BlockStyles[,] newSave)
        {
            BlockStyles[,] tempPush = new BlockStyles[newSave.GetLength(0), newSave.GetLength(1)];
            Array.Copy(newSave, tempPush, newSave.Length);
            _undoArray.Push(tempPush);
        }

        //private void UndoRollback()
        //{
        //    if (_undoArray.Count > 0)
        //    {
                
        //        _playGrid = _undoArray.Pop();
        //        CreatePlayfield(_playGrid);
        //    }
            
        //}

        //TODO: Create grid for undo implementation
        private void CreatePlayfield(BlockStyles[,] sentPlayfield)
        {
            int targetCount = 0;
            int activeTarget = 0;
            //sloupec
            arrayXSize = _playGrid.GetLength(0);
            //řádek
            arrayYSize = _playGrid.GetLength(1);

            //Reference na bloky
            _blocksGrid = new Blocks[arrayYSize, arrayXSize];
            //promažu, kdyby tam něco bylo

            PlayingFieldGrid.ColumnDefinitions.Clear();
            PlayingFieldGrid.RowDefinitions.Clear();

            var cellSize = new System.Windows.GridLength(50);

            //připravím řádky, sloupce
            //Zatím stále,
            //TODO později implementovat velikost
            for (int i = 0; i < arrayYSize; i++)
            {
                var colDef = new ColumnDefinition();
                colDef.Width = cellSize;



                PlayingFieldGrid.ColumnDefinitions.Add(colDef);
            }

            for (int i = 0; i < arrayXSize; i++)
            {
                var rowDef = new RowDefinition();
                rowDef.Height = cellSize;
                PlayingFieldGrid.RowDefinitions.Add(rowDef);
            }

            for (int i = 0; i < arrayXSize; i++)
            {
                for (int j = 0; j < arrayYSize; j++)
                {
                    //if (i != arrayXSize && j != arrayYSize)
                    //{
                    Blocks block = new Blocks();
                    _blocksGrid[j, i] = block;

                    block.Style = (Style)FindResource(GetStyles(sentPlayfield[i, j]));
                    if(sentPlayfield[i, j]==BlockStyles.Player)
                    {
                        _characterColumn = j;
                        _characterRow = i;
                    }

                    if (sentPlayfield[i, j] == BlockStyles.Target || sentPlayfield[i, j] == BlockStyles.BoxTarget)
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


                





                //foreach(LevelData x in levels)
                //{
                //    MessageBox.Show($"You clicked: {x.LevelNumber}");
                //}

                int totalLevels = levels.Count; // Your total levels
                int columns = (int)Math.Ceiling(Math.Sqrt(totalLevels)); // Columns ≈ √N (rounded up)
                int rows = (int)Math.Ceiling((double)totalLevels / columns); // Rows = Total / Columns (rounded up)

                LevelsGrid.Children.Clear();
                LevelsGrid.ColumnDefinitions.Clear();
                LevelsGrid.RowDefinitions.Clear();

                for (int i = 0; i<columns;i++)
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


                    Button btn = new Button()
                    {
                        Tag = levels[count].level
                    };
                    btn.Content = $"{col + (columns * (row - 1))}";
                    Grid.SetColumn(btn, col - 1);
                    Grid.SetRow(btn, row - 1);
                    

                    LevelsGrid.Children.Add(btn);
                    btn.Click += new RoutedEventHandler(LevelBtn_Click);
                    
                    LevelsGrid.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(LevelBtn_Click));
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
                throw new Exception();
            }
        }

        //internal void LoadLevels()
        //{
        //    try
        //    {
        //        // 1. Get correct path
        //        string projectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        //        string jsonPath = Path.Combine(projectRoot, "Levels.json");

        //        // 2. Read and deserialize JSON properly
        //        string jsonText = File.ReadAllText(jsonPath);
        //        levels = JsonSerializer.Deserialize<List<LevelData>>(jsonText);

        //        if (levels == null || levels.Count == 0)
        //        {
        //            MessageBox.Show("No levels found in JSON file");
        //            return;
        //        }

        //        // 3. Calculate grid layout
        //        int totalLevels = levels.Count;
        //        int columns = (int)Math.Ceiling(Math.Sqrt(totalLevels));
        //        int rows = (int)Math.Ceiling((double)totalLevels / columns);

        //        // 4. Clear previous grid
        //        LevelsGrid.Children.Clear();
        //        LevelsGrid.ColumnDefinitions.Clear();
        //        LevelsGrid.RowDefinitions.Clear();

        //        // 5. Create columns and rows
        //        for (int i = 0; i < columns; i++)
        //            LevelsGrid.ColumnDefinitions.Add(new ColumnDefinition());
        //        for (int i = 0; i < rows; i++)
        //            LevelsGrid.RowDefinitions.Add(new RowDefinition());

        //        // 6. Create buttons for each level
        //        for (int index = 0; index < totalLevels; index++)
        //        {
        //            int row = index / columns;
        //            int col = index % columns;

        //            var btn = new Button()
        //            {
        //                Content = $"Level {levels[index].LevelNumber}",
        //                Tag = levels[index], // Store the entire LevelData object
        //                Margin = new Thickness(5)
        //            };

        //            Grid.SetRow(btn, row);
        //            Grid.SetColumn(btn, col);
        //            btn.Click += LevelBtn_Click; // Single handler
        //            LevelsGrid.Children.Add(btn);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error loading levels: {ex.Message}");
        //        // Consider creating default levels here if loading fails
        //    }
        //}

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

        private void Undo_Rollback(object sender, RoutedEventArgs e)
        {
            if (_undoArray.Count > 0)
            {

                _playGrid = _undoArray.Pop();
                CreatePlayfield(_playGrid);
            }
        }

        private void LevelSelection(object sender, RoutedEventArgs e)
        {
            _gameStage = GameStage.LevelSelect;
            VictoryGrid.Visibility = Visibility.Hidden;
            LayoutGrid.Visibility = Visibility.Hidden;
            LevelsGrid.Visibility = Visibility.Visible;
        }

        private void LevelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                int buttonText = int.Parse(button.Content.ToString());
                //MessageBox.Show($"You clicked: {buttonText}");

                foreach(LevelData x in levels)
                {
                    if(x.level == buttonText)
                    {
                        _playGrid = ParseLevel(x.grid);
                        CreatePlayfield(_playGrid);
                        SwitchGameStage(GameStage.PlayStage);
                    }
                }
            }
        }
    }
}
