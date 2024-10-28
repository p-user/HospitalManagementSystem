using MediatR;
using Microsoft.Extensions.Logging;

namespace Shared.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull , IRequest<TResponse>
        where TResponse : notnull
        //Logging all requests
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle request= {Request}- Response={Response} - RequestData={RequestData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var response = await next();

            logger.LogInformation("[END] HandleD  {Request} with {Response}", typeof(TRequest).Name, typeof(TResponse).Name, request);
            return response;

        }
    }
}
