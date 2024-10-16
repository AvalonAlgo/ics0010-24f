using GameBrain;

namespace ConsoleUI;

public static class Visualizer
{
    public static void DrawBoard(TicTacToeBrain gameInstance)
    {
        // Print column headers
        Console.Write("  ");
        for (var x = 0; x < gameInstance.DimX; x++)
        {
            Console.Write(" " + x + "  "); 
        }
        Console.WriteLine();

        for (var y = 0; y < gameInstance.DimY; y++)
        {
            // Print row number
            Console.Write(y + " ");

            for (var x = 0; x < gameInstance.DimX; x++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                
                if (x == gameInstance.SmallBoardCenterX && y == gameInstance.SmallBoardCenterY)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                
                Console.Write(" " + DrawGamePiece(gameInstance.GameBoard[x, y]) + " ");
                if (x == gameInstance.DimX - 1) continue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("|");
            }

            Console.WriteLine();
            if (y == gameInstance.DimY - 1) continue;
            Console.Write("  ");
            for (var x = 0; x < gameInstance.DimX; x++)
            {
                Console.Write("---");
                if (x != gameInstance.DimX - 1)
                {
                    Console.Write("+");
                }
            }

            Console.WriteLine();
        }
    }
    
    private static string DrawGamePiece(EGamePiece piece) =>
        piece switch
        {
            EGamePiece.O => "O",
            EGamePiece.X => "X",
            _ => " "
        };
}