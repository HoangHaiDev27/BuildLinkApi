using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuildLinkApi.Infrastructure.Data.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("companies");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CompanyName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasIndex(x => x.Slug)
                .IsUnique();

            builder.Property(x => x.TaxCode)
                .HasMaxLength(100);

            builder.Property(x => x.RepresentativeName)
                .HasMaxLength(150);

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(30);

            builder.Property(x => x.Email)
                .HasMaxLength(150);

            builder.Property(x => x.Address)
                .HasMaxLength(500);

            builder.Property(x => x.Website)
                .HasMaxLength(250);

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(x => x.LogoFile)
                .WithMany()
                .HasForeignKey(x => x.LogoFileId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.BusinessLicenseFile)
                .WithMany()
                .HasForeignKey(x => x.BusinessLicenseFileId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProfileDocumentFile)
                .WithMany()
                .HasForeignKey(x => x.ProfileDocumentFileId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}