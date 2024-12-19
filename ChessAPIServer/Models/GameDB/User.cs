namespace APIServer.Models.GameDB;

public class GdbUserInfo
{
    public int pid { get; set; }
    public string nickname { get; set; }
    public DateTime create_dt { get; set; }
    public DateTime recent_login_dt { get; set; }
    public int wins { get; set; }
    public int loses { get; set; }
}