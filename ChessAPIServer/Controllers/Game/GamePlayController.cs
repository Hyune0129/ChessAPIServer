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
        response.Result = await _gameService.MovePiece(header.Uid, request.room_id, request.piece, request.from, request.to);
        _logger.ZLogInformation($"[MovePiece] Uid : {header.Uid}, room_id : {request.room_id}, piece : {request.piece}, from : {request.from}, to : {request.to}");
        return response;
    }

    [HttpPost("/waitTurn")]
    public async Task<TurnWaitResponse> WaitTurn([FromHeader] HeaderDTO header, TurnWaitRequest request)
    {
        // long polling system
        TurnWaitResponse response = new();
        (response.Result, response.roomData) = await _gameService.WaitTurn(header.Uid, request.room_id);

        return response;
    }
}