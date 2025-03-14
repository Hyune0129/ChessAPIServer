namespace ChessCLI.Chess.Domain.Pieces;
public class Queen : Piece
{
    public Queen(int positionRow, int positionCol, Team team) : base(positionRow, positionCol, team)
    {
        pieceType = PieceType.Queen;
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

        // move diagonal
        if (Math.Abs(positionRow - row) == Math.Abs(positionCol - col))
        {
            return true;
        }
        return false;
    }
}