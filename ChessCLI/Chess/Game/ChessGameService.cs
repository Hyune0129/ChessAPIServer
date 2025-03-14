using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ChessCLI.Chess.Authentication;
using ChessCLI.Chess.responses.Game;

namespace ChessCLI.Chess.Game;

public class ChessGameService
{
    HttpClient _client;
    ChessSessionManager _sessionManager;

    public ChessGameService(ChessSessionManager sessionManager)
    {
        _sessionManager = sessionManager;
        _client = HttpClientSingleton.GetChessClient();
    }

    public async Task GetGameInfo()
    {

    }

    public async Task Surrender()
    {
        throw new NotImplementedException();
    }

    public async Task PieceMove()
    {

        throw new NotImplementedException();
    }

    public async Task TurnWait()
    {

        throw new NotImplementedException();
    }

    public async Task<ChessServerErrorCode> SendPieceMove(long room_id, string from, string to, string piece)
    {
        StringContent json = new(JsonSerializer.Serialize(new
        {
            room_id = room_id,
            from = from,
            to = to,
            piece = piece
        }), Encoding.UTF8, "application/json");

        HttpResponseMessage httpResponse = await _client.PostAsync("/movepiece", json);
        PieceMoveResponse response = await httpResponse.Content.ReadFromJsonAsync<PieceMoveResponse>();
        return response.Result;
    }

    public async Task SendTurnWait()
    {

        HttpResponseMessage httpResponse = await _client.GetAsync("/waitTurn");
        throw new NotImplementedException();
    }

    public async Task<ChessServerErrorCode> SendGameSurrender(long room_id)
    {
        StringContent json = new(JsonSerializer.Serialize(new
        {
            room_id = room_id
        }), Encoding.UTF8, "application/json");
        HttpResponseMessage httpResponse = await _client.PostAsync("/GameSurrender", json);
        GameSurrenderResponse response = await httpResponse.Content.ReadFromJsonAsync<GameSurrenderResponse>();
        return response.Result;
    }

    bool IsKingCheck()
    {
        throw new NotImplementedException();
    }

    bool IsCheckMate()
    {
        throw new NotImplementedException();
    }

    bool IsStaleMate()
    {
        throw new NotImplementedException();
    }
}
