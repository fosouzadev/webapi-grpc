# Web Api gRPC
Web Api simples utilizada no estudo do gRPC.

## Comandos utilizados para criação dos projetos
### Solution
```csharp
dotnet new sln -n WebApiGrpc
```

### Servidor gRPC
```csharp
dotnet new grpc -n WebApiServer
dotnet sln add ./WebApiServer/
dotnet add package Microsoft.IdentityModel.Tokens
dotnet add package System.IdentityModel.Tokens.Jwt
```

Após criar o arquivo .proto e referenciar no csproj, basta compilar para converter em classes.

### Client
```csharp
dotnet new console -n ClientGrpc
dotnet sln add ./ClientGrpc/
dotnet add package Grpc.Net.Client
dotnet add package Google.Protobuf
dotnet add package Grpc.Tools
dotnet add package Newtonsoft.json
```

### Autenticação
Realizei testes com dois tipos de autenticação:
* Static Api Key: valor fixo validado via interceptor;
* Token JWT: token gerado pelo keycloak validado via interceptor.

## Container do Keycloak
Utilizado o Keycloak como container no docker para gerar o token JWT. [Documentação completa.](https://www.keycloak.org/documentation)

Executar como container:
```
docker run -d -p 8080:8080 --name keycloak-server -e KC_BOOTSTRAP_ADMIN_USERNAME=admin -e KC_BOOTSTRAP_ADMIN_PASSWORD=admin quay.io/keycloak/keycloak:26.0.1 start-dev
```
# Autenticar e obter token de usuário
Siga essa [documentação](https://www.keycloak.org/getting-started/getting-started-docker) para gerenciar os usuários.

Url de login no realm criado, com o usuário criado
```
http://localhost:8080/realms/webapi-grpc/account
```

Url de teste de login no client criado
```
https://www.keycloak.org/app/
```