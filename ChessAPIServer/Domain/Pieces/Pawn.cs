namespace APIServer.Domain.Pieces;
public class Pawn : Piece
{
    public new const string code_name = "P";
    public Boolean first_moved = false;

    public override bool MoveCheck(int index)
    {
        throw new NotImplementedException();
    }
}