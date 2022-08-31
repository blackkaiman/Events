using EvoEvents.Data.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvoEvents.Data.Configurations.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Email).IsRequired().HasMaxLength(74);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(20);

            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}
