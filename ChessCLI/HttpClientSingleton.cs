namespace ChessCLI;
public static class HttpClientSingleton
{
    static readonly string login_url = Environment.GetEnvironmentVariable("AccountServerAddress");
    static readonly string chess_url = Environment.GetEnvironmentVariable("GameServerAddress");
    static readonly string match_url = Environment.GetEnvironmentVariable("MatchServerAddress");
    static readonly HttpClient _chessClient;
    static readonly HttpClient _loginClient;
    static readonly HttpClient _matchClient;
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
        _matchClient = new HttpClient()
        {
            BaseAddress = new Uri(match_url)
        };
    }

    public static HttpClient GetChessClient() => _chessClient;
    public static HttpClient GetLoginClient() => _loginClient;
    public static HttpClient GetMatchClient() => _matchClient;
}