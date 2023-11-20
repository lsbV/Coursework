using DALTestsDB.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestLib;

namespace DALTestsDB.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasMany(x => x.Users).WithMany(x => x.Groups).UsingEntity<UserGroup>();


        }
    }
}
