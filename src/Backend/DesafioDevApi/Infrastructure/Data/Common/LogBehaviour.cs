using MediatR;
using Serilog;

namespace DesafioDevApi.Infrastructure.Data.Common
{
    public class LogBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse? response = await next();

            Log.Information(System.Text.Json.JsonSerializer.Serialize(new
            {
                CommandName = request.GetType().ToString(),
                CommandRequest = request,
                CommandResponse = response
            }));

            return response;
        }
    }
}
