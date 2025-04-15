using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SchoolApi.Models
{
    public class ResultSheetConfiguration : IEntityTypeConfiguration<ResultSheet>
    {
        public void Configure(EntityTypeBuilder<ResultSheet> builder)
        {
            builder.ToTable("ResultSheets");
            builder.Property(p => p.Id).HasColumnName(nameof(ResultSheet.Id));
            builder.Property(p => p.Name).HasColumnName(nameof(ResultSheet.Name)).HasMaxLength(64).IsRequired();
            builder.Property(p => p.StudentClassRecordId).HasColumnName(nameof(ResultSheet.StudentClassRecordId));
            builder.HasOne(c => c.StudentClassRecord)
                .WithMany(c => c.ResultSheets)
                .HasForeignKey(c => c.StudentClassRecordId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
