using APIServer.Repository;
using APIServer.Repository.Interfaces;
using APIServer.Services;
using APIServer.Services.Friend;
using APIServer.Services.Interfaces;
using ZLogger;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.Configure<DbConfig>(configuration.GetSection(nameof(DbConfig)));

builder.Services.AddTransient<IGameDb, GameDb>();
builder.Services.AddSingleton<IMemoryDb, MemoryDb>();
builder.Services.AddSingleton<IMasterDb, MasterDb>();


builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IDataLoadService, DataLoadService>();
builder.Services.AddTransient<IGameService, GameService>();
builder.Services.AddTransient<IFriendService, FriendService>();
builder.Services.AddControllers();

SettingLogger();

WebApplication app = builder.Build();


if (!await app.Services.GetService<IMasterDb>().Load())
{
    return;
}


// logger setting
ILoggerFactory loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();

app.UseMiddleware<APIServer.Middleware.VersionCheck>();
app.UseMiddleware<APIServer.Middleware.CheckUserAuthAndLoadUserData>();

app.UseRouting();

app.MapDefaultControllerRoute();

// todo : IMasterDb load


app.Run(configuration["ServerAddress"]);


void SettingLogger()
{
    ILoggingBuilder logging = builder.Logging;
    logging.ClearProviders();

    var fileDir = configuration["logdir"];

    var exists = Directory.Exists(fileDir);

    if (exists)
    {
        Directory.CreateDirectory(fileDir);
    }

    logging.AddZLoggerRollingFile(
        options =>
        {
            options.UseJsonFormatter();
            options.FilePathSelector = (timestamp, sequenceNumber) => $"{fileDir}{timestamp.ToLocalTime():yyyy-MM-dd}_{sequenceNumber:000}.log";
            options.RollingInterval = ZLogger.Providers.RollingInterval.Day;
            options.RollingSizeKB = 1024;
        });

    _ = logging.AddZLoggerConsole(options =>
    {
        options.UseJsonFormatter();
    });

}