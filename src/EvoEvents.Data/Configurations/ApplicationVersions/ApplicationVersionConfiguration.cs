using EvoEvents.Data.Models.ApplicationVersions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvoEvents.Data.Configurations.ApplicationVersions
{
    public class ApplicationVersionConfiguration : IEntityTypeConfiguration<ApplicationVersion>
    {
        public void Configure(EntityTypeBuilder<ApplicationVersion> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Version).IsRequired().HasMaxLength(100);

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasData(
                new ApplicationVersion { Id = 1, Name = "Create Skeleton", Version = "1.0.1" }
            );
        }
    }
}