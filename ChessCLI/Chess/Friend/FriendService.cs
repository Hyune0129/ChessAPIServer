using ChessCLI.Chess.Authentication;

namespace ChessCLI.Chess.Friend;

public class FriendService
{
    readonly HttpClient _client;
    readonly ChessSessionManager _chessSessionManager;

    public FriendService(ChessSessionManager chessSessionManager)
    {
        _client = HttpClientSingleton.GetChessClient();
    }
    public void SendFriendRequest(long uid)
    {
        throw new NotImplementedException();
        Console.WriteLine("Send Friend Request");
    }

    public void RemoveFriend()
    {
        throw new NotImplementedException();
        Console.WriteLine("Remove Friend");
    }
}