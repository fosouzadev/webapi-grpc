using Grpc.Core;
using Grpc.Net.Client;
using WebApiServer.Protos;

using GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5026");

Calculator.CalculatorClient client = new(channel);
Metadata headers = new()
{
    { "x-api-key", "28f1c1dc-50f9-4b3c-ba2f-91684d3d10b6" }
};

ResultReply replySum = await client.SumAsync(new ValuesRequest { Value1 = 4, Value2 = 5 }, headers);
Console.WriteLine("Result: " + replySum.Result);

ResultReply replySubtract = await client.SubtractAsync(new ValuesRequest { Value1 = 4, Value2 = 5 }, headers);
Console.WriteLine("Result: " + replySubtract.Result);