using System.Net.Http.Json;
using System.Text.Json;
using ChessCLI.Chess.responses.Game;

namespace ChessCLI.Chess.Game;

public class MatchGameService
{

    readonly HttpClient _client;
    public MatchGameService()
    {
        _client = HttpClientSingleton.GetMatchClient();
    }

    public async Task<SendMatchReqResponse> SendMatchReq(long uid)
    {
        SendMatchReqRequest request = new()
        {
            uid = uid
        };
        StringContent json = new(JsonSerializer.Serialize<SendMatchReqRequest>(request));
        HttpResponseMessage httpResponse = await _client.PostAsync("/RequestMatching", json);
        return await httpResponse.Content.ReadFromJsonAsync<SendMatchReqResponse>();
    }

    public async Task<SendCheckMatchingResponse> SendCheckCompletedMatch(long uid)
    {
        SendCheckMatchingRequest request = new()
        {
            uid = uid
        };

        StringContent json = new(JsonSerializer.Serialize<SendCheckMatchingRequest>(request));
        HttpResponseMessage httpResponse = await _client.PostAsync("/CheckMatching", json);
        return await httpResponse.Content.ReadFromJsonAsync<SendCheckMatchingResponse>();
    }
}