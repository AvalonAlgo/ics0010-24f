namespace GameBrain;

public class TicTacToeBrain
{
    private EGamePiece[,] _gameBoard;
    private EGamePiece _nextMoveBy = EGamePiece.X;
    private int _smallBoardCenterX;
    private int _smallBoardCenterY;
    
    public TicTacToeBrain(int boardSize) : this(boardSize, boardSize)
    {
    }
    private TicTacToeBrain(int boardX, int boardY)
    {
        _gameBoard = new EGamePiece[boardX, boardY];
        _smallBoardCenterX = boardX / 2;
        _smallBoardCenterY = boardY / 2;
    }
    
    public EGamePiece[,] GameBoard
    {
        get { return _gameBoard; }
        set { _gameBoard = value; }
    }
    public int DimX => _gameBoard.GetLength(0);
    public int DimY => _gameBoard.GetLength(1);
    public int SmallBoardCenterX => _smallBoardCenterX;
    public int SmallBoardCenterY => _smallBoardCenterY;
    
    public string GetNextMoveBy()
    {
        return _nextMoveBy.ToString();
    }
    public string SetNextMoveBy(EGamePiece gamePiece)
    {
        _nextMoveBy = gamePiece;
        Console.WriteLine("Starting move changed to " + gamePiece + "!");
        
        return "Starting move changed to " + gamePiece + "!";
    }
    public bool MakeAMove(int x, int y)
    {
        if (_gameBoard[x, y] == EGamePiece.Empty)
        {
            _gameBoard[x, y] = _nextMoveBy;
            _nextMoveBy = _nextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;

            return true;
        }

        return false;
    }
    public bool MoveSmallBoard(int newX, int newY)
    {
        int dx = newX - _smallBoardCenterX;
        int dy = newY - _smallBoardCenterY;
        
        if (Math.Abs(dx) <= 1 && Math.Abs(dy) <= 1 && newX > 0 && newY > 0)
        {
            _smallBoardCenterX = newX;
            _smallBoardCenterY = newY;
            
            _nextMoveBy = _nextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
            Console.WriteLine("Small board center moved! New center is: " + _smallBoardCenterX + "," + _smallBoardCenterY);
            
            return true;
        }
        
        return false;
    }
    private bool CheckLine(int row, int col, int rowIncrement, int colIncrement, EGamePiece player)
    {
        int count = 0; // Start with 0, we'll increment within the loop

        for (int i = 0; i < 3; i++) 
        {
            int r = row + i * rowIncrement;
            int c = col + i * colIncrement;

            // Ensure the cell is within the small 3x3 board around the center
            if (_gameBoard[r, c] == player)
            {
                count++;
            }
        }
        
        return count == 3;
    }
    public bool IsGameOver()
    {
        EGamePiece player = _nextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
        
        // Check rows
        for (int row = _smallBoardCenterY - 1; row <= _smallBoardCenterY + 1; row++)
        {
            if (CheckLine(row, _smallBoardCenterX - 1, 0, 1, player)) 
            {
                Console.WriteLine(player + " won!");
                return true;
            }
        }

        // Check first column
        if (CheckLine(_smallBoardCenterY - 1, _smallBoardCenterX - 1, 1, 0, player))
        {
            Console.WriteLine(player + " won!");
            return true;
        }

        // Check diagonals
        if (CheckLine(_smallBoardCenterY - 1, _smallBoardCenterX - 1, 1, 1, player) ||
            CheckLine(_smallBoardCenterY - 1, _smallBoardCenterX + 1, 1, -1, player))
        {
            Console.WriteLine(player + " won!");
            return true;
        }

        return false; 
    }
}