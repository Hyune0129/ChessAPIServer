namespace ChessCLI.Authentication.responses;
public class LoginResponse : LoginServerErrorCodeDTO
{
    public string token { get; set; }
    public long userID { get; set; }
}