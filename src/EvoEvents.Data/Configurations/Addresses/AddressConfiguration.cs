using EvoEvents.Data.Models.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EvoEvents.Data.Configurations.Addresses
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(u => u.Location).IsRequired().HasMaxLength(50);
        }
    }
}