using Grpc.Core;
using WebApiServer.Protos;

namespace WebApiServer.Services;

public class CalculatorService : Calculator.CalculatorBase
{
    public override Task<ResultReply> Sum(ValuesRequest request, ServerCallContext context)
    {
        return Task.FromResult(new ResultReply { Result = request.Value1 + request.Value2 });
    }

    public override Task<ResultReply> Subtract(ValuesRequest request, ServerCallContext context)
    {
        return Task.FromResult(new ResultReply { Result = request.Value1 - request.Value2 });
    }
}
