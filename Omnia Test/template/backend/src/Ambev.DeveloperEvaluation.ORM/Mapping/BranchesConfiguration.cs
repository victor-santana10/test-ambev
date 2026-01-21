using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BranchesConfiguration : IEntityTypeConfiguration<Branches>
{
    public void Configure(EntityTypeBuilder<Branches> builder)
    {
        builder.ToTable("Branches");

        builder.HasKey(b => b.Id);
        builder.Property(u => u.Id).IsRequired().HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(b => b.Name)
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(b => b.IsActive)
               .HasDefaultValue(true)
               .IsRequired();

        builder.Property(b => b.CreatedAt)
               .HasDefaultValueSql("now()")
               .IsRequired();

        builder.HasMany(b => b.Sales)
               .WithOne(s => s.Branch)
               .HasForeignKey(s => s.BranchId);
    }
}
