using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.Commands
{
    public class IdentifiedCommand<T, R> : IRequest<R>
        where T : IRequest<R>
    {
        public T Command { get; }
        public Guid Id { get; }
        public string UserId { get; }
        public IdentifiedCommand(T command, Guid id)
        {
            Command = command;
            Id = id;
        }
        public IdentifiedCommand(T command, Guid id, string userId)
        {
            Command = command;
            Id = id;
            UserId = userId;
        }
    }
}
