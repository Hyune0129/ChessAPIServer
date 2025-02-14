using APIServer.Models;
using APIServer.Repository.Interfaces;
using APIServer.Services;
using CloudStructures;
using CloudStructures.Structures;
using Microsoft.Extensions.Options;
using ZLogger;

namespace APIServer.Repository;

// use redis
public class MemoryDb : IMemoryDb
{
    readonly RedisConnection _redisConn;
    readonly ILogger<MemoryDb> _logger;
    readonly IOptions<DbConfig> _dbConfig;

    public MemoryDb(ILogger<MemoryDb> logger, IOptions<DbConfig> dbConfig)
    {
        _logger = logger;
        _dbConfig = dbConfig;
        RedisConfig config = new("default", _dbConfig.Value.Redis);
        _redisConn = new RedisConnection(config);
    }

    /// <summary>
    /// register token, uid => login
    /// </summary>
    public async Task<ErrorCode> RegisterUserAuthAsync(string token, long uid)
    {
        var key = MemoryDbKeyMaker.MakeUIDKey(uid.ToString());
        ErrorCode result = ErrorCode.None;
        RdbAuthUserData user = new()
        {
            Uid = uid,
            Token = token
        };

        try
        {
            RedisString<RdbAuthUserData> redis = new(_redisConn, key, LoginTimeSpan());
            if (await redis.SetAsync(user, LoginTimeSpan()) == false)
            {
                _logger.ZLogError($"[RegistUserAsync] Uid:{uid}, Token:{token}, ErrorMemssage : UserBasicAuth, RedisString set Error");
                result = ErrorCode.LoginFailAddRedis;
                return result;
            }
        }
        catch (Exception e)
        {
            _logger.ZLogError(e, $"[RegistUserAsync] Uid:{uid}, Token:{token}, ErrorMessage:Redis Connection Error");
            result = ErrorCode.LoginFailAddRedis;
            return result;
        }

        return result;
    }

    public Task<ErrorCode> DeleteUserAuthAsync(long uid)
    {
        throw new NotImplementedException();
    }

    public async Task<ErrorCode> CheckUserAuthAsync(string id, string authToken)
    {
        var key = MemoryDbKeyMaker.MakeUIDKey(id);
        ErrorCode result = ErrorCode.None;

        try
        {
            RedisString<RdbAuthUserData> redis = new(_redisConn, key, null);
            RedisResult<RdbAuthUserData> user = await redis.GetAsync();

            if (!user.HasValue)
            {
                _logger.ZLogError($"[CheckUserAuthAsync] Email = {id}, AuthToken = {authToken}, ErrorMessage: ID dose Not Exist");
                result = ErrorCode.CheckAuthFailNotExist;
                return result;
            }

            if (user.Value.Uid.ToString() != id || user.Value.Token != authToken)
            {
                _logger.ZLogError($"[CheckUserAuthAsync] Email = {id}, AuthToken = {authToken}, ErrorMessage = Wrong ID or Auth Token");
                result = ErrorCode.CheckAuthFailNotMatch;
                return result;
            }
        }
        catch (Exception e)
        {
            _logger.ZLogError(e, $"[CheckUserAuthAsync] Email = {id}, AuthToken = {authToken}, ErrorMessage:Redis Connection Error");
            result = ErrorCode.CheckAuthFailException;
            return result;
        }

        return result;
    }

    public async Task<(bool, RdbAuthUserData)> GetUserAsync(string id)
    {
        var uid = MemoryDbKeyMaker.MakeUIDKey(id);

        try
        {
            RedisString<RdbAuthUserData> redis = new(_redisConn, uid, null);
            RedisResult<RdbAuthUserData> user = await redis.GetAsync();
            if (!user.HasValue)
            {
                _logger.ZLogError($"[GetUserAsync] UID = {uid}, ErrorMessage = Not Assigned User, RedisString get Error");
                return (false, null);
            }

            return (true, user.Value);
        }
        catch (Exception e)
        {
            _logger.ZLogError(e, $"[GetUserAsync] UID:{uid}, ErrorMessage:ID does Not Exist");
            return (false, null);
        }
    }

    /// <summary>
    /// use in transaction lock
    /// </summary>
    public async Task<bool> LockUserReqAsync(string key)
    {
        try
        {
            RedisString<RdbAuthUserData> redis = new(_redisConn, key, NxKeyTimeSpan());
            if (await redis.SetAsync(new RdbAuthUserData
            {
                //empty
            }, NxKeyTimeSpan(), StackExchange.Redis.When.NotExists) == false)
            {
                return false;
            }


        }
        catch (Exception e)
        {
            _logger.ZLogError(e, $"[SetUserReqLockAsync] Key = {key}, ErrorMessage:Redis Connection Error");
            return false;
        }

        return true;
    }

    /// <summary>
    /// use in transaction unlock
    /// </summary>
    public async Task<bool> UnLockUserReqAsync(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            return false;
        }

        try
        {
            RedisString<RdbAuthUserData> redis = new(_redisConn, key, null);
            var redisResult = await redis.DeleteAsync();
            return redisResult;
        }
        catch (Exception e)
        {
            _logger.ZLogError(e, $"[DelUserReqLockAsync] Key = {key}, ErrorMessage:Redis Connection Error");
            return false;
        }
    }

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

    public TimeSpan LoginTimeSpan()
    {
        return TimeSpan.FromMinutes(RediskeyExpireTime.LoginKeyExpireMin);
    }

    public TimeSpan TicketKeyTimeSpan()
    {
        return TimeSpan.FromSeconds(RediskeyExpireTime.TicketKeyExpireSecond);
    }

    public TimeSpan NxKeyTimeSpan()
    {
        return TimeSpan.FromSeconds(RediskeyExpireTime.NxKeyExpireSecond);
    }

}