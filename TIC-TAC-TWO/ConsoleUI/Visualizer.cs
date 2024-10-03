﻿using GameBrain;

namespace ConsoleUI;

public static class Visualizer
{
    public static void DrawBoard(TicTacToeBrain gameInstance)
    {
        // Print column headers
        Console.Write("  "); // Initial spacing
        for (var x = 0; x < gameInstance.DimX; x++)
        {
            Console.Write("  " + (x) + " "); 
        }
        Console.WriteLine();

        for (var y = 0; y < gameInstance.DimY; y++)
        {
            Console.Write(y + " "); // Print row number

            for (var x = 0; x < gameInstance.DimX; x++)
            {
                Console.Write(" " + DrawGamePiece(gameInstance.GameBoard[x, y]) + " ");
                if (x == gameInstance.DimX - 1) continue;
                Console.Write("|");
            }

            Console.WriteLine();
            if (y == gameInstance.DimY - 1) continue;
            Console.Write("  "); // Initial spacing for separator line
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