using System.Text.Json;
using APIServer.DTO;
using APIServer.Models;
using APIServer.Repository;
using APIServer.Repository.Interfaces;

namespace APIServer.Middleware;

public class CheckUserAuthAndLoadUserData
{
    readonly IMemoryDb _memoryDb;
    readonly RequestDelegate _next; // middleware next

    public CheckUserAuthAndLoadUserData(RequestDelegate next, IMemoryDb memoryDb)
    {
        _next = next;
        _memoryDb = memoryDb;
    }

    public async Task Invoke(HttpContext context)
    {
        var formString = context.Request.Path.Value;

        // login, register dont check token
        if (string.Compare(formString, "/Login", StringComparison.OrdinalIgnoreCase) == 0 ||
        string.Compare(formString, "/UserCreate", StringComparison.OrdinalIgnoreCase) == 0)
        {
            await _next(context);

            return;
        }

        // hello controller check
        // todo : delete it if released
        if (string.Compare(formString, "/hello", StringComparison.OrdinalIgnoreCase) == 0)
        {
            await _next(context);

            return;
        }

        //have token?
        var (isTokenNotExist, token) = await IsTokenNotExistOrReturnToken(context);
        if (isTokenNotExist)
        {
            return;
        }

        //have uid?
        var (isUidNotExist, uid) = await IsUidNotExistOrReturnUid(context);
        if (isUidNotExist)
        {
            return;
        }

        // not data with uid key
        (bool isOk, RdbAuthUserData userInfo) = await _memoryDb.GetUserAsync(uid);
        if (await IsInvalidUserAuthTokenNotFound(context, isOk))
        {
            return;
        }

        if (await IsInvalidUserAuthTokenThenSendError(context, userInfo, token))
        {
            return;
        }

        // redis key lock check
        var userLockKey = MemoryDbKeyMaker.MakeUserLockKey(userInfo.Uid.ToString());
        if (await SetLockAndIsFailThenSendError(context, userLockKey))
        {
            return;
        }


        context.Items[nameof(RdbAuthUserData)] = userInfo;

        // call next middleware
        await _next(context);

        // unlock transaction key
        await _memoryDb.UnLockUserReqAsync(userLockKey);
    }

    async Task<(bool, string)> IsTokenNotExistOrReturnToken(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("token", out var token))
        {
            return (false, token);
        }

        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        var errorJsonResponse = JsonSerializer.Serialize(new MiddlewareResponse
        {
            Result = ErrorCode.TokenDoesNotExist
        });

        await context.Response.WriteAsync(errorJsonResponse);

        return (true, "");
    }

    async Task<(bool, string)> IsUidNotExistOrReturnUid(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("uid", out var uid)) // header have uid?
        {
            return (false, uid);
        }

        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        var errorJsonResponse = JsonSerializer.Serialize(new MiddlewareResponse
        {
            Result = ErrorCode.UidDoesNotExist
        });
        await context.Response.WriteAsJsonAsync(errorJsonResponse);
        return (true, "");
    }

    async Task<bool> SetLockAndIsFailThenSendError(HttpContext context, string authToken)
    {
        if (await _memoryDb.LockUserReqAsync(authToken))
        {
            return false;
        }

        context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        var errorJsonResponse = JsonSerializer.Serialize(new MiddlewareResponse
        {
            Result = ErrorCode.AuthTokenFailSetNx
        });
        await context.Response.WriteAsJsonAsync(errorJsonResponse);
        return true;
    }

    async Task<bool> IsInvalidUserAuthTokenThenSendError(HttpContext context, RdbAuthUserData userInfo, string token)
    {
        if (string.CompareOrdinal(userInfo.Token, token) == 0)
        {
            return false;
        }

        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        var errorJsonResponse = JsonSerializer.Serialize(new MiddlewareResponse
        {
            Result = ErrorCode.AuthTokenFailWrongAuthToken
        });
        await context.Response.WriteAsJsonAsync(errorJsonResponse);

        return true;
    }

    async Task<bool> IsInvalidUserAuthTokenNotFound(HttpContext context, bool isOk)
    {
        if (!isOk)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            var errorJsonResponse = JsonSerializer.Serialize(new MiddlewareResponse
            {
                Result = ErrorCode.AuthTokenKeyNotFound
            });
            await context.Response.WriteAsJsonAsync(errorJsonResponse);
        }
        return !isOk;
    }
}