using APIServer.DTO;
using APIServer.DTO.Game;
using APIServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChessAPIServer.Controllers.Game;

[ApiController]
[Route("[controller]")]
public class WaitTurnController : ControllerBase
{
    readonly IGameService _gameService;
    readonly ILogger<WaitTurnController> _logger;

    public WaitTurnController(IGameService gameService, ILogger<WaitTurnController> logger)
    {
        _gameService = gameService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<TurnWaitResponse> WaitTurn([FromHeader] HeaderDTO header, TurnWaitRequest request)
    {
        // long polling system
        TurnWaitResponse response = new();
        (response.Result, response.roomData) = await _gameService.WaitTurn(header.Uid, request.room_id);

        return response;
    }


}
