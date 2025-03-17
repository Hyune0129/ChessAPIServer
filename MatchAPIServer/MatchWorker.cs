using System.Collections.Concurrent;
using System.Text.Json;
using MatchAPIServer.DTO;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace MatchAPIServer;

public interface IMatchWorker : IDisposable
{
    public void AddUser(long uid);
    public (ErrorCode, CompleteMatchingData) GetCompleteMatching(long uid);
}

public class MatchWorker : IMatchWorker
{
    System.Threading.Thread _reqWorker = null;
    ConcurrentQueue<long> _reqQueue = new();

    System.Threading.Thread _completeWorker = null;

    // key : uid
    ConcurrentDictionary<long, int> _completeDic = new();

    // todo: 2개의 Pub/Sub을 사용하므로 Redis 객체가 2개 있어야 한다.
    // PUB/SUB를 이용하는 이유는, 로드 밸런싱을 통한 여러 매칭 서버를 사용할 때를 고려.
    // 매칭서버에서 -> 게임서버, 게임서버 -> 매칭서버로

    readonly string _matchingFoundRedisAddress = "";
    readonly string _roomCreatedRedisAddress = "";
    readonly string _matchingRedisPubKey = ""; // send game server
    readonly string _matchingRedisSubKey = ""; // receive from game server
    readonly string _gameServerAddress = "";
    readonly string _gameServerRequest = "";
    readonly ConnectionMultiplexer _redisMatchingFound;
    readonly ConnectionMultiplexer _redisRoomCreated;
    readonly ISubscriber _subScriberMatchingFound;
    readonly ISubscriber _subScriberRoomCreated;

    public MatchWorker(IOptions<MatchingConfig> matchingConfig)
    {
        _matchingFoundRedisAddress = matchingConfig.Value.MatchingFoundRedisAddress;
        _roomCreatedRedisAddress = matchingConfig.Value.RoomCreatedRedisAddress;
        _matchingRedisPubKey = matchingConfig.Value.PubKey;
        _matchingRedisSubKey = matchingConfig.Value.SubKey;
        _gameServerAddress = matchingConfig.Value.GameServerAddress; // 나중에 게임 서버가 여러개라면?
        _redisRoomCreated = ConnectionMultiplexer.Connect(_roomCreatedRedisAddress);
        _subScriberRoomCreated = _redisRoomCreated.GetSubscriber();

        _redisMatchingFound = ConnectionMultiplexer.Connect(_matchingFoundRedisAddress);
        _subScriberMatchingFound = _redisMatchingFound.GetSubscriber();

        _reqWorker = new System.Threading.Thread(this.RunMatching);
        _reqWorker.Start();

        _completeWorker = new System.Threading.Thread(this.RunMatchingComplete);
        _completeWorker.Start();
    }


    public void AddUser(long uid)
    {
        _reqQueue.Enqueue(uid);
    }


    // user check matching
    public (ErrorCode, CompleteMatchingData) GetCompleteMatching(long uid)
    {
        if (_completeDic.ContainsKey(uid))
        {
            CompleteMatchingData data = new();

            data.ServerAddress = _gameServerAddress;
            data.room_id = _completeDic.GetValueOrDefault(uid, -1);
            return (ErrorCode.None, data);
        }
        return (ErrorCode.ProcessMatching, null);
    }


    void RunMatching()
    {
        Random random = new();
        long user1 = -1;
        long user2 = -1;
        while (true)
        {
            try
            {
                if (_reqQueue.Count < 2)
                {
                    System.Threading.Thread.Sleep(1);
                    continue;
                }

                if (!_reqQueue.TryDequeue(out user1))
                {
                    continue;
                }
                if (!_reqQueue.TryDequeue(out user2))
                {
                    _reqQueue.Enqueue(user1);
                    continue;
                }

                // {
                //      white_uid : 1,
                //      black_uid : 2
                // }
                long white_uid = -1;
                long black_uid = -1;
                if (random.Next() % 2 == 1)
                {
                    white_uid = user1;
                    black_uid = user2;
                }
                else
                {
                    white_uid = user2;
                    black_uid = user1;
                }

                MatchingFound matching = new()
                {
                    white_uid = white_uid,
                    black_uid = black_uid
                };
                string json = JsonSerializer.Serialize(matching);

                _subScriberMatchingFound.Publish(_matchingRedisPubKey, json);

                user1 = -1;
                user2 = -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }


    void RunMatchingComplete()
    {
        long user1 = -1;
        long user2 = -1;
        int room_id = -1;
        var channel = _subScriberRoomCreated.Subscribe(_matchingRedisSubKey);
        try
        {
            // 받은 결과를 _completeDic add
            // event handler라 while(true)를 하지 않음.
            channel.OnMessage(message =>
            {
                RoomCreated? room = JsonSerializer.Deserialize<RoomCreated>(message.Message);
                _completeDic.TryAdd(user1, room_id);
                _completeDic.TryAdd(user2, room_id);
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }


    public void Dispose()
    {
        Console.WriteLine("MatchWorker dispose");
    }


}



public class MatchingConfig
{
    public string MatchingFoundRedisAddress { get; set; }
    public string RoomCreatedRedisAddress { get; set; }
    public string PubKey { get; set; }
    public string SubKey { get; set; }
    public string GameServerAddress { get; set; }
}