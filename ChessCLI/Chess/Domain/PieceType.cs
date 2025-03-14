namespace ChessCLI.Chess.Domain;
public enum PieceType
{
    None = 0,
    King = 1,
    Queen = 2,
    Bishop = 3,
    Knight = 4,
    Rook = 5,
    Pawn = 6,
}

public enum TeamPieceType : byte
{
    None = 0,
    WhiteKing = (Team.White << 4) | PieceType.King,
    WhiteQueen = (Team.White << 4) | PieceType.Queen,
    WhiteBishop = (Team.White << 4) | PieceType.Bishop,
    WhiteKnight = (Team.White << 4) | PieceType.Knight,
    WhiteRook = (Team.White << 4) | PieceType.Rook,
    WhitePawn = (Team.White << 4) | PieceType.Pawn,

    BlackKing = (Team.Black << 4) | PieceType.King,
    BlackQueen = (Team.Black << 4) | PieceType.Queen,
    BlackBishop = (Team.Black << 4) | PieceType.Bishop,
    BlackKnight = (Team.Black << 4) | PieceType.Knight,
    BlackRook = (Team.Black << 4) | PieceType.Rook,
    BlackPawn = (Team.Black << 4) | PieceType.Pawn,
}