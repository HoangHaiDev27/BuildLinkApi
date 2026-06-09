using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuildLinkApi.Infrastructure.Data.Configurations
{
    public class ProjectImageConfiguration : IEntityTypeConfiguration<ProjectImage>
    {
        public void Configure(EntityTypeBuilder<ProjectImage> builder)
        {
            builder.ToTable("project_images");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Caption)
                .HasMaxLength(300);

            builder.HasOne(x => x.Project)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.FileAsset)
                .WithMany()
                .HasForeignKey(x => x.FileAssetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}