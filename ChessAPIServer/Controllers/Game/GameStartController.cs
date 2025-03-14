using APIServer.DTO;
using APIServer.Services.Interfaces;
using ChessAPIServer.DTO.Game;
using Microsoft.AspNetCore.Mvc;

namespace ChessAPIServer.Controllers.Game;

[ApiController]
[Route("[controller]")]
public class GameStartController : ControllerBase
{
    readonly ILogger<GameStartController> _logger;
    readonly IGameService _GameService;

    public GameStartController(ILogger<GameStartController> logger, IGameService gameService)
    {
        _logger = logger;
        _GameService = gameService;
    }

    [HttpGet]
    public Task<GameStartResponse> StartGame([FromHeader] HeaderDTO header)
    {
        throw new NotImplementedException();
    }
}