using System.Data;
using APIServer.Models;
using APIServer.Repository.Interfaces;
using Microsoft.Extensions.Options;
using MySqlConnector;
using SqlKata.Compilers;
using SqlKata.Execution;
using ZLogger;

namespace APIServer.Repository;

public class MasterDb : IMasterDb
{
    readonly IOptions<DbConfig> _dbConfig;
    readonly ILogger<MasterDb> _logger;
    readonly MySqlCompiler _compiler;
    IDbConnection _dbConn;
    readonly IGameDb _gameDb;
    readonly IMemoryDb _memoryDb;
    readonly QueryFactory _queryFactory;
    public VersionDAO _version { get; set; }

    // ... list<data> _~list

    public MasterDb(ILogger<MasterDb> logger, IOptions<DbConfig> dbConfig, IMemoryDb memoryDb, IGameDb gameDb)
    {
        _logger = logger;
        _dbConfig = dbConfig;

        Open();

        _compiler = new MySqlCompiler();
        _queryFactory = new QueryFactory(_dbConn, _compiler);
        _memoryDb = memoryDb;
        _gameDb = gameDb;
    }

    public void Dispose()
    {
        Close();
    }

    public async Task<bool> Load()
    {
        try
        {
            _version = await _queryFactory.Query($"version").FirstOrDefaultAsync<VersionDAO>();
        }
        catch (Exception e)
        {
            // Console.WriteLine(e);
            _logger.ZLogError(e,
            $"[MasterDb.Load] ErrorCode: {ErrorCode.MasterDB_Fail_LoadData}");
            return false;
        }
        return true;
    }

    void Open()
    {
        _dbConn = new MySqlConnection(_dbConfig.Value.MasterDb);

        _dbConn.Open();
    }

    void Close()
    {
        _dbConn.Close();
    }
}