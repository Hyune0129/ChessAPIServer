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

    public async Task<(bool, int)> GetRoomNumber(string uid)
    {
        var key = MemoryDbKeyMaker.MakeUIDKey(uid);
        try
        {
            RedisString<int> redis = new(_redisConn, key, null);
            RedisResult<int> num = await redis.GetAsync();
            if (!num.HasValue)
            {
                _logger.ZLogError($"[GetRommNumber] key = {key}, ErrorMessage : Not Find Matched RoomNumber, Get RoomNumber Error");
            }

            return (true, num.Value);
        }
        catch (Exception e)
        {
            _logger.ZLogError(e, $"[GetRoomNumber] Key = {key}, ErrorMessage: RedisConnection Error");
            return (false, -1);
        }
    }


    public async Task<RdbRoomData> GetRoomDataByRoomid(long roomid)
    {
        var key = MemoryDbKeyMaker.MakeRoomNumKey(roomid.ToString());
        try
        {
            RedisString<RdbRoomData> redis = new(_redisConn, key, GameKeyTimeSpan());
            RedisResult<RdbRoomData> room = await redis.GetAsync();

            if (!room.HasValue)
            {
                _logger.ZLogError($"[GetRoomDataByRoomid] Key = {key}, ErrorMessage : Not Find Matched RoomNumber");
            }
            return room.Value;
        }
        catch (Exception ex)
        {
            _logger.ZLogError(ex, $"[GetRoomDataByRoomid] Key = {key}, ErrorMessage: RedisConnection Error");
            return null;
        }
    }


    public async Task<bool> CreateGameRoomAsync(long roomid)
    {
        var key = MemoryDbKeyMaker.MakeRoomNumKey(roomid.ToString());

        RdbBoard board = new();

        RdbRoomData room = new()
        {
            board = board.GetInitBoard() // 8 * 8 chess board
        };

        try
        {
            RedisString<RdbRoomData> redis = new(_redisConn, key, GameKeyTimeSpan());
            if (!await redis.SetAsync(room))
            {
                _logger.ZLogError($"[CreateGameRoomAsync] Key = {key}, ErrorMessage : redis Create Failed");
            }
            return true;
        }
        catch (Exception ex)
        {
            _logger.ZLogError(ex, $"[CreateGameRoomAsync] Key = {key}, ErrorMessage : RedisConnection Error");
            return false;
        }
    }


}