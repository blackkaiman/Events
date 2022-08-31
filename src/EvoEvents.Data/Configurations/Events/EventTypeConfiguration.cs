using EvoEvents.Data.Models.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EvoEvents.Data.Configurations.Events
{
    public class EventTypeConfiguration : IEntityTypeConfiguration<EventTypeLookup>
    {
        public void Configure(EntityTypeBuilder<EventTypeLookup> builder)
        {
            builder.Property(u => u.Name).IsRequired();
            builder.HasData(Enum.GetValues(typeof(EventType))
                .OfType<EventType>()
                .Select(x => new EventTypeLookup() { Id = x, Name = x.ToString() })
                .ToArray());
        }
    }
}