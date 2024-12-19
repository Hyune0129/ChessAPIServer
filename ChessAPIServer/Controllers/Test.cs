using APIServer.DTO;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class Hello : ControllerBase
{
    [HttpGet("/hello")]
    public HelloResponse HelloGet()
    {
        HelloResponse response = new();
        response.Result = ErrorCode.None;
        response.data = "[HelloGet] hello world!";
        return response;

    }

    [HttpPost("/hello")]
    public HelloResponse HelloPost(HelloRequest request)
    {
        HelloResponse response = new();
        response.Result = ErrorCode.None;
        response.data = $"[HelloPost] hello! text : {request.text}";
        return response;
    }
}

public class HelloResponse : ErrorCodeDTO
{
    public string data { get; set; }
}

public class HelloRequest
{
    public string text { get; set; }
}