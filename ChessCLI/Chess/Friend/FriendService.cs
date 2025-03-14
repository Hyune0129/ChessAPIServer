using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ChessCLI.Chess.Authentication;
using ChessCLI.Chess.responses.Friend;
using ChessCLI.Chess.Responses.Friend;

namespace ChessCLI.Chess.Friend;

// todo : Friend implement

public class FriendService
{
    readonly HttpClient _client;
    readonly ChessSessionManager _chessSessionManager;

    public FriendService(ChessSessionManager chessSessionManager)
    {
        _client = HttpClientSingleton.GetChessClient();
        _chessSessionManager = chessSessionManager;
    }

    public async Task AcceptFriend()
    {
        throw new NotImplementedException();
        Console.WriteLine("Accept Friend");
    }

    public async Task DeleteFriend()
    {
        throw new NotImplementedException();
        Console.WriteLine("Delete Friend");
    }

    public async Task ListFriend()
    {
        throw new NotImplementedException();
        Console.WriteLine("List Friend");
    }

    public async Task SendFriendReq()
    {
        throw new NotImplementedException();
        Console.WriteLine("Send Friend Request");
    }

    public async Task SendFriendReqSend(long uid)
    {
        throw new NotImplementedException();
        Console.WriteLine("Send Friend Request");
    }

    public async Task<ChessServerErrorCode> SendFriendReqAccept(long FriendId)
    {
        StringContent json = new(JsonSerializer.Serialize(new
        {
            FriendId = FriendId
        }), Encoding.UTF8, "application/json");
        HttpResponseMessage httpResponse = await _client.PostAsync("/FriendAccept", json);
        FriendAcceptResponse response = await httpResponse.Content.ReadFromJsonAsync<FriendAcceptResponse>();
        return response.Result;
    }

    public async Task<(ChessServerErrorCode, IEnumerable<ChessUser>)> SendListFriend()
    {
        HttpResponseMessage httpResponse = await _client.GetAsync("/FriendList");
        FriendListResponse response = await httpResponse.Content.ReadFromJsonAsync<FriendListResponse>();
        return (response.Result, response.FriendList);
    }

    public async Task<ChessServerErrorCode> SendDeleteFriend(long FriendUid)
    {
        StringContent json = new(JsonSerializer.Serialize(new
        {
            FriendUid = FriendUid
        }), Encoding.UTF8, "application/json");
        HttpResponseMessage httpResponse = await _client.PostAsync("/FriendDelete", json);
        FriendDeleteResponse response = await httpResponse.Content.ReadFromJsonAsync<FriendDeleteResponse>();
        return response.Result;
    }

}