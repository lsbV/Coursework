using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TestLib.Classes.Test;

namespace DALTestsDB.Configurations
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder
              .HasMany(t => t.Tasks)
              .WithOne(task => task.Test)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
