using EvoEvents.Data.Models.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace EvoEvents.Data.Configurations.Addresses
{
    public class CountryConfiguration : IEntityTypeConfiguration<CountryLookup>
    {
        public void Configure(EntityTypeBuilder<CountryLookup> builder)
        {
            builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
            builder.HasData(Enum.GetValues(typeof(Country))
              .OfType<Country>()
              .Select(x => new CountryLookup() { Id = x, Name = x.ToString() })
              .ToArray());
        }
    }
}