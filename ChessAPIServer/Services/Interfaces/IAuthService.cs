namespace APIServer.Services.Interfaces;

public interface IAuthService
{
    Task<ErrorCode> DeleteUserToken(long uid);
    Task<ErrorCode> UpdateLastLoginTime(long uid);
    Task<ErrorCode> VerifyUser(long uid);
    Task<ErrorCode> VerifyTokenToLoginServer(long player_id, string token);

    Task<ErrorCode> RegisterToken(long uid, string token);

}