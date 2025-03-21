namespace APIServer.Models.GameDB;

public class GdbUserInfo
{
    public long uid { get; set; } // user => game id
    public long player_id { get; set; } // login server's primary id
    public string nickname { get; set; }
    public DateTime create_dt { get; set; }
    public DateTime recent_login_dt { get; set; }
    public int wins { get; set; }
    public int loses { get; set; }
}