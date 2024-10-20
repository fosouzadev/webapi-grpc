using WebApiServer.Interceptors;
using WebApiServer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc(options =>
{
    // Static api key
    //options.Interceptors.Add<StaticApiKeyInterceptor>();

    // Token JWT
    options.Interceptors.Add<KeycloakJwtTokenInterceptor>();
});

var app = builder.Build();

app.UseRouting();

app.MapGrpcService<GreeterService>();
app.MapGrpcService<CalculatorService>();

app.Run();