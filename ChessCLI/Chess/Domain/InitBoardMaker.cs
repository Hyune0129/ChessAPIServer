namespace ChessCLI.Chess.Domain;

public class InitBoardMaker
{
    private readonly byte[] initBoard;

    /// <summary>
    /// make enums to byte
    /// </summary>
    public InitBoardMaker()
    {
        byte none = (byte)Team.None;

        byte black_king = (byte)TeamPieceType.BlackKing;
        byte black_queen = (byte)TeamPieceType.BlackQueen;
        byte black_rook = (byte)TeamPieceType.BlackRook;
        byte black_bishop = (byte)TeamPieceType.BlackBishop;
        byte black_knight = (byte)TeamPieceType.BlackKnight;
        byte black_pawn = (byte)TeamPieceType.BlackPawn;

        byte white_king = (byte)TeamPieceType.WhiteKing;
        byte white_queen = (byte)TeamPieceType.WhiteQueen;
        byte white_rook = (byte)TeamPieceType.WhiteRook;
        byte white_bishop = (byte)TeamPieceType.WhiteBishop;
        byte white_knight = (byte)TeamPieceType.WhiteKnight;
        byte white_pawn = (byte)TeamPieceType.WhitePawn;

        initBoard = new byte[64]{
                 black_rook,black_knight,black_bishop,black_queen,black_king,black_bishop,black_knight,black_rook, // 8
                 black_pawn,black_pawn,black_pawn,black_pawn,black_pawn,black_pawn,black_pawn,black_pawn, // 7
                 none,none,none,none,none,none,none,none, // 6
                 none,none,none,none,none,none,none,none, // 5
                 none,none,none,none,none,none,none,none, // 4
                 none,none,none,none,none,none,none,none, // 3
                 white_pawn,white_pawn,white_pawn,white_pawn,white_pawn,white_pawn,white_pawn,white_pawn, // 2
                 white_rook,white_knight,white_bishop,white_queen,white_king,white_bishop,white_knight,white_rook, // 1
            }
        ;
    }


    public byte[] GetInitBoard()
    {
        return (byte[])initBoard.Clone();
    }
}
