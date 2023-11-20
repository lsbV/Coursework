using DALTestsDB.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TestLib;
using TestLib.Classes.Test;

namespace DALTestsDB.Configurations
{
    public class TestAssignedConfiguration : IEntityTypeConfiguration<TestAssigned>
    {
        public void Configure(EntityTypeBuilder<TestAssigned> builder)
        {
            builder.HasMany(x => x.Users).WithMany(x => x.TestAssigneds).UsingEntity<TestAssignedUser>();

        }
    }
}
