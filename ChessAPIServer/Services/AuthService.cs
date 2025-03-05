using APIServer.DTO;
using APIServer.Models.GameDB;
using APIServer.Repository.Interfaces;
using APIServer.Services.Interfaces;
using ZLogger;

namespace APIServer.Services;

public class AuthService : IAuthService
{
    readonly ILogger<AuthService> _logger;
    readonly IMemoryDb _memoryDb;
    readonly IGameDb _gameDb;
    string _loginServerAPIAddress;

    public AuthService(IMemoryDb memoryDb, ILogger<AuthService> logger, IConfiguration configuration, IGameDb gameDb)
    {
        _memoryDb = memoryDb;
        _logger = logger;
        _loginServerAPIAddress = configuration.GetSection("AccountServerAddress").Value + configuration.GetSection("AccountServerVerifyTokenAPI").Value;
        _gameDb = gameDb;
    }
    public async Task<ErrorCode> DeleteUserToken(long uid)
    {
        return await _memoryDb.DeleteUserAuthAsync(uid);
    }

    /// <summary>
    ///  verify token from login server
    /// lastlogin time update
    /// if new user response ErrorCode 2005
    /// </summary>
    public async Task<ErrorCode> VerifyTokenToLoginServer(long player_id, string token)
    {
        try
        {
            HttpClient client = new();
            var loginServerResponse = await client.PostAsJsonAsync(_loginServerAPIAddress, new { PlayerId = player_id, Token = token });

            if (!ValidateLoginServerResponse(loginServerResponse))
            {
                _logger.ZLogDebug($"[VerifyTokenToLoginServer] ErrorCode:{ErrorCode.LoginServer_Fail_InvalidResponse} ");
                return ErrorCode.LoginServer_Fail_InvalidResponse;
            }

            return ErrorCode.None;
        }
        catch (Exception ex)
        {
            _logger.ZLogError(ex, $"[VerifyTokenToLoginServer] ErrorCode:{ErrorCode.LoginServer_Fail_InvalidResponse} ");
            return ErrorCode.LoginServer_Fail_InvalidResponse_Exception;
        }
    }

    public async Task<ErrorCode> UpdateLastLoginTime(long uid)
    {
        try
        {
            var count = await _gameDb.UpdateLastLoginTime(uid);

            if (count != 1)
            {
                _logger.ZLogDebug($"[UpdateLastLoginTime] ErrorCode: {ErrorCode.LoginUpdateRecentLoginFail}, count : {count}");
                return ErrorCode.LoginUpdateRecentLoginFail;
            }

        }
        catch (Exception ex)
        {
            _logger.ZLogError(ex,
            $"[UpdateLastLoginTime] ErrorCode : {ErrorCode.LoginUpdateRecentLoginFailException}, uid : {uid}");
            return ErrorCode.LoginUpdateRecentLoginFailException;
        }
        return ErrorCode.None;
    }

    public async Task<ErrorCode> VerifyUser(long uid)
    {
        try
        {
            GdbUserInfo userInfo = await _gameDb.GetUserByUid(uid);
            if (userInfo is null)
            {
                return ErrorCode.LoginFailUserNotExist;
            }

            return ErrorCode.None;
        }
        catch (Exception e)
        {
            _logger.ZLogError(e,
            $"[VerifyUUser] ErrorCode: {ErrorCode.LoginFailException}, uid: {uid}");
            return ErrorCode.LoginFailException;
        }
    }

    /// <summary>
    /// register user's token in memorydb(redis)
    /// </summary>
    public async Task<ErrorCode> RegisterToken(long uid, string token)
    {

        ErrorCode errorCode = await _memoryDb.RegisterUserAuthAsync(token, uid);
        return errorCode;
    }

    bool ValidateLoginServerResponse(HttpResponseMessage? response)
    {
        if (response is null)
        {
            return false;
        }
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            return false;
        }
        return true;
    }

    bool ValidateLoginServerAuthErrorCode(ErrorCodeDTO? authResult)
    {
        if (authResult == null || authResult.Result != ErrorCode.None)
        {
            return false;
        }

        return true;
    }

}