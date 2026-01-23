using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SalesConfiguration : IEntityTypeConfiguration<Sales>
{
    public void Configure(EntityTypeBuilder<Sales> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(s => s.Id);
        builder.Property(u => u.Id).IsRequired().HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.UserId)
               .IsRequired();

        builder.Property(s => s.BranchId)
               .IsRequired();

        builder.Property(s => s.Total)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(s => s.IsActive)
               .HasDefaultValue(true)
               .IsRequired();

        builder.Property(s => s.CreatedAt)
               .HasDefaultValueSql("now()")
               .IsRequired();

        builder.HasIndex(s => s.UserId);
        builder.HasIndex(s => s.BranchId);

        builder.HasOne(s => s.User)
               .WithMany(b => b.Sales)
               .HasForeignKey(s => s.UserId);

        builder.HasOne(s => s.Branch)
               .WithMany(b => b.Sales)
               .HasForeignKey(s => s.BranchId);

        builder.HasMany(s => s.SalesItems)
               .WithOne(si => si.Sales)
               .HasForeignKey(si => si.SalesId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

