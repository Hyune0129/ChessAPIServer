using APIServer.Models;
using APIServer.Repository.Interfaces;
using CloudStructures;
using CloudStructures.Structures;
using ZLogger;

namespace APIServer.Repository;

public partial class MemoryDb : IMemoryDb
{

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

    public async Task<ErrorCode> DeleteUserAuthAsync(long uid)
    {
        var key = MemoryDbKeyMaker.MakeUIDKey(uid.ToString());
        try
        {
            var redis = new RedisString<String>(_redisConn, key, LoginTimeSpan());
            if (await redis.DeleteAsync() == false)
            {
                _logger.ZLogError($"[MemoryDb.DeleteUserAuthAsync] uid :{uid}, ErrorMessage : Redis Delete Error");
                return ErrorCode.LogoutRedisDelFail;
            }
            return ErrorCode.None;
        }
        catch (Exception ex)
        {
            _logger.ZLogError(ex, $"[MemoryDb.DeleteUserAuthAsync] uid :{uid}, ErrorMessage : Redis Connection Error");
            return ErrorCode.LogoutRedisDelFail;
        }

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


}