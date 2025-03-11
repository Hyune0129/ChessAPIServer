using ChessCLI.Chess.DataLoad;

namespace ChessCLI.Chess.Responses.Auth;
public class LoginResponse : ChessServerErrorCodeDTO
{
    public ChessUserData userData { get; set; }
}




// {
// "userData": {
//         "userInfo": {
//             "uid": 1,
//             "player_id": 31,
//             "nickname": "tester",
//             "create_dt": "2025-03-05T07:09:16",
//             "recent_login_dt": "2025-03-06T05:34:48",
//             "wins": 0,
//             "loses": 0
//         }
//     },
//     "result": 0
// }