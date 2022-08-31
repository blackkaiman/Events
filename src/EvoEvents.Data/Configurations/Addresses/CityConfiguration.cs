using EvoEvents.Data.Models.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace EvoEvents.Data.Configurations.Addresses
{
    public class CityConfiguration : IEntityTypeConfiguration<CityLookup>
    {
        public void Configure(EntityTypeBuilder<CityLookup> builder)
        {
            builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
            builder.HasData(Enum.GetValues(typeof(City))
                .OfType<City>()
                .Select(x => new CityLookup() { Id = x, Name = x.ToString() })
                .ToArray());
        }
    }
}