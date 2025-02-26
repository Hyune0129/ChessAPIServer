namespace APIServer.Domain.Pieces;
public class Knight : Piece
{
    public new const string code_name = "N";

    public override bool MoveCheck(int index)
    {
        throw new NotImplementedException();
    }
}