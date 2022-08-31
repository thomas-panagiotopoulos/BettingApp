using BettingApp.Services.Betting.Infrastructure.Idempotency;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.Commands
{
    public class IdentifiedCommandHandler<T, R> : IRequestHandler<IdentifiedCommand<T, R>, R>
        where T : IRequest<R>
    {
        private readonly ILogger<IdentifiedCommandHandler<T, R>> _logger;
        private readonly IRequestManager _requestManager;
        private readonly IMediator _mediator;

        public IdentifiedCommandHandler(ILogger<IdentifiedCommandHandler<T, R>> logger,
                                        IRequestManager requestManager,
                                        IMediator mediator)
        {
            _logger = logger;
            _requestManager = requestManager;
            _mediator = mediator;
        }

        /// <summary>
        /// Creates the result value to return if a previous request was found
        /// </summary>
        /// <returns></returns>
        protected virtual R CreateResultForDuplicateRequest()
        {
            return default(R);
        }

        public async Task<R> Handle(IdentifiedCommand<T, R> message, CancellationToken cancellationToken)
        {
            var alreadyExists = await _requestManager.ExistAsync(message.Id);

            if (alreadyExists)
            {
                _logger.LogInformation($"IdempotentCommandHandler: Command already exists. " +
                                        $"CommandType: {typeof(T).Name}, CommandId: {message.Id}");

                return CreateResultForDuplicateRequest();
            }
            else
            {
                try
                {
                    _logger.LogInformation($"IdempotentCommandHandler: Create request for command and the send it. " +
                                        $"CommandType: {typeof(T).Name}, CommandId: {message.Id}");

                    await _requestManager.CreateRequestForCommandAsync<T>(message.Id);

                    // ALterantively, we can include the UsedId in the created ClientRequest in order to relate the
                    // request with the User who initiated the command.
                    //await _requestManager.CreateRequestForCommandWithUserIdAsync<T>(message.Id, message.UserId);

                    // Send the embeded business command to mediator so it runs its related CommandHandler 
                    var result = await _mediator.Send(message.Command, cancellationToken);

                    return result;
                }
                catch
                {
                    return default(R);
                }
            }
        }
    }
}
