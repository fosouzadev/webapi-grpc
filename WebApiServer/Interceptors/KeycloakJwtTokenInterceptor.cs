using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.IdentityModel.Tokens;

namespace WebApiServer.Interceptors;

public class KeycloakJwtTokenInterceptor(IConfiguration configuration) : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
    {
        string token = GetTokenFromHeader(context.RequestHeaders);

        ValidateToken(token);

        return await continuation(request, context);
    }

    private static string GetTokenFromHeader(Metadata headers)
    {
        string authorizationHeaderValue = headers.FirstOrDefault(h => h.Key.ToLower() == "authorization")?.Value;

        if (authorizationHeaderValue is null || authorizationHeaderValue.StartsWith("Bearer ") == false)
            return null;

        return authorizationHeaderValue["Bearer ".Length..];
    }

    private void ValidateToken(string token)
    {
        if (token == null)
            throw new RpcException(new Status(StatusCode.Unauthenticated, "Invalid JWT Token"));

        try
        {
            TokenValidationParameters validationParameters = new()
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["Auth:Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["Auth:Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Auth:Jwt:SecretKey"])),
                ValidateLifetime = true
            };

            new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out SecurityToken validatedToken);
        }
        catch(Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Unauthenticated, ex.Message));
        }
    }
}