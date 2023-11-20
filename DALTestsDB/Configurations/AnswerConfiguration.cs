using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TestLib.Abstractions;

namespace DALTestsDB.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.UseTptMappingStrategy();
            //builder.HasOne(answer => answer.Task).WithMany(task => task.Answers).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
