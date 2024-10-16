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
    Console.WriteLine("Put your mark, or to move the active zone.");
    Console.WriteLine("To put your mark, type: <x,y> (ex: 1,2)");
    Console.WriteLine("To move the active center, type: move <x,y> (ex: move 1,2)");

    while (true)
    {
        Console.WriteLine(gameInstance.GetNextMoveBy() + "'s turn!");
        Console.WriteLine("Current active zone center is: " + gameInstance.SmallBoardCenterX + "," + gameInstance.SmallBoardCenterY);

        ConsoleUI.Visualizer.DrawBoard(gameInstance);
        var input = Console.ReadLine()!;
        
        // parse input
        if (input.StartsWith("move"))
        {
            var inputSplit = input.Split(' ');
            if (inputSplit[0].ToLower().Equals("move"))
            {
                var coordinates = inputSplit[1].Split(",");
                var inputX = int.Parse(coordinates[0]);
                var inputY = int.Parse(coordinates[1]);
            
                var isBoardMoveSuccessful = gameInstance.MoveSmallBoard(inputX, inputY);
                if (!isBoardMoveSuccessful)
                {
                    Console.WriteLine("Board move unsuccessful! Please try again.");
                }
        
                // checks if a player has won, or board ran out of space - tie
                if (gameInstance.IsGameOver())
                {
                    Console.WriteLine("Thank you for playing!");
            
                    break;
                }
            }
        }
        else
        {
            var inputSplit = input.Split(",");
            var inputX = int.Parse(inputSplit[0]);
            var inputY = int.Parse(inputSplit[1]);
            
            var isMoveSuccessful = gameInstance.MakeAMove(inputX, inputY);
            if (!isMoveSuccessful)
            {
                Console.WriteLine("Move unsuccessful! Please try again.");
            }
        
            // checks if a player has won, or board ran out of space - tie
            if (gameInstance.IsGameOver())
            {
                Console.WriteLine("Thanks for playing!");
            
                break;
            }
        }
    }

    return "Redundant";
}