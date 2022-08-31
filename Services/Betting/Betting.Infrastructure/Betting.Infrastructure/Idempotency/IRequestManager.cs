using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Infrastructure.Idempotency
{
    public interface IRequestManager
    {
        Task<bool> ExistAsync(Guid id);
        Task CreateRequestForCommandAsync<T>(Guid id);

        Task<bool> ExistWithUserIdAsync(Guid id, string userId);
        Task CreateRequestForCommandWithUserIdAsync<T>(Guid id, string userId);
    }
}
