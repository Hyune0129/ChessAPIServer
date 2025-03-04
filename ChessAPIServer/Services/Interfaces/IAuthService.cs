namespace APIServer.Services.Interfaces;

public interface IAuthService
{
    Task<ErrorCode> DeleteUserToken(long userid);
    Task<ErrorCode> UpdateLastLoginTime(long uid);
    Task<ErrorCode> VerifyUser(long uid);
    Task<ErrorCode> VerifyTokenToLoginServer(long uid, string token);

    Task<ErrorCode> RegisterToken(long uid, string token);

}