namespace APIServer.Model.DAO;

// Ldb : Login database
public class LdbAccountInfo
{
    public Int64 player_id { get; set; }
    public string email { get; set; }
    public string pw { get; set; }
    public string salt_value { get; set; }
    public string recent_login_dt { get; set; }
    public string create_dt { get; set; }
}