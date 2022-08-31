using System;

namespace BettingApp.Services.Betting.Infrastructure.Idempotency
{
    public class ClientRequest
    {
        public string UserId { get; set; } // not used
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
    }
}
