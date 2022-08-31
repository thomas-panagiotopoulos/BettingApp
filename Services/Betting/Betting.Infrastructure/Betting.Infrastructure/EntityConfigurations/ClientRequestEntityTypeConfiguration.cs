using BettingApp.Services.Betting.Infrastructure.Idempotency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Infrastructure.EntityConfigurations
{
    public class ClientRequestEntityTypeConfiguration : IEntityTypeConfiguration<ClientRequest>
    {
        public void Configure(EntityTypeBuilder<ClientRequest> requestConfiguration)
        {
            requestConfiguration.ToTable("requests", BettingContext.DEFAULT_SCHEMA);
            requestConfiguration.HasKey(cr => cr.Id);
            requestConfiguration.Property(cr => cr.Name).IsRequired();
            requestConfiguration.Property(cr => cr.Time).IsRequired();

            // We dont make the UserId necessary to avoid "breaking" the Idempotency mechanism in case 
            // a request could be created without being related to a User, so in these scenarios we leave it empty.
            //requestConfiguration.Property(cr => cr.UserId).IsRequired();
        }
    }
}
