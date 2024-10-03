namespace GameBrain;

public class TicTacToeBrain
{
    private EGamePiece[,] _gameBoard;
    private EGamePiece _nextMoveBy { get; set; } = EGamePiece.X;

    public TicTacToeBrain() : this(3)
    {
    }
    public TicTacToeBrain(int boardSize) : this(boardSize, boardSize)
    {
    }

    private TicTacToeBrain(int boardX, int boardY)
    {
        _gameBoard = new EGamePiece[boardX, boardY];
    }


    public EGamePiece[,] GameBoard
    {
        get => GetBoard();
        private set => _gameBoard = value;
    }

    public string SetNextMoveBy(EGamePiece gamePiece)
    {
        _nextMoveBy = gamePiece;
        Console.WriteLine("Starting move changed to " + gamePiece + "!");
        
        return "Starting move changed to " + gamePiece + "!";
    }
    public int DimX => _gameBoard.GetLength(0);
    public int DimY => _gameBoard.GetLength(1);
    
    private EGamePiece[,] GetBoard()
    {
        var copyOfBoard = new EGamePiece[_gameBoard.GetLength(0), _gameBoard.GetLength(1)];
        for (var x = 0; x < _gameBoard.GetLength(0); x++)
        {
            for (var y = 0; y < _gameBoard.GetLength(1); y++)
            {
                copyOfBoard[x, y] = _gameBoard[x, y];
            }
        }

        return copyOfBoard;
    }
    
    public bool MakeAMove(int x, int y)
    {
        if (_gameBoard[x, y] != EGamePiece.Empty)
        {
            return false;
        }

        _gameBoard[x, y] = _nextMoveBy;
        
        // flip the next piece
        _nextMoveBy = _nextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;

        return true;
    }

    private bool CheckLine(int row, int col, int rowIncrement, int colIncrement, EGamePiece player)
    {
        int count = 1; // Start with 1 to include the current position

        // Check in one direction
        for (int i = 1; i < 3; i++) 
        {
            int r = row + i * rowIncrement;
            int c = col + i * colIncrement;
            if (r >= 0 && r < _gameBoard.GetLength(0) && c >= 0 && c < _gameBoard.GetLength(1) && _gameBoard[r, c] == player)
            {
                count++;
            }
            else 
            {
                break; 
            }
        }

        // Check in the opposite direction
        for (int i = 1; i < 3; i++)
        {
            int r = row - i * rowIncrement;
            int c = col - i * colIncrement;
            if (r >= 0 && r < _gameBoard.GetLength(0) && c >= 0 && c < _gameBoard.GetLength(1) && _gameBoard[r, c] == player)
            {
                count++;
            }
            else
            {
                break;
            }
        }

        return count >= 3;
    }
    
    public bool IsGameOver(int lastMoveX, int lastMoveY)
    {
        EGamePiece player = _gameBoard[lastMoveX, lastMoveY]; // Get the player who made the last move
        Func<string> printMessage = () =>
        {
            Console.WriteLine(player + " won!"); 
            return player + " won!";
        };
        
        // Check row
        if (CheckLine(lastMoveX, lastMoveY, 0, 1, player))
        {
            printMessage();
            return true;
        }
        
        // Check column
        if (CheckLine(lastMoveX, lastMoveY, 1, 0, player))
        {
            printMessage();
            return true;
        }

        // Check diagonals
        if (CheckLine(lastMoveX, lastMoveY, 1, 1, player))
        {
            printMessage();
            return true;
        }
        if (CheckLine(lastMoveX, lastMoveY, 1, -1, player))
        {
            printMessage();
            return true;
        }
        
        // Check for a draw (if no winner and _gameBoard is full)
        for (int row = 0; row < this.DimX; row++) {
            for (int col = 0; col < this.DimY; col++) {
                if (_gameBoard[row, col] == EGamePiece.Empty) { 
                    return false; // Game not over
                }
            }
        }

        return false;
    }

    public void ResetGame()
    {
        _gameBoard = new EGamePiece[_gameBoard.GetLength(0), _gameBoard.GetLength(1)];
        _nextMoveBy = EGamePiece.X;
    }
}