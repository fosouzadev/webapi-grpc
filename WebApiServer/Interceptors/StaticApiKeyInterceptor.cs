using Grpc.Core;
using Grpc.Core.Interceptors;

namespace WebApiServer.Interceptors;

public class StaticApiKeyInterceptor(IConfiguration configuration) : Interceptor
{
    private readonly string _apiKey = configuration["Auth:StaticApiKey"];

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        if (context.RequestHeaders.Any(header => 
            header.Key == configuration["Auth:Static:HeaderName"] && 
            header.Value == configuration["Auth:Static:ApiKey"]) == false)
        {
            throw new RpcException(new Status(StatusCode.Unauthenticated, "Invalid API Key"));
        }

        return await continuation(request, context);
    }
}