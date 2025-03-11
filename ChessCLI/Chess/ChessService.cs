using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ChessCLI.Authentication;
using ChessCLI.Chess.Authentication;
using ChessCLI.Chess.Responses.Auth;

namespace ChessCLI.Chess;

public class ChessService
{
    readonly HttpClient _client;
    readonly SessionManager _sessionManager;
    readonly ChessSessionManager _chessSessionManager;
    public ChessService(SessionManager sessionManager)
    {
        _chessSessionManager = new ChessSessionManager();
        _sessionManager = sessionManager;
        _client = HttpClientSingleton.GetChessClient();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sessionManager.currentUser.token);
    }

    public async Task<ChessServerErrorCode> ChessServerLogin()
    {
        LoginResponse response = await SendLoginReq();
        if (response.Result != ChessServerErrorCode.None)
        {
            return response.Result;
        }
        _chessSessionManager.currentUser = response.userData.userInfo;
        _chessSessionManager.isLogined = true;

        return response.Result;
    }

    public async Task<LoginResponse> SendLoginReq()
    {
        StringContent json = new(JsonSerializer.Serialize(new
        {
            player_id = _sessionManager.currentUser.player_id,
            AccountToken = _sessionManager.currentUser.token
        }), Encoding.UTF8, "application/json");
        HttpResponseMessage httpResponse = await _client.PostAsync("/Login", json);
        return await httpResponse.Content.ReadFromJsonAsync<LoginResponse>();
    }
    public async Task<ChessServerErrorCode> ChessServerLogout()
    {
        HttpResponseMessage httpResponse = await _client.GetAsync("/Logout");
        LogoutResponse response = await httpResponse.Content.ReadFromJsonAsync<LogoutResponse>();
        return response.Result;

    }

}