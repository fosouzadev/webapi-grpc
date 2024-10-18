using WebApiServer.Interceptors;
using WebApiServer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<StaticApiKeyInterceptor>();
});

var app = builder.Build();

app.UseRouting();

app.MapGrpcService<GreeterService>();
app.MapGrpcService<CalculatorService>();

app.Run();