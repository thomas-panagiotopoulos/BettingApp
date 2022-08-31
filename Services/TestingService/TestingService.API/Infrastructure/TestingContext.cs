using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.TestingService.API.Infrastructure
{
    public class TestingContext : DbContext
    {
        public TestingContext(DbContextOptions<TestingContext> options) : base(options)
        {

        }
    }
}
