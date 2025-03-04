namespace APIServer.Domain;
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
    WhiteKing = Team.White | PieceType.King,
    WhiteQueen = Team.White | PieceType.Queen,
    WhiteBishop = Team.White | PieceType.Bishop,
    WhiteKnight = Team.White | PieceType.Knight,
    WhiteRook = Team.White | PieceType.Rook,
    WhitePawn = Team.White | PieceType.Pawn,

    BlackKing = Team.Black | PieceType.King,
    BlackQueen = Team.Black | PieceType.Queen,
    BlackBishop = Team.Black | PieceType.Bishop,
    BlackKnight = Team.Black | PieceType.Knight,
    BlackRook = Team.Black | PieceType.Rook,
    BlackPawn = Team.Black | PieceType.Pawn,
}