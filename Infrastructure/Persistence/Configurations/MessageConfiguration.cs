using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations {

    public class MessageConfiguration : IEntityTypeConfiguration<Message> {

        public void Configure(EntityTypeBuilder<Message> builder) {
            builder.HasKey(x => x.Id);
        }
    }
}
