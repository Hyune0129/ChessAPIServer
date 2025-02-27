namespace APIServer.Domain.Pieces;
public class Pawn : Piece
{
    public Boolean first_moved = false;

    public Pawn(int positionRow, int positionCol, Team team) : base(positionRow, positionCol, team)
    {
        switch (team)
        {
            case Team.White:
                if (positionRow != 6)
                {
                    first_moved = true;
                }
                break;
            case Team.Black:
                if (positionRow != 1)
                {
                    first_moved = true;
                }
                break;
        }
        first_moved = false;
    }

    public override bool MoveCheck(int row, int col)
    {
        if (row < 0 || row > 7 || col < 0 || col > 7)
        {
            return false;
        }
        switch (team)
        {
            case Team.White:
                // case of moving
                if (first_moved)
                {
                    if (positionRow - row == 2 && positionCol == col)
                    {
                        return true;
                    }
                }
                if (positionRow - row == 1 && positionCol == col)
                {
                    return true;
                }

                // case of eating
                if ((positionRow - row == 1 && positionCol - col == 1) || (positionRow - row == 1 && positionCol - col == -1))
                {
                    return true;
                }
                return false;
            case Team.Black:
                // case of moving
                if (first_moved)
                {
                    if (row - positionRow == 2 && positionCol == col)
                    {
                        return true;
                    }
                }

                if (row - positionRow == 1 && positionCol == col)
                {
                    return true;
                }

                // case of eating
                if ((row - positionRow == 1 && col - positionCol == 1) || (row - positionRow == 1 && col - positionCol == -1))
                {
                    return true;
                }
                return false;
            default:
                return false;
        }
    }

}