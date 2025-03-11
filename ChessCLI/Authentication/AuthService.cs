namespace ChessCLI.Authentication;

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ChessCLI.Authentication.responses;

public class AuthService
{
    readonly HttpClient _client;
    readonly SessionManager _sessionManager;

    public AuthService(SessionManager sessionManager)
    {
        _sessionManager = sessionManager;
        _client = HttpClientSingleton.GetLoginClient();
    }

    public async Task<LoginServerErrorCode> Login(string email, string password)
    {

        LoginResponse response = await SendLoginRequest(email, password);
        if (response.Result == LoginServerErrorCode.None)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sessionManager.currentUser.token);
            _sessionManager.currentUser.token = response.token;
            _sessionManager.currentUser.player_id = response.userID;
            _sessionManager.isLogined = true;
        }
        return response.Result;
    }

    public async Task<LoginServerErrorCode> Register(string email, string password)
    {
        CreateAccountResponse response = await SendCreateAccountRequest(email, password);
        return response.Result;
    }

    public async Task<LoginServerErrorCode> Logout()
    {
        LoginServerErrorCode response = await SendLogoutRequest();

        if (response != LoginServerErrorCode.None)
        {
            return response;
        }
        _client.DefaultRequestHeaders.Authorization = null;
        _sessionManager.currentUser = new User();
        _sessionManager.isLogined = false;
        return 0;
    }

    public async Task<LoginResponse> SendLoginRequest(string email, string password)
    {
        StringContent json = new(JsonSerializer.Serialize(new
        {
            UserId = email,
            Password = password
        }), Encoding.UTF8, "application/json");

        HttpResponseMessage httpResponse = await _client.PostAsync("/LoginAccount", json);

        return await httpResponse.Content.ReadFromJsonAsync<LoginResponse>();
    }

    public async Task<CreateAccountResponse> SendCreateAccountRequest(string email, string password)
    {
        StringContent json = new(JsonSerializer.Serialize(new
        {
            UserId = email,
            Password = password
        }), Encoding.UTF8, "application/json");

        HttpResponseMessage httpResponse = await _client.PostAsync("/CreateAccount", json);

        return await httpResponse.Content.ReadFromJsonAsync<CreateAccountResponse>();
    }

    public async Task<LoginServerErrorCode> SendLogoutRequest()
    {
        HttpResponseMessage httpResponse = await _client.PostAsync("/Logout", null);
        LogoutResponse response = await httpResponse.Content.ReadFromJsonAsync<LogoutResponse>();
        return response.Result;
    }

}



