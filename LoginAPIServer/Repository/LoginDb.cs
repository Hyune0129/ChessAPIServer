using System.Data;
using APIServer.Model;
using APIServer.Model.DAO;
using APIServer.Utils;
using Microsoft.Extensions.Options;
using MySqlConnector;
using SqlKata.Compilers;
using SqlKata.Execution;
using ZLogger;

namespace APIServer.Repository;

public class LoginDb : ILoginDb
{
    readonly IOptions<DbConfig> _dbConfig;
    readonly ILogger<LoginDb> _logger;
    IDbConnection _dbConn;
    readonly MySqlCompiler _compiler;
    readonly QueryFactory _queryFactory;

    public LoginDb(ILogger<LoginDb> logger, IOptions<DbConfig> dbConfig)
    {
        _logger = logger;
        _dbConfig = dbConfig;

        Open();

        _compiler = new MySqlCompiler();
        _queryFactory = new QueryFactory(_dbConn, _compiler);

    }



    public async Task<ErrorCode> CreateAccountAsync(string email, string password)
    {
        try
        {
            var saltValue = Security.SaltString();
            var hashingPassword = Security.MakeHashingPassWord(saltValue, password);

            var count = await _queryFactory.Query("account_info").InsertAsync(new LdbAccountInfo
            {
                player_id = 0,
                email = email,
                salt_value = saltValue,
                pw = hashingPassword,
                create_dt = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
            });
            // count = row affected

            _logger.ZLogInformation(
                $"[CreateAccount] email: {email}, salt_value : {saltValue}, hashed_pw:{hashingPassword}");

            return count != 1 ? ErrorCode.CreateAccountFailInsert : ErrorCode.None;
        }
        catch (Exception ex)
        {
            _logger.ZLogError(ex, $"[LoginDb.CreateAccount] ErrorCode : {ErrorCode.CreateAccountFailException}");
            return ErrorCode.CreateAccountFailException;
        }
    }
    public async Task<(ErrorCode, long)> VerifyUser(string email, string password)
    {
        try
        {
            LdbAccountInfo userInfo = await _queryFactory.Query("account_info")
                                    .Where("Email", email)
                                    .FirstOrDefaultAsync<LdbAccountInfo>();
            if (userInfo is null)
            {
                return (ErrorCode.LoginFailUserNotExist, 0);
            }

            var hashingPassword = Security.MakeHashingPassWord(userInfo.salt_value, password);
            // equal password check
            if (userInfo.pw != hashingPassword)
            {
                return (ErrorCode.LoginFailPwNotMatch, 0);
            }
            return (ErrorCode.None, userInfo.player_id);
        }
        catch (Exception e)
        {
            _logger.ZLogError(e, $"[LoginDb.VerifyUser] ErrorCode : {ErrorCode.LoginFailException}");
            return (ErrorCode.LoginFailException, 0);
        }
    }

    public void Dispose()
    {
        Close();
    }



    void Open()
    {
        _dbConn = new MySqlConnection(_dbConfig.Value.AccountDb); // mysql connector
        _dbConn.Open();
    }

    void Close()
    {
        _dbConn.Close();
    }
}

public class DbConfig
{
    public string AccountDb { get; set; }
}