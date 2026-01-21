using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SalesItemsConfiguration : IEntityTypeConfiguration<SalesItems>
{
    public void Configure(EntityTypeBuilder<SalesItems> builder)
    {
        builder.ToTable("SaleItems");

        builder.HasKey(si => si.Id);
        builder.Property(u => u.Id).IsRequired().HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(si => si.SalesId)
               .IsRequired();

        builder.Property(si => si.ProductId)
               .IsRequired();

        builder.Property(si => si.Quantity)
               .IsRequired();

        builder.Property(si => si.Price)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(si => si.Discount)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(si => si.Total)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(si => si.IsActive)
               .HasDefaultValue(true)
               .IsRequired();

        builder.Property(si => si.CreatedAt)
               .HasDefaultValueSql("now()")
               .IsRequired();

        builder.HasIndex(si => si.SalesId);
        builder.HasIndex(si => si.ProductId);

        builder.HasOne<Sales>()
               .WithMany()
               .HasForeignKey(si => si.SalesId);

        builder.HasIndex(si => si.ProductId);

        builder.HasOne<Products>()
               .WithMany()
               .HasForeignKey(si => si.ProductId);
    }
}
