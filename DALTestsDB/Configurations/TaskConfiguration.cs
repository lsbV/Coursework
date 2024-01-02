using DALTestsDB.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TestLib.Abstractions;
using Task = TestLib.Abstractions.Task;

namespace DALTestsDB.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.UseTptMappingStrategy();
            builder.Ignore(t => t.Description);

            builder
               .HasMany(task => task.Answers)
               .WithOne(answer => answer.Task)
               .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasOne(task => task.Body)
                .WithOne(body => body.Task)
                .HasForeignKey<Body>(Body => Body.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany<UserTaskResult>().WithOne(x => x.Task).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
