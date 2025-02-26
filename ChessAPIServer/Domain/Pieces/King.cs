namespace APIServer.Domain.Pieces;
public class King : Piece
{
    public new const string code_name = "K";

    public override bool MoveCheck(int index)
    {
        throw new NotImplementedException();
    }
}