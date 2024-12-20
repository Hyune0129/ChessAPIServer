using System.Text.Json;
using APIServer.DTO;
using APIServer.Repository.Interfaces;

namespace APIServer.Middleware;

public class VersionCheck
{
    readonly RequestDelegate _next;
    readonly ILogger<VersionCheck> _logger;
    readonly IMasterDb _masterDb;

    public VersionCheck(RequestDelegate next, ILogger<VersionCheck> logger, IMasterDb masterDb)
    {
        _next = next;
        _logger = logger;
        _masterDb = masterDb;
    }

    public async Task Invoke(HttpContext context)
    {
        var appVersion = context.Request.Headers["AppVersion"].ToString();
        var masterDataVersion = context.Request.Headers["MasterDataVersion"].ToString();

        if (!(await VersionCompare(appVersion, masterDataVersion, context)))
        {
            return;
        }

        // next middleware
        await _next(context);
    }

    async Task<bool> VersionCompare(string appVersion, string masterDataVersion, HttpContext context)
    {
        if (!appVersion.Equals(_masterDb._version!.app_version))
        {
            context.Response.StatusCode = StatusCodes.Status426UpgradeRequired;
            var errorJsonResponse = JsonSerializer.Serialize(new MiddlewareResponse
            {
                Result = ErrorCode.InvalidMasterDataVersion
            });
            await context.Response.WriteAsync(errorJsonResponse);
            return false;
        }
        return true;
    }
}