using BuildLinkApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuildLinkApi.Infrastructure.Data.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("addresses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Label)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Kind)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.ReceiverName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.ReceiverPhone)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Detail)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasOne(x => x.Account)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
