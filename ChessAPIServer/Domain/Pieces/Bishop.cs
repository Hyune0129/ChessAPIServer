namespace APIServer.Domain.Pieces;
public class Bishop : Piece
{
    public Bishop(int positionRow, int positionCol, Team team) : base(positionRow, positionCol, team)
    {
        pieceType = PieceType.Bishop;
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

        if (Math.Abs(positionRow - row) == Math.Abs(positionCol - col))
        {
            return true;
        }
        return false;
    }
}