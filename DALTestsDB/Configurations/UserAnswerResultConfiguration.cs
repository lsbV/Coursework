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
    public class UserAnswerResultConfiguration : IEntityTypeConfiguration<UserAnswerResult>
    {
        public void Configure(EntityTypeBuilder<UserAnswerResult> builder)
        {
            builder
               .HasOne(t => t.UserTaskResult)
               .WithMany(t => t.UserAnswerResults)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
