using MatchAPIServer;

var builder = WebApplication.CreateBuilder(args);


IConfiguration configuration = builder.Configuration;
builder.Services.Configure<MatchingConfig>(configuration.GetSection(nameof(MatchingConfig)));

builder.Services.AddSingleton<IMatchWorker, MatchWorker>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapDefaultControllerRoute();

app.Run(configuration["ServerAddress"]);
