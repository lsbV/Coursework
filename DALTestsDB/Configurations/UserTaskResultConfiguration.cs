using DALTestsDB.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DALTestsDB.Configurations
{
    public class UserTaskResultConfiguration : IEntityTypeConfiguration<UserTaskResult>
    {
        public void Configure(EntityTypeBuilder<UserTaskResult> builder)
        {
            builder
               .HasOne(x => x.UserTestResult)
               .WithMany(x => x.UserTaskResults)
               .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(t => t.UserAnswerResults)
                .WithOne(t => t.UserTaskResult)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
