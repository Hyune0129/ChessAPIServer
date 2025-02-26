namespace APIServer.Domain.Pieces;
public class Queen : Piece
{
    public new const string code_name = "Q";

    public override bool MoveCheck(int index)
    {
        throw new NotImplementedException();
    }
}