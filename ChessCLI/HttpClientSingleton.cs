namespace ChessCLI;
public static class HttpClientSingleton
{
    static readonly string login_url = "http://localhost:5073";
    static readonly string chess_url = "http://localhost:5074";
    static readonly HttpClient _chessClient;
    static readonly HttpClient _loginClient;
    static HttpClientSingleton()
    {
        _chessClient = new HttpClient()
        {
            BaseAddress = new Uri(chess_url)
        };
        _chessClient.DefaultRequestHeaders.Add("appVersion", Version.appVersion);
        _loginClient = new HttpClient()
        {
            BaseAddress = new Uri(login_url)
        };
    }

    public static HttpClient GetChessClient() => _chessClient;
    public static HttpClient GetLoginClient() => _loginClient;

}