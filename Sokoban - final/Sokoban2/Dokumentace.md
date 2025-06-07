Co je tento projekt?

-Projekt je vytvo�en� hry Sokoban, kde c�lem je pomoc� hrac�ho pan��ka dotla�it bedny na pole vyzna�en� jako c�l.
-Bednu lze jen tla�it, nelze p�it�hnout k sob�. Bednu nelze tla�it, kdy� za bednou je dal�� bedna nebo ze�.
-V �rovn�ch lze se pohybovat skrz WASD, lze tak� vr�tit pohyby a tak� zcela za��t od za��tku �rovn�.
-Hra m� 12 level�, kter� jsou vyzna�eny barvy podle obt�nosti. Zelen� je nejjednodu��, �lut� je n�ro�n�, oran�ov� je t�k�, �erven� je nejobt�n�j��
-P�eji p��jemn� hran�


Dokumentace:

App.xaml
	-Obsahuje styly pro bloky

MainWindow.xaml
	-Obsahuje: 
		-LayoutGrid
			- 2 ��sti: 
				PlayingFieldGrid - UI hrac� plochy
				ControlsGrid - tla��tka menu - n�vrat na v�ber level�, undo - krok zp�t, reset - restart levelu
					-hra ovl�d�na p�es WASD

		-Victory Grid
			-Menu p�i v�h�e levelu - tla��tko na v�b�r level�
		
		-Levels Grid
			- Grid s v�berem level� - generov�no p�es k�d s r�zn�mi obt�nostmi dle barvy

LevelData.cs
	-class s properties s daty z parsovan�ho jsonu - (��slo �rovn�, hrac� plocha, obt�nost - barva)

Levels.json
	-levely pro release
	
MainWindow.xaml.cs
	
	
	
	//Grid
	private int arrayXSize_column; 
    private int arrayYSize_row;
		- velikosti pol�

	private BlockStyles[,] _playGrid;
		-enumy pro typy pol�
    private Blocks[,] _blocksGrid;
		-reference na bloky v poli

    //Hr��
    private Blocks _player;
	private int _characterColumn = 2;
    private int _characterRow = 2;
		-reference na blok hr��e + pozice

    //V�hra
	private int _targetBlockCount = 0;
    private int _activeTargetBlocks = 0;
		-aktivn� a celkov� po�et c�lov�ch blok�

    
	//Undo
	private BlockStyles[,] _startPos;
		-startovn� pozice
    private Stack<BlockStyles[,]> _undoArray = new Stack<BlockStyles[,]>();
		
		- _undoArray pro vr�cen� pozice p�i kroku zp�t (Undo)
    

    //Current stage of the game
    private GameStage _gameStage = GameStage.PlayStage;


    //Levels list
    List<LevelData> levels;
		-list s objekty LevelData

	 region Gameplay
		-metody v hrac� f�z� (PlayStage)

			OnKeyDown
				- p��j�m� input v hrac� f�zi - 4 sm�ry - WASD

			Move 
				-logika pro v�echny pohyby

			WinCheck
				- kontrola v�hry

			Reset
				
				- Restart levelu

			Undo_Rollback
				- Vr�cen� kroku

			
		
		region UI
			- pomocn� metody pro vykreslov�n�

			CreatePlayfield
				- Vykreslen� hrac�ho pole

		region Levels
			- Metody spojen� s levely

				LoadLevels
					- vytvo�en� tla��tek level�

				LevelSelectBtn_Click	
					- ClickEvent na v�ech tla��tk�ch level�
					- p�epne na GameStage - hrac� f�ze

				LevelSelection
					- p�i VictoryGrid screen - p�epne na LevelSelect

				ParseLevel
					- parseje grid z LevelData

				SwitchGameStage
					- pomocn� metoda pro zm�nu _gameStage a UI
					- p�ijmut� gamestage koresponduj�c� se sv�m gridem nastav� sv�mu gridu hodnotu visibility= "visible" a ostatn�m visibility= "hidden"
					




	
	

