using BettingApp.Services.Betting.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Infrastructure.Idempotency
{
    public class RequestManager : IRequestManager
    {
        private readonly BettingContext _context;

        public RequestManager(BettingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<bool> ExistAsync(Guid id)
        {
            var request = await _context.FindAsync<ClientRequest>(id);

            return request != null;
        }

        public async Task CreateRequestForCommandAsync<T>(Guid id)
        {
            var exists = await ExistAsync(id);

            var request = exists ?
                throw new BettingDomainException($"Request with {id} already exists") :
                new ClientRequest()
                {
                    Id = id,
                    Name = typeof(T).Name,
                    Time = DateTime.UtcNow.AddHours(2)
                };

            _context.Add(request);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistWithUserIdAsync(Guid id, string userId)
        {
            var request = await _context.FindAsync<ClientRequest>(id);

            if (request != null && request.UserId != null)
            {
                return request.UserId.Equals(userId);
            }

            return false;
        }

        public async Task CreateRequestForCommandWithUserIdAsync<T>(Guid id, string userId)
        {
            var exists = await ExistAsync(id);

            var request = exists ?
                throw new BettingDomainException($"Request with {id} already exists") :
                new ClientRequest()
                {
                    UserId = userId,
                    Id = id,
                    Name = typeof(T).Name,
                    Time = DateTime.UtcNow.AddHours(2)
                };

            _context.Add(request);

            await _context.SaveChangesAsync();
        }
    }
}
