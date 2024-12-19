namespace APIServer.Services.Interfaces;

public interface IUserService
{
    Task<ErrorCode> ChangeNickname(long uid, string nickname);
    Task<ErrorCode> CreateNickname(long uid, string nickname);
}