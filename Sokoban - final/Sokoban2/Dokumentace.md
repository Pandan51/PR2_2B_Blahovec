Co je tento projekt?

-Projekt je vytvoøení hry Sokoban, kde cílem je pomocí hracího panáèka dotlaèit bedny na pole vyznaèená jako cíl.
-Bednu lze jen tlaèit, nelze pøitáhnout k sobì. Bednu nelze tlaèit, když za bednou je další bedna nebo zeï.
-V úrovních lze se pohybovat skrz WASD, lze také vrátit pohyby a také zcela zaèít od zaèátku úrovnì.
-Hra má 12 levelù, které jsou vyznaèeny barvy podle obtížnosti. Zelená je nejjednoduší, žlutá je nároèná, oranžová je tìžká, èervená je nejobtížnìjší
-Pøeji pøíjemné hraní


Dokumentace:

App.xaml
	-Obsahuje styly pro bloky

MainWindow.xaml
	-Obsahuje: 
		-LayoutGrid
			- 2 èásti: 
				PlayingFieldGrid - UI hrací plochy
				ControlsGrid - tlaèítka menu - návrat na výber levelù, undo - krok zpìt, reset - restart levelu
					-hra ovládána pøes WASD

		-Victory Grid
			-Menu pøi výhøe levelu - tlaèítko na výbìr levelù
		
		-Levels Grid
			- Grid s výberem levelù - generováno pøes kód s rùznými obtížnostmi dle barvy

LevelData.cs
	-class s properties s daty z parsovaného jsonu - (èíslo úrovnì, hrací plocha, obtížnost - barva)

Levels.json
	-levely pro release
	
MainWindow.xaml.cs
	
	
	
	//Grid
	private int arrayXSize_column; 
    private int arrayYSize_row;
		- velikosti polí

	private BlockStyles[,] _playGrid;
		-enumy pro typy polí
    private Blocks[,] _blocksGrid;
		-reference na bloky v poli

    //Hráè
    private Blocks _player;
	private int _characterColumn = 2;
    private int _characterRow = 2;
		-reference na blok hráèe + pozice

    //Výhra
	private int _targetBlockCount = 0;
    private int _activeTargetBlocks = 0;
		-aktivní a celkový poèet cílových blokù

    
	//Undo
	private BlockStyles[,] _startPos;
		-startovní pozice
    private Stack<BlockStyles[,]> _undoArray = new Stack<BlockStyles[,]>();
		
		- _undoArray pro vrácení pozice pøi kroku zpìt (Undo)
    

    //Current stage of the game
    private GameStage _gameStage = GameStage.PlayStage;


    //Levels list
    List<LevelData> levels;
		-list s objekty LevelData

	 region Gameplay
		-metody v hrací fází (PlayStage)

			OnKeyDown
				- pøíjímá input v hrací fázi - 4 smìry - WASD

			Move 
				-logika pro všechny pohyby

			WinCheck
				- kontrola výhry

			Reset
				
				- Restart levelu

			Undo_Rollback
				- Vrácení kroku

			
		
		region UI
			- pomocné metody pro vykreslování

			CreatePlayfield
				- Vykreslení hracího pole

		region Levels
			- Metody spojené s levely

				LoadLevels
					- vytvoøení tlaèítek levelù

				LevelSelectBtn_Click	
					- ClickEvent na všech tlaèítkách levelù
					- pøepne na GameStage - hrací fáze

				LevelSelection
					- pøi VictoryGrid screen - pøepne na LevelSelect

				ParseLevel
					- parseje grid z LevelData

				SwitchGameStage
					- pomocná metoda pro zmìnu _gameStage a UI
					- pøijmutá gamestage korespondující se svým gridem nastaví svému gridu hodnotu visibility= "visible" a ostatním visibility= "hidden"
					




	
	

