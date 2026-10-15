using Microsoft.EntityFrameworkCore;
using StyleWardrobe.Api.Models;

namespace StyleWardrobe.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Clothes> Clothes => Set<Clothes>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(u => u.Id);

            entity.Property(u => u.FirebaseUid)
                .IsRequired()
                .HasMaxLength(128);

            entity.HasIndex(u => u.FirebaseUid)
                .IsUnique();

            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            entity.Property(u => u.DisplayName)
                .HasMaxLength(100);

            entity.Property(u => u.AvatarUrl)
                .HasMaxLength(500);

            entity.Property(u => u.CreatedAt)
                .IsRequired();
        });

        modelBuilder.Entity<Clothes>(entity =>
        {
            entity.ToTable("Clothes");
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(c => c.Type)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(c => c.Color)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(c => c.Style)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(c => c.ImageUrl)
                .HasMaxLength(500);

            entity.Property(c => c.CreatedAt)
                .IsRequired();

            entity.HasOne(c => c.User)
                .WithMany(u => u.Clothes)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(c => c.UserId);
        });
    }
}
