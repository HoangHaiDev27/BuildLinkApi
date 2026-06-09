using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuildLinkApi.Infrastructure.Data.Configurations
{
    public class CompanyCertificateConfiguration : IEntityTypeConfiguration<CompanyCertificate>
    {
        public void Configure(EntityTypeBuilder<CompanyCertificate> builder)
        {
            builder.ToTable("company_certificates");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.IssuedBy)
                .HasMaxLength(200);

            builder.HasOne(x => x.Company)
                .WithMany(x => x.Certificates)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.CertificateFile)
                .WithMany()
                .HasForeignKey(x => x.CertificateFileId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}