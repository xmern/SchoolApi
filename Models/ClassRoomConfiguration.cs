using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SchoolApi.Models
{
    public class ClassRoomConfiguration : IEntityTypeConfiguration<ClassRoom>
    {
        public void Configure(EntityTypeBuilder<ClassRoom> builder)
        {
            builder.ToTable("ClassRooms");
            builder.Property(p=>p.Id).HasColumnName(nameof(ClassRoom.Id));
            builder.Property(p => p.Name).HasColumnName(nameof(ClassRoom.Name)).HasMaxLength(64).IsRequired();
            builder.Property(p => p.ClassId).HasColumnName(nameof(ClassRoom.ClassId));
            builder.HasOne(c=>c.Class)
                .WithMany(c=>c.ClassRooms)
                .HasForeignKey(c => c.ClassId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
