using APIServer.Domain;
using APIServer.Repository.Interfaces;
using CloudStructures;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace APIServer.Repository;

// use redis
public partial class MemoryDb : IMemoryDb
{
    readonly RedisConnection _redisConn;
    readonly ILogger<MemoryDb> _logger;
    readonly IOptions<DbConfig> _dbConfig;
    readonly InitBoardMaker _boardMaker;

    public MemoryDb(ILogger<MemoryDb> logger, IOptions<DbConfig> dbConfig, InitBoardMaker boardMaker)
    {
        _logger = logger;
        _dbConfig = dbConfig;
        RedisConfig config = new("default", _dbConfig.Value.Redis);
        _redisConn = new RedisConnection(config);
        _boardMaker = boardMaker;
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

    public TimeSpan GameKeyTimeSpan()
    {
        return TimeSpan.FromHours(RediskeyExpireTime.GameKeyTimeSpanHour);
    }

}