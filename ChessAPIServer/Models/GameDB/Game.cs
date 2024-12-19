namespace APIServer.Models.GameDB;

public class GdbGameInfo
{
    public int game_key { get; set; }
    public int white_uid { get; set; }
    public int black_uid { get; set; }

    // move record ref : https://www.chess.com/ko/terms/chess-notation-ko
    public List<string> move_record { get; set; }
    public Piece[][] board { get; set; }
}