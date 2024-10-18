# Web Api gRPC
Web Api simples utilizada no estudo do gRPC.

## Comandos utilizados para criação dos projetos
### Solution
```csharp
dotnet new sln -n WebApiGrpc
```

### Servidor
```csharp
dotnet new grpc -n WebApiServer
dotnet sln add ./WebApiServer/
```

Após criar o arquivo .proto e referenciar no csproj, basta compilar para converter em classes.

### Client
```csharp
dotnet new console -n ClientGrpc
dotnet sln add ./ClientGrpc/
dotnet add package Grpc.Net.Client
dotnet add package Google.Protobuf
dotnet add package Grpc.Tools
```