using APIServer.Model;

namespace APIServer.Repository;

public interface ILoginDb : IDisposable
{
    Task<ErrorCode> CreateAccountAsync(string userid, string password);
    Task<(ErrorCode, long)> VerifyUser(string userid, string password); // email -> user db id
}