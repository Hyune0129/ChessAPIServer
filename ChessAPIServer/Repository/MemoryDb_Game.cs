using APIServer.Domain;
using APIServer.Models;
using APIServer.Repository.Interfaces;
using CloudStructures;
using CloudStructures.Structures;
using ZLogger;

namespace APIServer.Repository;
public partial class MemoryDb : IMemoryDb
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns>
    /// bool | true : playing game, false : not playing game
    /// int  | return playing room number if not playing, return -1;
    /// </returns>

    public async Task<(bool, int)> GetRoomNumber(long uid)
    {
        var key = MemoryDbKeyMaker.MakeUIDKey(uid.ToString());
        RedisString<int> redis = new(_redisConn, key, null);
        RedisResult<int> num = await redis.GetAsync();
        if (!num.HasValue)
        {
            _logger.ZLogError($"[MemoryDb.GetRommNumber] key = {key}, ErrorMessage : Not Find Matched RoomNumber, Get RoomNumber Error");
            return (false, -1);
        }
        _logger.ZLogInformation($"[MemoryDb.GetRommNumber] key = {key}");
        return (true, num.Value);
    }


    public async Task<RdbRoomData> GetRoomDataByRoomid(long roomid)
    {
        var key = MemoryDbKeyMaker.MakeRoomNumKey(roomid.ToString());
        RedisDictionary<string, string> redis = new(_redisConn, key, GameKeyTimeSpan());
        Dictionary<string, string> redisResult = await redis.GetAllAsync();
        if (redisResult.Count == 0)
        {
            _logger.ZLogError($"[MemoryDb.GetRoomDataByRoomid] Key = {key}, ErrorMessage : Not Find Matched RoomNumber");
            return null;
        }
        RdbRoomData room = new()
        {
            white_uid = redisResult.GetValueOrDefault("white_uid", "-1"),
            black_uid = redisResult.GetValueOrDefault("black_uid", "-1"),
            turn = redisResult.GetValueOrDefault("turn", "None"),
            last_move = redisResult.GetValueOrDefault("last_move", " ")
        };

        _logger.ZLogInformation($"[MemoryDb.GetRoomDataByRoomid] Key = {key}");
        return room;
    }

    public async Task<RdbGameData> GetGameDataByRoomid(long roomid)
    {
        var key = MemoryDbKeyMaker.MakeGameNumKey(roomid.ToString());
        RedisString<byte[]> redis = new RedisString<byte[]>(_redisConn, key, GameKeyTimeSpan());
        RedisResult<byte[]> board = await redis.GetAsync();
        if (!board.HasValue)
        {
            _logger.ZLogDebug($"[MemoryDb.GetGameDataByRoomid] roomid : {roomid} Error : not matched game by roomid");
            return null;
        }
        _logger.ZLogInformation($"[MemoryDb.GetGameDataByRoomid] roomid : {roomid}");
        return new RdbGameData() { board = (byte[])board.GetValueOrNull() };
    }

    public async Task<bool> CreateRoom(long roomid, long white_uid, long black_uid)
    {
        var key = MemoryDbKeyMaker.MakeRoomNumKey(roomid.ToString());

        RdbRoomData room = new()
        {
            white_uid = white_uid.ToString(),
            black_uid = black_uid.ToString()
        };

        RedisHashSet<RdbRoomData> redis = new(_redisConn, key, GameKeyTimeSpan());

        if (!await redis.AddAsync(room))
        {
            _logger.ZLogError($"[MemoryDb.CreateRoom] key = {key}, ErrorMessage : redis Create Failed");
            return false;
        }

        _logger.ZLogInformation($"[MemoryDb.CreateRoom] key = {key}");
        return true;
    }

    public async Task<bool> CreateGame(long roomid)
    {
        var key = MemoryDbKeyMaker.MakeGameNumKey(roomid.ToString());
        RdbGameData game = new()
        {
            board = _boardMaker.GetInitBoard() // 8 * 8 => byte[64] chess board
        };

        RedisString<RdbGameData> redisString = new(_redisConn, key, GameKeyTimeSpan());
        if (!await redisString.SetAsync(game))
        {
            _logger.ZLogError($"[MemoryDb.CreateGame] key = {key}, ErrorMessage : redis Create Failed");
            return false;
        }
        _logger.ZLogInformation($"[MemoryDb.CreateGame] key = {key}");
        return true;
    }

    public async Task<bool> UpdateRoomData(long roomid, string turn, int turnCount, string last_move)
    {
        var key = MemoryDbKeyMaker.MakeRoomNumKey(roomid.ToString());

        RdbRoomData room = new()
        {
            turn = turn,
            turnCount = turnCount,
            last_move = last_move
        };

        RedisHashSet<RdbRoomData> redis = new(_redisConn, key, GameKeyTimeSpan());
        if (!await redis.AddAsync(room))
        {
            _logger.ZLogDebug($"[MemoeyDb.UpdataRoomData] roomid : {roomid}, turn : {turn}, last_move : {last_move} Error : redis update error");
            return false;
        }

        _logger.ZLogInformation($"[MemoeyDb.UpdataRoomData] roomid : {roomid}, turn : {turn}, last_move : {last_move}");
        return true;

    }

    public async Task<string> GetTurnByRoomid(long roomid)
    {
        var key = MemoryDbKeyMaker.MakeRoomNumKey(roomid.ToString());

        RedisDictionary<string, string> redis = new(_redisConn, key, GameKeyTimeSpan());

        RedisResult<string> result = await redis.GetAsync("turn");
        if (!result.HasValue)
        {
            return null;
        }
        return result.Value;
    }

    public async Task<bool> UpdateGameData(long roomid, byte[] board)
    {
        var key = MemoryDbKeyMaker.MakeGameNumKey(roomid.ToString());
        RedisString<RdbGameData> redis = new(_redisConn, key, GameKeyTimeSpan());
        RdbGameData game = new()
        {
            board = board
        };
        if (!await redis.SetAsync(game))
        {
            _logger.ZLogDebug($"[MemoryDb.UpdateGameData] roomid : {roomid} Error : redis update error");
            return false;
        }

        _logger.ZLogInformation($"[MemoryDb.UpdateGameData] roomid : {roomid}");
        return true;
    }

    // public async Task<bool> DeleteRoom(long roomid)
    // {
    //     var key = MemoryDbKeyMaker.MakeRoomNumKey(roomid.ToString());
    //     RedisHashSet<RdbRoomData> redis = new(_redisConn, key, GameKeyTimeSpan());
    //     return await redis.DeleteAsync();
    // }
    // public async Task<bool> DeleteGame(long roomid)
    // {
    //     var key = MemoryDbKeyMaker.MakeGameNumKey(roomid.ToString());
    //     RedisString<byte[]> redis = new(_redisConn, key, GameKeyTimeSpan());
    //     return await redis.DeleteAsync();
    // }


}