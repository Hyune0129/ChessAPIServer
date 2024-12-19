namespace APIServer.Models.GameDB;


public class Pawn : Piece
{
    public new const string code_name = "P";
    public Boolean first_moved = false;

}

public class Rook : Piece
{
    public new const string code_name = "R";

}
public class Bishop : Piece
{
    public new const string code_name = "B";

}

public class Knight : Piece
{
    public new const string code_name = "N";

}
public class King : Piece
{
    public new const string code_name = "K";

}
public class Queen : Piece
{
    public new const string code_name = "Q";
}
public abstract class Piece
{
    public string code_name = "";
    public string team { get; set; } // black or white
    public string position { get; set; } // "a4, c2 .."
}
