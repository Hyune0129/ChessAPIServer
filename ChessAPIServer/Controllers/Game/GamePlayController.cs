using APIServer.DTO;
using APIServer.DTO.Game;
using APIServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ZLogger;

namespace APIServer.Controllers.Game;

[ApiController]
[Route("[controller]")]
public class GamePlayController : ControllerBase
{
    private readonly ILogger<GamePlayController> _logger;
    private readonly IGameService _gameService;

    public GamePlayController(ILogger<GamePlayController> logger, IGameService gameService)
    {
        _logger = logger;
        _gameService = gameService;
    }

    [HttpPost("/movepiece")]
    public async Task<PieceMoveResponse> MovePiece([FromHeader] HeaderDTO header, PieceMoveRequest request)
    {
        PieceMoveResponse response = new();
        response.Result = await _gameService.MovePiece(header.Uid, request.move_code);
        _logger.ZLogInformation($"[MovePiece] Uid : {header.Uid}, move_code : {request.move_code}");
        return response;
    }

    [HttpPost("/waitTurn")]
    public async Task<TurnWaitResponse> WaitTurn(TurnWaitRequest request)
    {
        // long polling system
        TurnWaitResponse response = new();
        response.Result = await _gameService.WaitTurn(request.game_key);

        return response;
    }
}