namespace APIServer.Models.GameDB;

public class GdbGameInfo
{
    public int game_key { get; set; }
    public int white_uid { get; set; }
    public int black_uid { get; set; }
    public List<string> move_record { get; set; }
    public List<Piece> pieces { get; set; }
}