using System.Data;
using APIServer.Repository.Interfaces;
using Microsoft.Extensions.Options;
using MySqlConnector;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace APIServer.Repository;

public partial class GameDb : IGameDb
{
    readonly IOptions<DbConfig> _dbConfig;
    readonly ILogger<GameDb> _logger;
    readonly MySqlCompiler _compiler;
    IDbConnection _dbConn;
    readonly QueryFactory _queryFactory;
    public GameDb(IOptions<DbConfig> dbConfig, ILogger<GameDb> logger)
    {
        _dbConfig = dbConfig;
        _logger = logger;

        Open();

        _compiler = new MySqlCompiler();
        _queryFactory = new QueryFactory(_dbConn, _compiler);

    }

    public void Dispose()
    {
        Close();
    }



    void Open()
    {
        _dbConn = new MySqlConnection(_dbConfig.Value.GameDb);

        _dbConn.Open();
    }

    void Close()
    {
        _dbConn.Close();
    }
}