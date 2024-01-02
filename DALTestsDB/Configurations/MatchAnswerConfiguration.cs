using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib.Abstractions;
using TestLib.Classes.Answers;

namespace DALTestsDB.Configurations
{
    public class MatchAnswerConfiguration : IEntityTypeConfiguration<MatchAnswer>
    {
        public void Configure(EntityTypeBuilder<MatchAnswer> builder)
        {
            builder.HasBaseType<Answer>();

            builder.Property(e => e.Side).IsRequired();

            builder.HasOne(ma => ma.Partner)
               .WithOne()
               .HasForeignKey<MatchAnswer>(ma => ma.PartnerId)
               .IsRequired(false);
        }
    }
}
