namespace APIServer.Domain.Pieces;
public class Rook : Piece
{

    public new const string code_name = "R";

    public Rook(int positionRow, int positionCol, Team team) : base(positionRow, positionCol, team)
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

        // move straight
        if (positionRow == row || positionCol == col)
        {
            return true;
        }
        return false;
    }
}