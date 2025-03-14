namespace ChessCLI.Chess.Domain.Pieces;
public class Rook : Piece
{

    public Rook(int positionRow, int positionCol, Team team) : base(positionRow, positionCol, team)
    {
        pieceType = PieceType.Rook;
    }

    public override bool MoveCheck(int row, int col)
    {
        if (row < 0 || row > 7 || col < 0 || col > 7)
        {
            return false;
        }

        // same pos
        if (positionRow == row && positionCol == col)
        {
            return false;
        }

        // move straight
        if (positionRow == row || positionCol == col)
        {
            return true;
        }
        return false;
    }
}