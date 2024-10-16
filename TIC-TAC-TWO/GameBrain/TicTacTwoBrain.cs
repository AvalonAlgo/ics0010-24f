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
        
        if (Math.Abs(dx) <= 1 && Math.Abs(dy) <= 1)
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
    public bool IsGameOver()
    {
        // Func<string> printMessage = () =>
        // {
        //     Console.WriteLine(player + " won!"); 
        //     return player + " won!";
        // };
        

        return false;
    }
}