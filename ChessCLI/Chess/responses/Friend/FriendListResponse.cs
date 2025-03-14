namespace ChessCLI.Chess.responses.Friend;

using ChessCLI.Chess.Authentication;


public class FriendListResponse : ChessServerErrorCodeDTO
{
    // todo : use ChessUser class?
    public IEnumerable<ChessUser> FriendList { get; set; }
}
