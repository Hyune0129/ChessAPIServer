using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using APIServer;
using APIServer.Repository;
using APIServer.Services;
using APIServer.DTO.Worker;
using APIServer.Worker.Interface;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using ZLogger;

namespace APIServer.Worker;

public class MatchWorker : IMatchWorker
{
    readonly GameService _gameService;
    readonly ILogger<MatchWorker> _logger;
    System.Threading.Thread _worker;
    readonly string _matchingFoundRedisAddress = "";
    readonly string _roomCreatedRedisAddress = "";
    readonly string _matchingRedisPubKey = ""; // receive from match server
    readonly string _matchingRedisSubKey = ""; // send match server
    readonly string _serverAddress = "";
    readonly ConnectionMultiplexer _redisMatchingFound;
    readonly ConnectionMultiplexer _redisRoomCreated;
    readonly ISubscriber _subScriberMatchingFound;
    readonly ISubscriber _subScriberRoomCreated;
    public MatchWorker(GameService gameService, ILogger<MatchWorker> logger, IOptions<MatchingConfig> options)
    {
        _gameService = gameService;
        _logger = logger;

        _matchingRedisPubKey = options.Value.PubKey;
        _matchingRedisSubKey = options.Value.SubKey;

        _redisMatchingFound = ConnectionMultiplexer.Connect(options.Value.MatchingFoundRedisAddress);
        _redisRoomCreated = ConnectionMultiplexer.Connect(options.Value.RoomCreatedRedisAddress);

        _subScriberMatchingFound = _redisMatchingFound.GetSubscriber();
        _subScriberRoomCreated = _redisRoomCreated.GetSubscriber();

        _serverAddress = options.Value.ServerAddress;

        _worker = new(this.MatchAndCreateRoom);
        _worker.Start();
    }

    // sub matching
    async void MatchAndCreateRoom()
    {
        var channel = _subScriberMatchingFound.Subscribe(_matchingRedisSubKey);
        channel.OnMessage(async message =>
        {
            MatchingFound? matching = JsonSerializer.Deserialize<MatchingFound>(message.Message);
            if (matching != null)
            {
                (ErrorCode Result, long room_id) = await _gameService.GameInit(matching.white_uid, matching.black_uid, false);
                if (Result != ErrorCode.None)
                {
                    _logger.ZLogDebug($"");
                }
                RoomCreated room = new()
                {
                    room_id = room_id,
                    server_address = _serverAddress
                };
                string json = JsonSerializer.Serialize<RoomCreated>(room);

                _subScriberRoomCreated.Publish(_matchingRedisPubKey, json);
            }
        });
    }

    public void Dispose()
    {
        _logger.LogInformation("MatchingWorker Dispose");
    }
}


