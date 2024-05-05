using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull

    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle request={Request} - Response  ={Response} - RequestData={RequestData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = new Stopwatch();
            timer.Start();

            var response = await next(); 

            timer.Stop();
            var timeToken = timer.Elapsed;
            if (timeToken.Seconds > 3)
                logger.LogInformation("[PERFORMANC] The Request {request} took {TimeTaken}",
                    typeof(TRequest).Name, timeToken.Seconds);

            return response;
        }
    }
}
