namespace APIServer.Models.GameDB;


public class Pawn : Piece
{
    public new const string code_name = "P";
    public Boolean first_moved = false;

    public override bool MoveCheck(string pos)
    {
        throw new NotImplementedException();
    }
}

public class Rook : Piece
{

    public new const string code_name = "R";

    public override bool MoveCheck(string pos)
    {
        throw new NotImplementedException();
    }
}
public class Bishop : Piece
{
    public new const string code_name = "B";


    public override bool MoveCheck(string pos)
    {
        throw new NotImplementedException();
    }
}

public class Knight : Piece
{
    public new const string code_name = "N";

    public override bool MoveCheck(string pos)
    {
        throw new NotImplementedException();
    }
}
public class King : Piece
{
    public new const string code_name = "K";

    public override bool MoveCheck(string pos)
    {
        throw new NotImplementedException();
    }
}
public class Queen : Piece
{
    public new const string code_name = "Q";

    public override bool MoveCheck(string pos)
    {
        throw new NotImplementedException();
    }
}
public abstract class Piece
{
    public string code_name = "";
    public Team team { get; set; } // White, Black
    public string position { get; set; } // "a4, c2 .."

    /// <summary>
    /// check vaild move and not over boundary
    /// </summary>
    /// <param name="pos"> col = a,b,c,d, ...,h  row = 1,2,3,4,...,8</param>
    /// 
    public abstract bool MoveCheck(string pos);
}

public enum Team
{
    White, Black
}
