namespace ChessCLI.Chess.Domain.Pieces;
public class Knight : Piece
{

    public Knight(int positionRow, int positionCol, Team team) : base(positionRow, positionCol, team)
    {
        pieceType = PieceType.Knight;
    }
    public override bool MoveCheck(int row, int col)
    {
        if (row < 0 || row > 7 || col < 0 || col > 7)
        {
            return false;
        }
        if ((positionRow - row == 2 && positionCol - col == 1) || (positionRow - row == 2 && positionCol - col == -1) ||
            (positionRow - row == -2 && positionCol - col == 1) || (positionRow - row == -2 && positionCol - col == -1) ||
            (positionRow - row == 1 && positionCol - col == 2) || (positionRow - row == 1 && positionCol - col == -2) ||
            (positionRow - row == -1 && positionCol - col == 2) || (positionRow - row == -1 && positionCol - col == -2))
        {
            return true;
        }
        return false;
    }
}