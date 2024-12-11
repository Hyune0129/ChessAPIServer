using APIServer.Model.DTO;
using APIServer.Repository;
using Microsoft.AspNetCore.Mvc;
using ZLogger;

namespace APIServer.Controllers;


[ApiController]
[Route("[controller]")]
public class CreateAccount : ControllerBase
{
    readonly ILogger<CreateAccount> _logger;
    readonly ILoginDb _loginDb;

    public CreateAccount(ILoginDb loginDb, ILogger<CreateAccount> logger)
    {
        _loginDb = loginDb;
        _logger = logger;
    }

    [HttpPost]
    public async Task<CreateAccountResponse> AccountCreate(CreateAccountRequest request)
    {
        CreateAccountResponse response = new();

        response.Result = await _loginDb.CreateAccountAsync(request.UserID, request.Password);
        _logger.ZLogInformation($"[create account] userid:{request.UserID}");
        return response;
    }
}