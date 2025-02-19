
using Microsoft.OpenApi.Extensions;

namespace APIServer.Models;


// redisdb => rdb
public class RdbAuthUserData
{
    public long Uid { get; set; } = 0;
    public string Token { get; set; } = "";
}

public class RdbRoomData
{
    // W(white) / B(black) + K / Q / B / N / R / P

    public byte[,] board { get; set; }


    // move record ref : https://www.chess.com/ko/terms/chess-notation-ko
    public string last_move { get; set; }

}


public class RdbBoard
{
    private readonly byte[,] initBoard;

    /// <summary>
    /// make enums to byte
    /// </summary>
    public RdbBoard()
    {
        byte none = (byte)RdbTeam.None;

        byte black_king = (byte)RdbTeam.Black | (byte)RdbPieces.King;
        byte black_queen = (byte)RdbTeam.Black | (byte)RdbPieces.Queen;
        byte black_bishop = (byte)RdbTeam.Black | (byte)RdbPieces.Bishop;
        byte black_knight = (byte)RdbTeam.Black | (byte)RdbPieces.Knight;
        byte black_rook = (byte)RdbTeam.Black | (byte)RdbPieces.Rook;
        byte black_pawn = (byte)RdbTeam.Black | (byte)RdbPieces.Pawn;

        byte white_king = (byte)RdbTeam.White | (byte)RdbPieces.King;
        byte white_queen = (byte)RdbTeam.White | (byte)RdbPieces.Queen;
        byte white_bishop = (byte)RdbTeam.White | (byte)RdbPieces.Bishop;
        byte white_knight = (byte)RdbTeam.White | (byte)RdbPieces.Knight;
        byte white_rook = (byte)RdbTeam.White | (byte)RdbPieces.Rook;
        byte white_pawn = (byte)RdbTeam.White | (byte)RdbPieces.Pawn;

        initBoard = new byte[,]{
                { black_rook,black_knight,black_bishop,black_queen,black_king,black_bishop,black_knight,black_rook},
                { black_pawn,black_pawn,black_pawn,black_pawn,black_pawn,black_pawn,black_pawn,black_pawn},
                { none,none,none,none,none,none,none,none},
                { none,none,none,none,none,none,none,none},
                { none,none,none,none,none,none,none,none},
                { none,none,none,none,none,none,none,none},
                { white_pawn,white_pawn,white_pawn,white_pawn,white_pawn,white_pawn,white_pawn,white_pawn},
                { white_rook,white_knight,white_bishop,white_queen,white_king,white_bishop,white_knight,white_rook},
            }
        ;
    }


    public byte[,] GetInitBoard()
    {
        return initBoard;
    }
}



public enum RdbTeam : byte
{
    None = Team.None << 4,
    White = Team.White << 4,
    Black = Team.Black << 4,
}

public enum RdbPieces : byte
{
    None = Pieces.None,
    King = Pieces.King,
    Queen = Pieces.Queen,
    Bishop = Pieces.Bishop,
    Knight = Pieces.Knight,
    Rook = Pieces.Rook,
    Pawn = Pieces.Pawn,
}