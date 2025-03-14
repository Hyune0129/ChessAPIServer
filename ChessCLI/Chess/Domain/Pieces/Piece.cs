namespace ChessCLI.Chess.Domain.Pieces;


public abstract class Piece
{
    public PieceType pieceType { get; set; }
    public Team team { get; set; } // White, Black
    public int positionRow { get; set; } // 0~7
    public int positionCol { get; set; } // 0~7

    public Piece(int positionRow, int positionCol, Team team)
    {
        this.positionRow = positionRow;
        this.positionCol = positionCol;
        this.team = team;
    }

    public static Piece GetPieceByPieceType(PieceType pieceType, int positionRow, int positionCol, Team team)
    {
        switch (pieceType)
        {
            case PieceType.Pawn:
                return new Pawn(positionRow, positionCol, team);
            case PieceType.Rook:
                return new Rook(positionRow, positionCol, team);
            case PieceType.Knight:
                return new Knight(positionRow, positionCol, team);
            case PieceType.Bishop:
                return new Bishop(positionRow, positionCol, team);
            case PieceType.Queen:
                return new Queen(positionRow, positionCol, team);
            case PieceType.King:
                return new King(positionRow, positionCol, team);
            case PieceType.None:
                return new BlankPiece(positionRow, positionCol, team);
            default:
                return null;
        }
    }

    /// <summary>
    /// check vaild move and not over boundary
    /// </summary>
    /// <param name="pos"> 0~63</param>
    /// 
    public abstract bool MoveCheck(int row, int col);
}

public class BlankPiece : Piece
{
    public BlankPiece(int positionRow, int positionCol, Team team) : base(positionRow, positionCol, team)
    {
        team = Team.None;
        pieceType = PieceType.None;
    }

    public override bool MoveCheck(int row, int col)
    {
        return false;
    }
}