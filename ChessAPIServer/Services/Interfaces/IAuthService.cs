namespace APIServer.Services.Interfaces;

public interface IAuthService
{
    Task<ErrorCode> DeleteUserToken(long userid);
    Task<ErrorCode> Login(long userid, string token);

    Task<ErrorCode> UpdateLastLoginTime(long uid);
    Task<ErrorCode> VerifyUser(long uid);
    Task<(ErrorCode, string)> RegisterToken(long uid);

}