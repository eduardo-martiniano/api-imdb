using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using api_imdb.Models.Etities;

namespace api_imdb.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.AppUserId)
                   .IsRequired()
                   .HasColumnType("varchar(100)");

            builder.Property(p => p.Active)
                   .IsRequired();

            builder.ToTable("UsersActives");
        }
    }
}
