#region

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UdemyNewMicroservice.Order.Domain.Entities;

#endregion

namespace UdemyNewMicroservice.Order.Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.Province).HasMaxLength(50).IsRequired();
        builder.Property(x => x.District).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Line).HasMaxLength(200).IsRequired();
        builder.Property(x => x.ZipCode).HasMaxLength(20).IsRequired();
    }
}