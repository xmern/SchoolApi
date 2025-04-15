using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SchoolApi.Models
{
    public class SubjectRecordConfiguration : IEntityTypeConfiguration<SubjectRecord>
    {
        public void Configure(EntityTypeBuilder<SubjectRecord> builder)
        {
            builder.ToTable("SubjectRecords");
            builder.Property(p => p.Id).HasColumnName(nameof(SubjectRecord.Id));
            builder.Property(p => p.Name).HasColumnName(nameof(SubjectRecord.Name)).HasMaxLength(64).IsRequired();
            builder.Property(p => p.FirstCA).HasColumnName(nameof(SubjectRecord.FirstCA));
            builder.Property(p => p.SecondCA).HasColumnName(nameof(SubjectRecord.SecondCA));
            builder.Property(p => p.ExamScore).HasColumnName(nameof(SubjectRecord.ExamScore));
            builder.Property(p => p.ResultSheetId).HasColumnName(nameof(SubjectRecord.ResultSheetId));
            builder.HasOne(c => c.ResultSheet)
                .WithMany(c => c.SubjectRecords)
                .HasForeignKey(c => c.ResultSheetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
