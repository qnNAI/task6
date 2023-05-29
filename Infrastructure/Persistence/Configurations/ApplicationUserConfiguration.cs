using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations {
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser> {

        public void Configure(EntityTypeBuilder<ApplicationUser> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Username).IsRequired();
            builder.HasMany(x => x.SentMessages)
                .WithOne(x => x.Sender)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.ReceivedMessages)
                .WithOne(x => x.Recipient)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(x => x.Username).IsUnique();
        }
    }
}
