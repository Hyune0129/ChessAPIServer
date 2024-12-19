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
    public async Task<ErrorCode> DeleteUserToken(Int64 userid)
    {
        return await _memoryDb.DeleteUserAuthAsync(userid);
    }

    /// <summary>
    ///  verify token from login server
    /// lastlogin time update
    /// if new user response ErrorCode 2005
    /// </summary>
    public async Task<ErrorCode> Login(long userid, string token)
    {
        ErrorCode errorCode = await VerifyUser(userid);
        if (errorCode != ErrorCode.None)
        {
            return errorCode;
        }
        errorCode = await VerifyTokenToLoginServer(userid, token);
        if (errorCode != ErrorCode.None)
        {
            return errorCode;
        }
        errorCode = await UpdateLastLoginTime(userid);
        if (errorCode != ErrorCode.None)
        {
            return errorCode;
        }

        return ErrorCode.None;
    }

    public async Task<ErrorCode> VerifyTokenToLoginServer(long uid, string token)
    {
        try
        {
            HttpClient client = new();
            var loginServerResponse = await client.PostAsJsonAsync(_loginServerAPIAddress, new { PlayerId = uid, Token = token });

            if (!ValidateLoginServerResponse(loginServerResponse))
            {
                return ErrorCode.LoginServer_Fail_InvalidResponse;
            }

            var authResult = await loginServerResponse.Content.ReadFromJsonAsync<ErrorCodeDTO>();
            if (!ValidateLoginServerAuthErrorCode(authResult))
            {
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
                _logger.ZLogError($"[UpdateLastLoginTime] ErrorCode: {ErrorCode.LoginUpdateRecentLoginFail}, count : {count}");
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

    public async Task<(ErrorCode, string)> RegisterToken(long uid)
    {
        throw new NotImplementedException();
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