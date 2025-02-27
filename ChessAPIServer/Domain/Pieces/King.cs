namespace APIServer.Domain.Pieces;
public class King : Piece
{
    const PieceType pieceType = PieceType.King;
    public King(int positionRow, int positionCol, Team team) : base(positionRow, positionCol, team)
    {
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

        if (Math.Abs(positionRow - row) <= 1 && Math.Abs(positionCol - col) <= 1)
        {
            return true;
        }
        return false;
    }
}