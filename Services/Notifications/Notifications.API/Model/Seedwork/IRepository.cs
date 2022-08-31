using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Notifications.API.Model.Seedwork
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
