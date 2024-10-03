using GameBrain;
using MenuSystem;

var gameInstance = new TicTacToeBrain(5);

var optionsMenu = new Menu(EMenuLevel.Secondary, "TIC-TAC-TWO Options", [
        new MenuItem()
        {
            Title = "X Starts",
            Shortcut = "X",
            MenuItemAction = () => gameInstance.SetNextMoveBy(EGamePiece.X)
        },
        new MenuItem()
        {
            Title = "O Starts",
            Shortcut = "O",
            MenuItemAction = () => gameInstance.SetNextMoveBy(EGamePiece.O)
        },
    ]
);

var mainMenu = new Menu(EMenuLevel.Main, "TIC-TAC-TWO", [
        new MenuItem()
        {
            Shortcut = "O",
            Title = "Options",
            MenuItemAction = optionsMenu.Run
        },
        new MenuItem()
        {
            Shortcut = "N",
            Title = "New game",
            MenuItemAction = NewGame
        }
    ]
);

mainMenu.Run();

return;

// ========================================

string NewGame()
{
    while (true)
    {
        ConsoleUI.Visualizer.DrawBoard(gameInstance);
        Console.Write("Give me coordinates <x,y> (ex: 1,2):");
        var input = Console.ReadLine()!;
        
        var inputSplit = input.Split(",");
        var inputX = int.Parse(inputSplit[0]);
        var inputY = int.Parse(inputSplit[1]);
        
        gameInstance.MakeAMove(inputX, inputY);

        // checks if a player has won, or board ran out of space - tie
        if (gameInstance.IsGameOver(inputX, inputY))
        {
            Console.WriteLine("Thanks for playing!");
            
            break;
        }
    }

    return "Redundant";
}