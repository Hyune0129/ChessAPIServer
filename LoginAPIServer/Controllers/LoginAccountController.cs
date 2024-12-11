using APIServer.Model.DTO;
using APIServer.Repository;
using APIServer.Utils;
using Microsoft.AspNetCore.Mvc;
using ZLogger;

namespace APIServer.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginAccount : ControllerBase
{
    readonly string _saltValue;
    readonly ILogger<LoginAccount> _logger;
    readonly ILoginDb _loginDb;

    public LoginAccount(ILogger<LoginAccount> logger, ILoginDb loginDb, IConfiguration configuration)
    {
        _saltValue = configuration.GetSection("TokenSaltValue").Value;
        _logger = logger;
        _loginDb = loginDb;
    }

    [HttpPost]
    public async Task<LoginAccountResponse> Login(LoginAccountRequest request)
    {
        LoginAccountResponse response = new();
        (response.Result, response.UserID) = await _loginDb.VerifyUser(request.UserID, request.Password);
        _logger.ZLogInformation($"[Login] UserId : {request.UserID}");
        response.Token = Security.MakeHashingToken(_saltValue, response.UserID);
        return response;

    }


}