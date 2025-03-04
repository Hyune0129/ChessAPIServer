using APIServer.Model;
using APIServer.Model.DTO;
using APIServer.Repository;
using APIServer.Utils;
using Microsoft.AspNetCore.Mvc;
using ZLogger;

namespace APIServer.Controllers;

[ApiController]
[Route("[controller]")]
public class VerifyToken
{
    readonly ILogger<VerifyToken> _logger;
    readonly ILoginDb _loginDb;
    readonly string _saltValue;

    public VerifyToken(ILogger<VerifyToken> logger, ILoginDb loginDb, IConfiguration config)
    {
        _logger = logger;
        _loginDb = loginDb;
        _saltValue = config.GetSection("TokenSaltValue").Value;
    }

    [HttpPost]
    public VerifyTokenResponse Verify(VerifyTokenRequest request)
    {
        VerifyTokenResponse response = new();
        if (Security.MakeHashingToken(_saltValue, request.PlayerId) != request.Token)
        {
            _logger.ZLogDebug(
                $"[AccountDb.VerifyToken] ErrorCode: {ErrorCode.VerifyTokenFail}");
            response.Result = ErrorCode.VerifyTokenFail;
        }
        return response;
    }

}
