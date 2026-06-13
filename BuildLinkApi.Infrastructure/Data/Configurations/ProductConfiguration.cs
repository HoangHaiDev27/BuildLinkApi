using BuildLinkApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuildLinkApi.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasMaxLength(280);

            builder.HasIndex(x => x.Slug)
                .IsUnique();

            builder.Property(x => x.Brand)
                .HasMaxLength(150);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.OriginalPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Unit)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Size)
                .HasMaxLength(100);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Company)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ThumbnailFile)
                .WithMany()
                .HasForeignKey(x => x.ThumbnailFileId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
