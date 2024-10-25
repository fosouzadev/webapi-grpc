# Web Api gRPC
Web Api simples utilizada no estudo do gRPC.

## Autenticação
A aplicação tem disponível dois tipos de autenticação:
* Static Api Key: valor fixo validado via interceptor;
* Token JWT: token gerado pelo keycloak validado via interceptor.

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

## Container do Keycloak
Documentação para criar [container do Keycloak](https://www.keycloak.org/getting-started/getting-started-docker).

## Configurações do Keycloak
Crie um novo `Realm`.

Em `Clients`, crie um novo cliente para representar a aplicação. Defina as seguintes configurações:
* Informe um `Client ID`;
* Defina a chave `Client authentication` para `On`;
* Defina a chave `Authorization` para `On`;
* Desmarque as caixas de seleção `Standard flow` e `Direct access grants`.

Nas configurações do cliente, na aba `Client scopes`, faça as seguintes configurações:
* Exclua todos os escopos que são adicionados por padrão;
* Entre no escopo `{ClientName}-dedicated` e faça as seguintes configurações:
    * Adicione um novo `mapper` do tipo `By configuration` chamado `Audience`. Defina um nome e selecione sua aplicação no campo `Included Client Audience`, verifique se está marcado `On` para adicionar no token;

Nas configurações do cliente, na aba `Credentials`, salve o valor do `Client secret`, será necessário para gerar o token JWT.

## Gerar token JWT
Utilizando o [Postman](https://www.postman.com/downloads/), crie uma nova requisição do tipo `POST`.

Url: `http://localhost:8080/realms/{RealmName}/protocol/openid-connect/token`

Body: Selecione `x-www-form-urlencoded`, com as seguintes chaves:
* grant_type: `client_credentials`
* client_id: `{ClientName}`
* client_secret: `{ClientSecret}`

Utilizando o site [jwt.io](https://jwt.io/), é possível visualizar o conteúdo do token.

## Configurações da aplicação
No arquivo `appsettings.json`, informe os valores para `{RealmName}`, `{Audience}` e `{PublicKey}`.

No arquivo `Program.cs`, defina qual opção de autenticação irá utilizar na aplicação `Server` e `Client`.