using Grpc.Core;
using Grpc.Net.Client;
using Newtonsoft.Json;
using WebApiServer.Protos;

using GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5026");

Calculator.CalculatorClient client = new(channel);
Metadata headers = new()
{
    // Static api key
    //{ "x-api-key", "28f1c1dc-50f9-4b3c-ba2f-91684d3d10b6" }

    // Token JWT
    { "Authorization", GetJwtToken().Result }
};

ResultReply replySum = await client.SumAsync(new ValuesRequest { Value1 = 4, Value2 = 5 }, headers);
Console.WriteLine("Result: " + replySum.Result);

ResultReply replySubtract = await client.SubtractAsync(new ValuesRequest { Value1 = 4, Value2 = 5 }, headers);
Console.WriteLine("Result: " + replySubtract.Result);


static async Task<string> GetJwtToken()
{
    const string Realm = "";
    const string clientId = "";
    const string clientSecret = "";
    const string scope = ""; 

    HttpClient httpClient = new();
    httpClient.BaseAddress = new Uri($"http://localhost:8080/realms/{Realm}/protocol/openid-connect/");

    FormUrlEncodedContent content = new(new Dictionary<string, string>
    {
        { "grant_type", "client_credentials" },
        { "client_id", clientId },
        { "client_secret", clientSecret },
        { "scope", scope },
    });

    HttpResponseMessage response = await httpClient.PostAsync("token", content);
    string responseBodyJson = await response.Content.ReadAsStringAsync();
    
    dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(responseBodyJson);

    return $"{jsonObject.token_type} {jsonObject.access_token}";
}