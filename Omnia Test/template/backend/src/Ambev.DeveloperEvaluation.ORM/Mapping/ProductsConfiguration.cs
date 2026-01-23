using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductsConfiguration : IEntityTypeConfiguration<Products>
{
    public void Configure(EntityTypeBuilder<Products> builder)
    {
        builder.ToTable("Products");

        // Primary Key
        builder.HasKey(p => p.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(p => p.Name)
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(p => p.Price)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(p => p.IsActive)
               .HasDefaultValue(true)
               .IsRequired();

        builder.Property(p => p.CreatedAt)
               .HasDefaultValueSql("now()")
               .IsRequired();

        builder.HasMany(p => p.SalesItems)
               .WithOne(si => si.Product)
               .HasForeignKey(si => si.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
