using APIServer.DTO;
using APIServer.DTO.Game;
using APIServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ZLogger;

namespace APIServer.Controllers.Game;

[ApiController]
[Route("[controller]")]
public class GameSurrenderController : ControllerBase
{
    private readonly ILogger<GameSurrenderController> _logger;
    private readonly IGameService _gameService;
    public GameSurrenderController(ILogger<GameSurrenderController> logger, IGameService gameService)
    {
        _logger = logger;
        _gameService = gameService;
    }

    [HttpPost]
    public async Task<GameSurrenderResponse> SurrenderGame([FromHeader] HeaderDTO header, GameSurrenderRequest request)
    {
        GameSurrenderResponse response = new();
        response.Result = await _gameService.SurrenderGame(header.Uid, request.room_id);
        _logger.ZLogInformation($"[GameSurrender] Uid : {header.Uid}, room_id : {request.room_id}");
        return response;
    }
}