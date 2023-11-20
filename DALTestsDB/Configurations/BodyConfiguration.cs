using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TestLib.Abstractions;
using TestLib.Classes.Test;

namespace DALTestsDB.Configurations
{
    public class BodyConfiguration : IEntityTypeConfiguration<Body>
    {
        public void Configure(EntityTypeBuilder<Body> builder)
        {
            builder.UseTptMappingStrategy();

            builder
                .HasOne(body => body.Task)
                .WithOne(task => task.Body)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
