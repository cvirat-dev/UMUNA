using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Umuna.Server.Domain.Entities;

namespace Umuna.Server.Infrastructure.Database.Configurations
{
    public class CameraPositionConfiguration : IEntityTypeConfiguration<CameraPosition>
    {
        public void Configure(EntityTypeBuilder<CameraPosition> builder)
        {
            builder.HasKey(cp => cp.Id);
            
            builder.Property(cp => cp.PositionX)
                .IsRequired();
            
            builder.Property(cp => cp.PositionY)
                .IsRequired();
            
            builder.Property(cp => cp.PositionZ)
                .IsRequired();
            
            builder.Property(cp => cp.RotationX)
                .IsRequired();
            
            builder.Property(cp => cp.RotationY)
                .IsRequired();
            
            builder.Property(cp => cp.RotationZ)
                .IsRequired();
            
            builder.Property(cp => cp.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETUTCDATE()");

            // Foreign key relationship with User
            builder.HasOne(cp => cp.User)
                .WithMany(u => u.CameraPositions)
                .HasForeignKey(cp => cp.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
