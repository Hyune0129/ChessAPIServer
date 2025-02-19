namespace APIServer.Models;

// GameDb와 Redis에서 둘 다 사용되는 case
public enum Team
{
    None = 0,
    White = 1,
    Black = 2
}

public enum Pieces
{
    None = 0,
    King = 1,
    Queen = 2,
    Bishop = 3,
    Knight = 4,
    Rook = 5,
    Pawn = 6,
}

public enum ChessNotations
{
    King = 'K',
    Queen = 'Q',
    Bishop = 'B',
    Knight = 'N',
    Rook = 'R',
    Pawn = 'P'
}