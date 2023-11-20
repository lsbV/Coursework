using DALTestsDB.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DALTestsDB.Configurations
{
    public class UserTestResultConfiguration : IEntityTypeConfiguration<UserTestResult>
    {
        public void Configure(EntityTypeBuilder<UserTestResult> builder)
        {
            builder
                .HasMany(t => t.UserTaskResults)
                .WithOne(t => t.UserTestResult)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.TestAssignedUser)
                .WithOne()
                .HasForeignKey<UserTestResult>(x => x.TestAssignedUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
