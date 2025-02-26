namespace APIServer.Domain.Pieces;
public class Bishop : Piece
{
    public new const string code_name = "B";



    public override bool MoveCheck(int index)
    {
        throw new NotImplementedException();
    }
}