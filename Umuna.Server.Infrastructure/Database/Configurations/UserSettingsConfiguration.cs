using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Umuna.Server.Domain.Entities;

namespace Umuna.Server.Infrastructure.Database.Configurations
{
    public class UserSettingsConfiguration : IEntityTypeConfiguration<UserSettings>
    {
        public void Configure(EntityTypeBuilder<UserSettings> builder)
        {
            builder.HasKey(us => us.Id);
            
            builder.Property(us => us.CameraRotationSpeed)
                .IsRequired();
            
            builder.Property(us => us.CameraTranslationSpeed)
                .IsRequired();
            
            builder.Property(us => us.CameraZoomSpeed)
                .IsRequired();
            
            builder.Property(us => us.UpdatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETUTCDATE()");
            
            // Foreign key relationship with User
            builder.HasOne(us => us.User)
                .WithOne(u => u.Settings)
                .HasForeignKey<UserSettings>(us => us.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Enforce one-to-one constraint at database level
            builder.HasIndex(us => us.UserId)
                .IsUnique();
        }
    }
}
