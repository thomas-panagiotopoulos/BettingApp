using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Payments.API
{
    public class PaymentsSettings
    {
        public bool TopUpAccepted { get; set; }
        public bool WithdrawAccepted { get; set; }

        public string EventBusConnection { get; set; }
    }
}
