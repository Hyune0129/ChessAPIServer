using APIServer.Chess.Domain;
using ChessCLI.Chess.Domain;
using ChessCLI.Chess.Domain.Pieces;

namespace ChessCLI.Chess.Game;

public class ChessGamePage
{
    public readonly ChessGameService _gameService;
    public Board _board;
    public Team _myTeam = Team.Black;

    // First Start Game
    public ChessGamePage(ChessGameService chessGameService)
    {
        _board = new Board(new InitBoardMaker().GetInitBoard());
        _gameService = chessGameService;
    }

    // Load Game
    public ChessGamePage(ChessGameService chessGameService, Board board)
    {
        _board = board;
        _gameService = chessGameService;
    }

    public async Task StartGamePage()
    {
        Console.WriteLine("Game Started");
        
        throw new NotImplementedException();
    }

    public async Task ProcessGamePage()
    {

    }


    // white team board print
    public void PrintBoard()
    {
        Console.WriteLine(" Your Piece is Upper(K, R, N, Q, B, P) ");
        Console.WriteLine("   a   b   c   d   e   f   g   h ");
        Console.WriteLine("  +---+---+---+---+---+---+---+---+");
        for (int row = 0; row < 8; row++)
        {
            Console.Write(8 - row + " "); // 왼쪽 숫자 표시
            for (int col = 0; col < 8; col++)
            {
                Console.Write("| " + GetCharByPiece(_board.board[row, col]) + " ");
            }
            Console.Write($"| {8 - row}");
            Console.WriteLine("\n  +---+---+---+---+---+---+---+---+");
        }
        Console.WriteLine("   a   b   c   d   e   f   g   h ");
    }

    // black team board print
    public void RotatePrintBoard()
    {
        Console.WriteLine(" Your Piece is Upper(K, R, N, Q, B, P) ");
        Console.WriteLine("   h   g   f   e   d   c   b   a ");
        Console.WriteLine("  +---+---+---+---+---+---+---+---+");
        for (int row = 7; row >= 0; row--)
        {
            Console.Write(8 - row + " "); // 왼쪽 숫자 표시
            for (int col = 7; col >= 0; col--)
            {
                Console.Write("| " + GetCharByPiece(_board.board[row, col]) + " ");
            }
            Console.Write($"| {8 - row}");
            Console.WriteLine("\n  +---+---+---+---+---+---+---+---+");
        }
        Console.WriteLine("   h   g   f   e   d   c   b   a ");
    }

    public char GetCharByPiece(Piece piece)
    {
        switch (piece.pieceType)
        {
            case PieceType.None:
                return '.';
            case PieceType.King:
                return piece.team == _myTeam ? 'K' : 'k';
            case PieceType.Queen:
                return piece.team == _myTeam ? 'Q' : 'q';
            case PieceType.Rook:
                return piece.team == _myTeam ? 'R' : 'r';
            case PieceType.Bishop:
                return piece.team == _myTeam ? 'B' : 'b';
            case PieceType.Knight:
                return piece.team == _myTeam ? 'N' : 'n';
            case PieceType.Pawn:
                return piece.team == _myTeam ? 'P' : 'p';
            default:
                return '.';
        }
    }
}