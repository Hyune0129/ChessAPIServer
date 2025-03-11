using ChessCLI.Authentication;

namespace ChessCLI.Chess.Authentication;
public class ChessUser : User
{
    string nickname;
    long uid;
    int wins = 0;
    int loses = 0;
    DateTime create_dt;
    DateTime recent_login_dt;
}