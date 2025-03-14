using APIServer.DTO;
using APIServer.DTO.Game;
using APIServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ZLogger;

namespace APIServer.Controllers.Game;

[ApiController]
[Route("[controller]")]
public class MovePieceController : ControllerBase
{
    private readonly ILogger<MovePieceController> _logger;
    private readonly IGameService _gameService;

    public MovePieceController(ILogger<MovePieceController> logger, IGameService gameService)
    {
        _logger = logger;
        _gameService = gameService;
    }

    [HttpPost]
    public async Task<PieceMoveResponse> MovePiece([FromHeader] HeaderDTO header, PieceMoveRequest request)
    {
        PieceMoveResponse response = new();
        response.Result = await _gameService.MovePiece(header.Uid, request.room_id, request.piece, request.from, request.to);
        _logger.ZLogInformation($"[MovePiece] Uid : {header.Uid}, room_id : {request.room_id}, piece : {request.piece}, from : {request.from}, to : {request.to}");
        return response;
    }

}