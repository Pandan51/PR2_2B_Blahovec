Main
-Vytvoření kořene stromu pomocí json souboru
-deklarace proměnných

-obsahuje loop celého programu
	-podle displayMode rozhoduje, jestli je prohlížeč(true), nebo seznam(false)
	-na začátku obou loopu používají metodu displayBrowser, nebo displayList, která vypíše 	tlačítka a další prvky dle displayMode
	-v loopu se volají různe metody z tříd Display, Find a FileManagement

	MarkSalesman
	-přidává objekty Salesman do listu označených (marked)

	MakeTree
	-testovací metoda, která vytvoří strom


Display
-statická třída
	DisplayBrowser
	-vykreslí menu a další prvky prohlížeče
	
	DisplayList
	-vykreslí menu a další prvky seznamu

	HighlightedColouring
	-mění barvu textu a pozadí

	DisplaySalesmanTree
	-vypíše všechny prvky ze stromu

	ForegroundColor
	-mění barvu textu
	-vytvořeno pro jednoduchost psaní kódu

	DisplayPopUpMenu
	-volá se v DisplayBrowser
	-vykresluje tlačítko odstranit, nebo přidat, zda je současný obchodník přidán na seznam

Find
-statická třída
	FindMarkedInList
	-používá metoda DisplayPopUpMenu ve třídě Display k prohledání seznamu označených

	FindUpperSalesman
	-vyhledá nadřazeného současně vybraného obchodníka

	GetTotalSalesRecursive
	-používáse se v DisplayBrowser ve třídě Display pro vypsání součet prodeje ze sítě

FileManagement

	currentListName
	- obsahuje současný název seznamu

	isListLoaded
	-bool, zda je list načten
	-v případě false nejde přidávat na seznam

	changed
	- bool, který v případě, kdy seznam není uložen bude použit k zavolání metody 	

	ListNotLoadedWarning
	-Říká uživateli, že list není načten

	ListNotSavedWarning
	-pokud je boolean changed true a chceme ukončit program, tak se zavolá

	ListIsLoaded a ListNotLoaded
	- upravují isListLoaded

Salesman
- každý objekt má
	-ID
	-Name(jméno)
	-Surname(příjmení)
	-Sales(prodeje)
	-Subordinates(podřízení)
		-List objektů třídy Salesman

	AddSubordinate
	-přidá podřazeného
	
	DeserializeTree
	-načte informace z JSON souboru

	SaveList
	-ukládá .txt soubor s ID každého zaměstnance z seznamu označených
	
	LoadList
	-načítá z .txt souboru ID
	-volá metodu FindIDTraverseTree

	FindIDTraverseTree
	-prohledá celý strom a porovnává ID z textového souboru posláno z metody LoadList
	-vrací list zaměstnanců

	CreateList
	-vytvoří soubor .txt s název muj_vyber.txt, zda není zadán uživatelem
	a varuje, zda seznam se zadaným jménem existuje
	
	NameFile
	-slouží k pojmenování souborů
	-volá se v metodách LoadList, CreateList

	SalesmanData
	-slouží jako podpůrná třída k načítání z JSON souboru



